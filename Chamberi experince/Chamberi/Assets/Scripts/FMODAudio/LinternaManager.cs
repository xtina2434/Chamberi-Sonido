using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class LinternaManager : MonoBehaviour
{
    private EventInstance clickEvent;
    void Start()
    {
        clickEvent = AudioManager.instance.CreateEventInstance(FMODEvents.instance.click);
    }

    public void PlayClick()
    {
        clickEvent.start();
    }

    private void OnDestroy()
    {
        clickEvent.release();
    }
}
