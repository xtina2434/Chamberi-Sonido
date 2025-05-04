using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveName : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text text;


    public void Saludar()
    {
        string nombre = inputField.text;
        text.text = "¡Hola " + nombre + "!";
    }
}
