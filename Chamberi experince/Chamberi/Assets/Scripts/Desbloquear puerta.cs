using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desbloquearpuerta : MonoBehaviour
{
    private void TurnDoor()
    {
        Debug.Log("Se abre la puerta");
        //Habre la puerta
        transform.Rotate(0,0,90);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Algo colisiona");
        if (collision.gameObject.tag == "Key") TurnDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo triguer");
    }
}
