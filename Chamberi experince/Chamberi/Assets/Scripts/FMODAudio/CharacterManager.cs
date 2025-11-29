using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // indice de la zona de reverb por donde este pasando el jugador
    private int currentReverbIndex = -1;

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
        if (distanceMoved > minDistanceMoved)
        {
            isMoving = true;
        }
        else
        {
            isMoving= false;
        }

        // actualizar ultima posicion
        lastPosition = currentPosition;
    }
    private void UpdateSound()
    {
        if (isMoving)
        {
            // Aplicar reverb si estamos en una zona de reverb
            if (currentReverbIndex >= 0)
            {
                playerFootsteps.setReverbLevel(currentReverbIndex, 1.0f);
            }
            else
            {
                // fuera de zonas, reverb OFF
                playerFootsteps.setReverbLevel(0, 0.0f);
                playerFootsteps.setReverbLevel(1, 0.0f);
                playerFootsteps.setReverbLevel(2, 0.0f);
                playerFootsteps.setReverbLevel(3, 0.0f);
            }
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

    private void OnTriggerEnter(Collider other)
    {
        ReverbManager reverbZone = other.GetComponent<ReverbManager>();
        if(reverbZone != null)
        {
            currentReverbIndex = reverbZone.reverbIndex;
            Debug.Log($"Entramos en zona Reverb: {other.name}, Preset: {reverbZone.preset}, Index: {reverbZone.reverbIndex}");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ReverbManager reverbZone = other.GetComponent<ReverbManager>();
        if(reverbZone != null)
        {
            currentReverbIndex = -1;
            Debug.Log($"Salimos de zona Reverb: {other.name}, Preset: {reverbZone.preset}");
        }
    }
    private void OnDestroy()
    {
        playerFootsteps.release();
    }
}
