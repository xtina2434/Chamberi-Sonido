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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisionando");
        PlayTrain();

        Collider[] childColliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in childColliders)
        {
            Debug.Log("objetos");
            if (col.gameObject.CompareTag("Desactivable"))
            {
                Debug.Log("desactivado");
                col.enabled = false;
            }
        }
    }
}
