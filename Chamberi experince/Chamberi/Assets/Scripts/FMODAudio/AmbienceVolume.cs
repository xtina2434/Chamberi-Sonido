using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AmbienceVolume : MonoBehaviour
{
    // intensidad ambiente creepy
    private string intensityParam = "ambience_intensity";

    private float maxIntensity = 1.0f;
    private float fade = 2.0f;
    private float maxDistance = 50f;

    private bool playerInside = false;
    private Transform player;

    private float currentIntensity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside)
        {
            Vector3 toPlayer = player.position - transform.position;

            float signed = Vector3.Dot(toPlayer, transform.forward);

            AudioManager.instance.ambienceCreepyEventInstance.getParameterByName(intensityParam, out currentIntensity);
          

            if (signed <= 0f)
            {
                float target = 0f;
                currentIntensity = Mathf.Lerp(currentIntensity, target, Time.deltaTime * fade);
               
            }
            else
            {
                float t = Mathf.Clamp01(signed / maxDistance);
                float target = Mathf.Lerp(0f, maxIntensity, t);

                currentIntensity = Mathf.Lerp(currentIntensity, target,Time.deltaTime * fade);
              
            }

            AudioManager.instance.ambienceCreepyEventInstance.setParameterByName(intensityParam, currentIntensity);
        }
        else
        {
            currentIntensity = Mathf.Lerp(currentIntensity, 0f, Time.deltaTime * fade);

            AudioManager.instance.ambienceCreepyEventInstance.setParameterByName(intensityParam, currentIntensity);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           playerInside = true;
        }
    }
}
