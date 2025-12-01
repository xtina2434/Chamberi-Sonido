using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

// Almacena todas las referencias a eventos FMOD usados en la escena 
// Permite acceso global implementando el patron singleton
public class FMODEvents : MonoBehaviour
{
    [field: Header("Metro SFX")]
    [field: SerializeField] public EventReference metro { get; private set; }
    [field: Header("Ambience Creepy")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Tormenta")]
    [field: SerializeField] public EventReference tormenta { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps  { get; private set; }

    [field : Header("Rat SFX")]
    [field: SerializeField] public EventReference ratSound { get; private set;}

    [field: Header("Linterna SFX")]
    [field: SerializeField] public EventReference click { get; private set; }
    [field: Header("Torno SFX")]
    [field: SerializeField] public EventReference torno { get; private set; }
    [field: Header("Gotas SFX")]
    [field: SerializeField] public EventReference gotas { get; private set; }

    [field: Header("Maquinas SFX")]
    [field: SerializeField] public EventReference maquina { get; private set; }
    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        // Evita tener mas de una instancia
        if (instance != null)
        {
            Debug.LogError("Se ha encontrado mas de un FMODEvents script en la escena.");
        }
        instance = this;
    }
}
