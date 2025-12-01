using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestiona el audio de los pasos del jugador con reverb segun la zona en la que se encuentre
public class CharacterManager : MonoBehaviour
{
    // Indice de la zona de reverb actual y anterior
    private int currentReverbIndex = -1;
    private int previousReverbIndex = -1;

    // Instancia del evento de FMOD de pasos del jugador
    private EventInstance playerFootsteps;

    private CharacterController characterController;
    // Umbral de distancia minima para considerar que el jugador esta en movimiento
    private float minDistanceMoved = 0.05f;
    // Posicion del frame anterior
    private Vector3 lastPosition;
    // Bandera que indica si el personaje esta moviendose
    private bool isMoving = false;

    // Courutine para transicion suave de reverb
    private Coroutine reverbFadeCoroutine;
    // Duracion del fade
    private float fade = 1.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        // Posicion inicial
        lastPosition = transform.position;

        // Inicializa el evento de pasos
        if (FMODEvents.instance != null && AudioManager.instance != null)
        {
            playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
        }
    }
    private void FixedUpdate()
    {
        if (characterController == null)
        {
            Debug.Log("controller null");
            return;
        }

        UpdateMovementState();
        UpdateSound();
        // Actualiza la posicion 3D de la fuente de sonido (el jugador esta en movimiento)
        playerFootsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    // Comprueba si el jugador se ha movido lo suficiente desde el ultimo FixedUpdate
    private void UpdateMovementState()
    {
        // Posicion actual
        Vector3 currentPosition = transform.position;

        // Ignorar componente Y
        Vector3 currentHorizontal = new Vector3(currentPosition.x, 0f, currentPosition.z);
        Vector3 lastHorizontal = new Vector3(lastPosition.x, 0f, lastPosition.z);

        // Calcular distancia real movida desde el ultimo fixedupdate
        float distanceMoved = Vector3.Distance(currentHorizontal, lastHorizontal);

        // Si la distancia movida supera el umbral minimo, se considera que el personaje esta en movimiento
        isMoving = distanceMoved > minDistanceMoved;

        // Actualizar ultima posicion
        lastPosition = currentPosition;
    }

    // Reproduce o detiene el sonido de pasos segun si el jugador se mueve 
    private void UpdateSound()
    {
        if (isMoving)
        {
            // Estado actual de la instancia de sonido
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);

            // Si el sonido esta detenido, se inicia
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }

        else
        {
            // Si el personaje esta quieto, se detiene el sonido
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    // Devuelve el indice de la zona de reverb donde se encuentra actualmente el jugador
    public  int getReverbIndex()
    {
        return currentReverbIndex;
    }

    // Detecta cuando un jugador ha entrado a una zona de reverb
    private void OnTriggerEnter(Collider other)
    {
        ReverbManager reverbZone = other.GetComponent<ReverbManager>();
        if(reverbZone != null)
        {
            // Actualiza el indice previo y actual
            previousReverbIndex = currentReverbIndex;
            currentReverbIndex = reverbZone.reverbIndex;

            if (reverbFadeCoroutine != null)
            {
                StopCoroutine(reverbFadeCoroutine);
            }

            // Transicion suave entre la reverb anterior y la nueva
            reverbFadeCoroutine = StartCoroutine(FadeReverbSmooth(previousReverbIndex, currentReverbIndex));
        }
    }

    // Detecta cuando un jugador ha salido de una zona de reverb
    private void OnTriggerExit(Collider other)
    {
        ReverbManager reverbZone = other.GetComponent<ReverbManager>();
        if(reverbZone != null)
        {
            // Actualiza el indice previo y actual
            previousReverbIndex = currentReverbIndex ;
            currentReverbIndex = -1;
            if (reverbFadeCoroutine != null)
            {
                StopCoroutine(reverbFadeCoroutine);
            }
            // Transicion suave entre la reverb anterior y la nueva
            reverbFadeCoroutine = StartCoroutine(FadeReverbSmooth(previousReverbIndex, currentReverbIndex));
        }
    }
    private void OnDestroy()
    {
        playerFootsteps.release();
    }

    // Aplica una transicion (fade in/fade out) entre dos zonas de reverb
    // Los indices de las zonas de reverb validos son >= 0
    private IEnumerator FadeReverbSmooth(int fromIndex, int toIndex)
    {
        // Tiempo transcurrido del fade
        float time = 0f;

        // Bucle de interpolacion hasta completar la duracion del fade
        while (time < fade)
        {
            time += Time.deltaTime; // Incrementa tiempo segun frame
            float t = time / fade;  // Normaliza tiempo entre 0 y 1

            // Reduce la reverb de la zona anterior
            if(fromIndex >= 0)
            {
                playerFootsteps.setReverbLevel(fromIndex, Mathf.Lerp(1f,0f,t));
            }
            // Aumenta la reverb de la zona nueva
            if (toIndex >= 0)
            {
                playerFootsteps.setReverbLevel(toIndex, Mathf.Lerp(0f, 1f, t));
            }

            // Espera al siguiente frame
            yield return null;
        }
        // Asegurar que el reverb de la zona anterior quede apagado
        if (fromIndex >= 0)
        {
            playerFootsteps.setReverbLevel(fromIndex, 0f);
        }
        // Asegurar que el reverb de la zona nueva quede completamente activo
        if (toIndex >= 0)
        {
            playerFootsteps.setReverbLevel(toIndex, 1f);
        }
    }
}
