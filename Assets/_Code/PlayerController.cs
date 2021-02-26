﻿using UnityEngine;

#pragma warning disable CS0649

public class PlayerController : MonoBehaviour {
    [SerializeField] Bandit banditController;
    [SerializeField] PlayerSubsystem[] subsystems;
    [SerializeField] Transform targetSensor;

    FMOD.Studio.EventInstance heartbeatEvent;

    bool initialised;
    PlayerInput input;

    public bool IsDead => banditController.IsDead;

    private void Update()
    {
        heartbeatEvent.setParameterByName("Health", banditController.GetHP());
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LowpassS", banditController.GetHP());

    }
    void Awake() {
        Initialise();
        heartbeatEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Heartbeat");
        heartbeatEvent.start();
    }

    void Initialise() {
        if (initialised)
            return;
        foreach (var subsystem in subsystems)
            subsystem.Initialise(this);
        banditController.JumpedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Jump);
        banditController.LandedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Landing);
        banditController.AttackedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Attack);
        banditController.DiedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Death);
        banditController.FootstepEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Footstep);
        banditController.AttackHitEvent += CheckForTargetsAndHit;
        banditController.HurtEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Hurt);
        banditController.BlockEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Block);
       
        input = new PlayerInput();
        banditController.Setup(input);
        initialised = true;
    }

    void CheckForTargetsAndHit() {
        var layer = LayerMask.NameToLayer("Enemy");
        var hit = Physics2D.Raycast(targetSensor.position, banditController.GetFacingDirection(), .1f, 1 << layer);
        if (hit) {
            var enemy = hit.transform.GetComponent<EnemyController>();
            enemy.DealDamage(25);
        }
    }

    void NotifySubsystemsAboutNewEvent(PlayerEventType eventType) {
        foreach (var playerSubsystem in subsystems)
            playerSubsystem.HandleEvent(eventType);
    }

    public void DealDamage(float damage) {
        banditController.TakeDamage(damage);
        Debug.Log(banditController.GetHP());
    }
}

public abstract class PlayerSubsystem : MonoBehaviour {
    protected PlayerController player;

    public void Initialise(PlayerController player) {
        this.player = player;
    }

    public abstract void HandleEvent(PlayerEventType eventType);
}

public enum PlayerEventType {
    Jump,
    Landing,
    Death,
    Attack,
    Footstep,
    Hurt,
    Block
}