using UnityEngine;

public class EnemyController : MonoBehaviour {
    public Bandit banditController;
    public float speed = .5f;
    public float attackRange = .25f;
    public float attackCooldown = 2f;
    public float damage = 10;

    private EnemyInput _enemyInput;
    private PlayerController _playerController;
    private float _timeToNextAttack;
    private float _distanceFromTarget;
    private const string FmodHitSoundPath = "event:/Enemy Sword Hit";
    private const string FmodDamageSoundPath = "event:/Enemy Damage";
    private const string FmodDeathSoundPath = "event:/Enemy Death";

    private void Awake() {
        _enemyInput = new EnemyInput();
        banditController.Setup(_enemyInput);
        banditController.AttackHitEvent += CheckIfTargetInRange;
        banditController.DiedEvent += KillSelf;
        banditController.DiedEvent += HandleDeath;
    }

    private void KillSelf() {
        Destroy(gameObject, 3);
    }

    private void CheckIfTargetInRange() {
        if (_distanceFromTarget <= attackRange)
        {
            _playerController.DealDamage(damage);
        }
    }

    private void Update() {
        if (banditController.IsDead) {
            _enemyInput.horizontalMove = 0;
            
            return;
        }
        
        UpdateTarget();
        UpdateMovement();
        UpdateAttack();
    }

    private void UpdateTarget() {
        if (!_playerController)
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        _distanceFromTarget = (_playerController.transform.position - transform.position).magnitude;
    }

    private void UpdateMovement() {
        if (_playerController && !_playerController.IsDead) {
            Vector3 targetPosition = _playerController.transform.position;
            Vector3 movementDirection = (targetPosition - transform.position).normalized;
            
            _enemyInput.horizontalMove = movementDirection.x * speed;
        }
        else
        {
            _enemyInput.horizontalMove = 0;
        }
    }

    private void UpdateAttack() {
        _enemyInput.isAttacking = false;
        
        if (_timeToNextAttack > 0) {
            _timeToNextAttack -= Time.deltaTime;
            
            return;
        }

        if (!_playerController || _playerController.IsDead)
        {
            return;
        }

        if (!(_distanceFromTarget <= attackRange))
        {
            return;
        }
        
        _enemyInput.isAttacking = true;
        _timeToNextAttack = attackCooldown;
                
        FMODUnity.RuntimeManager.PlayOneShot(FmodHitSoundPath, transform.position);
    }

    public void DealDamage(float damageToDeal) {
        banditController.TakeDamage(damageToDeal);
        
        FMODUnity.RuntimeManager.PlayOneShot(FmodDamageSoundPath, transform.position);
    }

    private void HandleDeath() {
        GetComponent<Collider2D>().enabled = false;
        
        FMODUnity.RuntimeManager.PlayOneShot(FmodDeathSoundPath, transform.position);
    }


}