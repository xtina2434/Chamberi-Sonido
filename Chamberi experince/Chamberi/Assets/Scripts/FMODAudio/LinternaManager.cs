using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class LinternaManager : MonoBehaviour
{
    private EventInstance clickEvent;
    public CharacterManager characterManager;
    void Start()
    {
        clickEvent = AudioManager.instance.CreateEventInstance(FMODEvents.instance.click);
    }

    // Se llama cuando se apaga/enciende la linterna y reproduce el sonido
    // Aplica reverb de la zona donde se encuentre el jugador a la instancia
    // del evento de encender/apagar la linterna 
    public void PlayClick()
    {
        int reberbIndex = characterManager.getReverbIndex();
        if (reberbIndex >= 0)
        {
            clickEvent.setReverbLevel(reberbIndex, 1.0f);
        }
        clickEvent.start();
    }

    private void OnDestroy()
    {
        clickEvent.release();
    }
}
