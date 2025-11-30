using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AmbienceStorm : MonoBehaviour
{
    private string intensityParam = "intensidad_tormenta";
    private string frequencyParam = "frecuencia_tormenta";


    private string obsParam = "obstruccion_tormenta";
    private string oclParam = "oclusion_tormenta";

    private float intensitySpeed = 0.05f;
    private float frequencySpeed = 0.5f;

    private bool playerInside = false;
    private float maxValue = 1.0f;
    private float fade = 0.5f;
    private float currentValue = 0f;
    private float maxDistance = 50f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        float intensityValue = Mathf.PingPong(Time.time * intensitySpeed, 1f);
        //float frequencyValue = Mathf.PingPong(Time.time * frequencySpeed, 1f);

        float frequencyValue = 0.0f;

        if (intensityValue >= 0.5f)
        {
            float t = Mathf.InverseLerp(0.5f, 1f, intensityValue);  

            frequencyValue = Mathf.PingPong(Time.time * frequencySpeed, 1f) * t;
        }
        AudioManager.instance.tormentaEventInstance.setParameterByName(intensityParam, intensityValue);
        AudioManager.instance.tormentaEventInstance.setParameterByName(frequencyParam, frequencyValue);

        if (playerInside)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            float t = Mathf.Clamp01(distance / maxDistance);
            float target = Mathf.Lerp(0f, 1f, t);

            //float target = playerInside ? 1f : 0f;

            currentValue = Mathf.Lerp(currentValue, target, Time.deltaTime * fade);
        }
        else
        {
            currentValue = Mathf.Lerp(currentValue, 0f, Time.deltaTime * fade);
        }

        AudioManager.instance.tormentaEventInstance.setParameterByName(obsParam, currentValue);
        AudioManager.instance.tormentaEventInstance.setParameterByName(oclParam, currentValue);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("entro");
        }
    }
}
