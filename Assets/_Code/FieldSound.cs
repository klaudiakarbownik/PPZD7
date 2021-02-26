using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSound : MonoBehaviour
{
    FMOD.Studio.EventInstance fieldSnapshot;
    private void Start()
    {
        fieldSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Place/Field");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            fieldSnapshot.start();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            fieldSnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}
