using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Detecta cuando el jugador entra al anden
// y avisa al metro para que empiece su movimiento
public class AndenManager : MonoBehaviour
{
    public MetroMovement metro;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            metro.StartMovement();
        }
    }
}
