using System;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;


public class DayNightController : MonoBehaviour {
    public static event Action<TimeOfDay> TimeOfDayChangedEvent = delegate { };
    TimeOfDay timeOfDay;

    public TimeOfDay TimeOfDay {
        get => timeOfDay;
        set {
            if (timeOfDay != value) {
                TimeOfDayChangedEvent(value);
                timeOfDay = value;
            }
        }
    }

    public void GotoNextStage() {
        EventInstance daySnapshot = RuntimeManager.CreateInstance("snapshot:/TimeOfDay/Day");
        EventInstance sunsetSnapshot = RuntimeManager.CreateInstance("snapshot:/TimeOfDay/Sunset");
        EventInstance nightSnapshot = RuntimeManager.CreateInstance("snapshot:/TimeOfDay/Night");
        TimeOfDay = (TimeOfDay) (((int) timeOfDay + 1) % 3);
        Debug.Log("Time of day is: " + timeOfDay);
        switch ((int)timeOfDay)
        {
            case 0:
                nightSnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                sunsetSnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                daySnapshot.start();
                break;
            case 1:
                daySnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                nightSnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                sunsetSnapshot.start();
                break;
            case 2:
                daySnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                sunsetSnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                nightSnapshot.start();
                break;
        }
    }
}