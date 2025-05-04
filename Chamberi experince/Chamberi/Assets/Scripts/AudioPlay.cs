using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    [SerializeField]
    public AudioClip sonido;
    public AudioSource source;

    [SerializeField]
    Material materialPasar, materialNoPasar;
    public void PlayTrain()
    {
        source.PlayOneShot(sonido);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayTrain();

        Collider[] childColliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in childColliders)
        {
            if (col.gameObject.CompareTag("Desactivable"))
            {
                col.enabled = false;

                Renderer renderer = col.gameObject.GetComponent<Renderer>();
                renderer.material = materialPasar;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Collider[] childColliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in childColliders)
        {
            if (col.gameObject.CompareTag("Desactivable"))
            {
                col.enabled = true;

                Renderer renderer = col.gameObject.GetComponent<Renderer>();
                renderer.material = materialNoPasar;
            }
        }
    }
}
