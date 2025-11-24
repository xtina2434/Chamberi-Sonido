
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Se ha encontrado mas de un Audio Manager en la escena.");
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 position) {

        RuntimeManager.PlayOneShot(sound, position);
    }
    
    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
