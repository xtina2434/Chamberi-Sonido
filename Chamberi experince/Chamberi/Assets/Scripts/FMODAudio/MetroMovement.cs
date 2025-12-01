using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroMovement : MonoBehaviour
{
    //private float puntoA = 0.0f;
    private float target = -300.0f;

    private float speed = 20f;

    //private bool dirToB = true;
    private bool start = false;
    //// Update is called once per frame
    private void Start()
    {
        Debug.Log(transform.position);
    }
    void Update()
    {
        if(start)
        {
            //float target = dirToB ? puntoB : puntoA;

            Vector3 pos = transform.position;

            pos.x = Mathf.MoveTowards(pos.x, target, speed * Time.deltaTime);
            transform.position = pos;


        }

        //if (Mathf.Abs(pos.x - target) < 0.01f)
        //{
        //    dirToB = !dirToB;
        //}
    }

    public void StartMovement()
    {
        if(!start)
        {
            start = true;
        }
    }

}
