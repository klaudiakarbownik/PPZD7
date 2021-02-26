using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour {
    public Animator animator;
    public UnityEvent onDoorOpen = new UnityEvent();
    public UnityEvent onDoorClose = new UnityEvent();
    private const string PlayerTag = "Player";
    private const string AnimatorPropertyName = "character_nearby";
    private int _animatorPropertyHash;

    private void Start()
    {
        _animatorPropertyHash = Animator.StringToHash(AnimatorPropertyName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(PlayerTag))
        {
            return;
        }
        
        animator.SetBool(_animatorPropertyHash, true);
        onDoorOpen.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(PlayerTag))
        {
            return;
        }
        
        animator.SetBool(_animatorPropertyHash, false);
        onDoorClose.Invoke();
    }
}
