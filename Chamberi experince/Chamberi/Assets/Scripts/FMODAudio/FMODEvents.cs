using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience Creepy")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps  { get; private set; }

    [field : Header("Rat SFX")]
    [field: SerializeField] public EventReference ratSound { get; private set;}
    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Se ha encontrado mas de un FMODEvents script en la escena.");
        }
        instance = this;
    }
}
