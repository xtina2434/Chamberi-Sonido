using FMOD.Studio;
using UnityEngine;

public class MetroManager : MonoBehaviour
{
    private EventInstance trenInstance;
    private int currentReverbIndex = -1;

    void Start()
    {
        trenInstance = AudioManager.instance.CreateEventInstance(FMODEvents.instance.metro);
    }

    void Update()
    {
        // Actualizar posición 3D con doppler
        trenInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        ReverbManager zone = other.GetComponent<ReverbManager>();
        if (zone != null)
        {
            currentReverbIndex = zone.reverbIndex;
            trenInstance.setReverbLevel(currentReverbIndex, 1.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ReverbManager zone = other.GetComponent<ReverbManager>();
        if (zone != null)
        {
            currentReverbIndex = -1;

            // apagar todas las reverbs igual que con los pasos
            trenInstance.setReverbLevel(0, 0f);
            trenInstance.setReverbLevel(1, 0f);
            trenInstance.setReverbLevel(2, 0f);
            trenInstance.setReverbLevel(3, 0f);
        }
    }
}
