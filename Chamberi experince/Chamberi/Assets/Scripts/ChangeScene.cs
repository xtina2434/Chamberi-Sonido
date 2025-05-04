using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        SceneManager.LoadScene(1);  //Cambia a la pantalla de fin
    }
}
