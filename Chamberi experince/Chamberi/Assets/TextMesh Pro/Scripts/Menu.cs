using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject _playCanvas;
    [SerializeField]
    GameObject _pauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        // Activar canvas de play
        _playCanvas.SetActive(true);
    }

    public void Play()
    {
        // Activar canvas de pausa
        _pauseCanvas.SetActive(true);
    }
}
