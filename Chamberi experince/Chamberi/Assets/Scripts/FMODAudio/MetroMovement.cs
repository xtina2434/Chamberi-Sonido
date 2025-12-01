using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla el movimiento del metro desde su punto inicial a un destno
public class MetroMovement : MonoBehaviour
{
    private float target = -300.0f;
    private float speed = 20f;
    private bool start = false;
    void Update()
    {
        if(start)
        {
            Vector3 pos = transform.position;

            // Mueve la posicion X hacia el objetivo de forma suave
            pos.x = Mathf.MoveTowards(pos.x, target, speed * Time.deltaTime);
            transform.position = pos;
        }
    }

    // Se llama cuando el jugador ha entrado al anden y activa el movimiento del metro
    public void StartMovement()
    {
        if(!start)
        {
            start = true;
        }
    }
}
