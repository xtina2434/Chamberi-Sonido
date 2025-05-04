using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    [SerializeField]
    public AudioClip sonido;
    public AudioSource source;
    public void PlayTrain()
    {
        source.PlayOneShot(sonido);
    }
}
