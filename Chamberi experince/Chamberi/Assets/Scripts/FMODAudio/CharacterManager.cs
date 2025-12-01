using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // indice de la zona de reverb por donde este pasando el jugador
    private int currentReverbIndex = -1;
    private int previousReverbIndex = -1;
    // evento de FMOD de pasos del jugador
    private EventInstance playerFootsteps;

    private CharacterController characterController;

    // distancia minima que el personaje debe moverse entre frames de FixedUpdate
    // para considerarse que esta en movimiento
    private float minDistanceMoved = 0.05f;

    // posicion del frame anterior
    private Vector3 lastPosition;

    // bandera que indica si el personaje esta moviendose
    private bool isMoving = false;

    private Coroutine reverbFadeCoroutine;
    private float fade = 1.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // posicion inicial
        lastPosition = transform.position;
        if (FMODEvents.instance != null && AudioManager.instance != null)
        {
            playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (characterController == null)
        {
            Debug.Log("controller null");
            return;
        }

        UpdateMovementState();
        UpdateSound();
        // actualiza la posicion 3D de la fuente de sonido 
        playerFootsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
    private void UpdateMovementState()
    {
        // posicion actual
        Vector3 currentPosition = transform.position;

        // ignorar componente Y
        Vector3 currentHorizontal = new Vector3(currentPosition.x, 0f, currentPosition.z);
        Vector3 lastHorizontal = new Vector3(lastPosition.x, 0f, lastPosition.z);

        // calcular distancia real movida desde el ultimo fixedupdate
        float distanceMoved = Vector3.Distance(currentHorizontal, lastHorizontal);

        // si la distancia movida supera el umbral minimo, se considera que el personaje esta en movimiento
        isMoving = distanceMoved > minDistanceMoved;
        // actualizar ultima posicion
        lastPosition = currentPosition;
    }
    private void UpdateSound()
    {
        if (isMoving)
        {
            //si el sonido esta detenido, se inicia
            PLAYBACK_STATE playbackState;
            //estado actual de la instancia de sonido
           playerFootsteps.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }

        else
        {
            // si el personaje esta quieto, se detiene el sonido
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    public  int getReverbIndex()
    {
        return currentReverbIndex;
    }
    private void OnTriggerEnter(Collider other)
    {
        ReverbManager reverbZone = other.GetComponent<ReverbManager>();
        if(reverbZone != null)
        {
            previousReverbIndex = currentReverbIndex;
            currentReverbIndex = reverbZone.reverbIndex;

            if (reverbFadeCoroutine != null)
            {
                StopCoroutine(reverbFadeCoroutine);
            }
            reverbFadeCoroutine = StartCoroutine(FadeReverbSmooth(previousReverbIndex, currentReverbIndex));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ReverbManager reverbZone = other.GetComponent<ReverbManager>();
        if(reverbZone != null)
        {
            previousReverbIndex = currentReverbIndex ;
            currentReverbIndex = -1;
            if (reverbFadeCoroutine != null)
            {
                StopCoroutine(reverbFadeCoroutine);
            }
            reverbFadeCoroutine = StartCoroutine(FadeReverbSmooth(previousReverbIndex, currentReverbIndex));
        }
    }
    private void OnDestroy()
    {
        playerFootsteps.release();
    }
    private IEnumerator FadeReverbSmooth(int fromIndex, int toIndex)
    {

        float time = 0f;

        while (time < fade)
        {
            time += Time.deltaTime;
            float t = time / fade;

            if(fromIndex >= 0)
            {
                playerFootsteps.setReverbLevel(fromIndex, Mathf.Lerp(1f,0f,t));
            }
            if (toIndex >= 0)
            {
                playerFootsteps.setReverbLevel(toIndex, Mathf.Lerp(0f, 1f, t));
            }


            yield return null;
        }
        if (fromIndex >= 0)
        {
            playerFootsteps.setReverbLevel(fromIndex, 0f);
        }
        if (toIndex >= 0)
        {
            playerFootsteps.setReverbLevel(toIndex, 1f);
        }
    }
}
