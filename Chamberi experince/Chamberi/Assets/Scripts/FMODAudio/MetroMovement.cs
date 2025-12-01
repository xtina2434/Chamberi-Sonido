using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            pos.x = Mathf.MoveTowards(pos.x, target, speed * Time.deltaTime);
            transform.position = pos;

        }
    }

    public void StartMovement()
    {
        if(!start)
        {
            start = true;
        }
    }
}
