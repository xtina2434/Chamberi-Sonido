using FMOD;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestiona una zona de reverb 3D en FMOD para los diferentes sonidos de la escena
// Configura el preset de reverb y los radios de influencia
public class ReverbManager : MonoBehaviour
{
    // Indice de la zona de reverb
    [Range(0, 3)]
    public int reverbIndex = 0;

    // Diferentes preset disponibles
    public enum Preset { Tunel, Anden, Entrada, Ciudad}
    // Preset de reverb a usar
    public Preset preset = Preset.Tunel;

    // Radios minimo y maximos de efecto
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;

    private FMOD.REVERB_PROPERTIES reverbProps;
    private FMOD.Reverb3D reverbInstance;
   
    void Start()
    {
        // Selecciona el preset de reverb segun la zona
        switch (preset)
        {
            case Preset.Tunel:
                reverbProps = FMOD.PRESET.ARENA();
                break;
            case Preset.Anden:
                reverbProps = FMOD.PRESET.STONEROOM();
                break;
            case Preset.Entrada:
                reverbProps = FMOD.PRESET.STONECORRIDOR();
                break;
            case Preset.Ciudad:
                reverbProps = FMOD.PRESET.CITY();
                break;
        }

        // Aplicar las propiedades del reverb a nivel del sistema
        var systemCore = RuntimeManager.CoreSystem;
        RESULT result = systemCore.setReverbProperties(reverbIndex, ref reverbProps);
        if (result != RESULT.OK)
        {
            UnityEngine.Debug.LogError("FMOD setReverbProperties error: " + result);
        }

        // Crear la instancia de Reverb3D 
        result = systemCore.createReverb3D(out reverbInstance);
        if (result != RESULT.OK)
        {
            UnityEngine.Debug.LogError("FMOD createReverb3D error: " + result);
            return;
        }

        // Establecer posicion y radios del reverb
        // Convertir la posicion de Unity a FMOD_VECTOR
        Vector3 pos = transform.position;
        FMOD.VECTOR fmodPos = new FMOD.VECTOR
        {
            x = pos.x,
            y = pos.y,
            z = pos.z
        };
        result = reverbInstance.set3DAttributes(
            ref fmodPos,
            minDistance,
            maxDistance
        );

        if (result != RESULT.OK)
        {
            UnityEngine.Debug.LogError("FMOD set3DAttributes error: " + result);
        }
    }

    private void OnDestroy()
    {
        reverbInstance.release();
    }

    // Actualiza la posicion del reverb por si el gameobject se mueve
    private void Update()
    {
        Vector3 pos = transform.position;
        FMOD.VECTOR fmodPos = new FMOD.VECTOR
        {
            x = pos.x,
            y = pos.y,
            z = pos.z
        };

        reverbInstance.set3DAttributes(
            ref fmodPos,
            minDistance,
            maxDistance
        );
    }

}
