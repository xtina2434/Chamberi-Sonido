using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
