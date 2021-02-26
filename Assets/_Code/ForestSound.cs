using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSound : MonoBehaviour
{
    FMOD.Studio.EventInstance forestSnapshot;
    private void Start()
    {
        forestSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Place/Forest");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            forestSnapshot.start();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            forestSnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}
