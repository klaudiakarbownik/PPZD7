using UnityEngine;
using UnityEngine.Audio;

public class InsideAudio : MonoBehaviour
{
    public AudioMixerSnapshot outsideAudioMixerSnapshot;
    public AudioMixerSnapshot insideAudioMixerSnapshot;
    public float transitionTime = 1.0f;
    public const string RoomTag = "Room";

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(RoomTag))
        {
            outsideAudioMixerSnapshot.TransitionTo(transitionTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(RoomTag))
        {
            insideAudioMixerSnapshot.TransitionTo(transitionTime);
        }
    }
}
