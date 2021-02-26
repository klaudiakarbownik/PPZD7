using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudioController : PlayerSubsystem
{
    private const string FmodJumpSoundPath = "event:/Jump";
    private const string FmodLandSoundPath = "event:/Land";
    private const string FmodDeathSoundPath = "event:/Player Death";
    private const string FmodHitSoundPath = "event:/Sword Hit";
    private const string FmodFootstepSoundPath = "event:/Footstep";
    private const string FmodDamageSoundPath = "event:/Player Damage";
    private const string FmodBlockSoundPath = "event:/Sword Block";
    
    public override void HandleEvent(PlayerEventType eventType) {
        switch (eventType) {
            case PlayerEventType.Jump:
                FMODUnity.RuntimeManager.PlayOneShot(FmodJumpSoundPath, transform.position);
                break;
            case PlayerEventType.Landing:
                FMODUnity.RuntimeManager.PlayOneShot(FmodLandSoundPath, transform.position);
                break;
            case PlayerEventType.Death:
                FMODUnity.RuntimeManager.PlayOneShot(FmodDeathSoundPath, transform.position);
                break;
            case PlayerEventType.Attack:
                FMODUnity.RuntimeManager.PlayOneShot(FmodHitSoundPath, transform.position);
                break;
            case PlayerEventType.Footstep:
                FMODUnity.RuntimeManager.PlayOneShot(FmodFootstepSoundPath, transform.position);
                break;
            case PlayerEventType.Hurt:
                FMODUnity.RuntimeManager.PlayOneShot(FmodDamageSoundPath, transform.position);
                break;
            case PlayerEventType.Block:
                FMODUnity.RuntimeManager.PlayOneShot(FmodBlockSoundPath, transform.position);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
        }
    }
}
