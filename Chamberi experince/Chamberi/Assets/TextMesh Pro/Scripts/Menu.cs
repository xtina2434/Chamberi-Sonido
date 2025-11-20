using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject _playCanvas;
    [SerializeField]
    GameObject _pauseCanvas;
    [SerializeField]
    GameObject _settingsCanvas;

    public void Pause()
    {
        // Activar canvas de play
        _settingsCanvas.SetActive(true);
    }

    public void Play()
    {
        // Activar canvas de pausa
        _pauseCanvas.SetActive(true);
    }
}
