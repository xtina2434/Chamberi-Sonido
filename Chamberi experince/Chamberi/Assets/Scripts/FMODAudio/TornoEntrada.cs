using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class TornoEntrada : MonoBehaviour
{
    bool hasEnter = false;
    private FMODUnity.StudioEventEmitter emitter;
    private EventInstance instance;
    private int reverbIndex = 2;

    // Aplica reverb de la zona de oficina a la instancia del evento de los tornos
    void Start()
    {
        emitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
        instance = emitter.EventInstance;
        instance.setReverbLevel(reverbIndex, 1.0f);
    }

    // Detecta cuando el jugador pasa el torno y se desactiva
    // Cuando sale, se vuelve a activar
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hasEnter = !hasEnter;
        }
        if (hasEnter)
        {
            emitter.Play();
        }
    }
}
