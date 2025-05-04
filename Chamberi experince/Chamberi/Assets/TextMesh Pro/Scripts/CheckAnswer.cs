using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckAnswer : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private Color correct = Color.green;
    private Color incorrect = Color.red;
    private int correctIndex = 1;

    public void Check()
    {
        int answer = dropdown.value;

        TMP_Text text = dropdown.captionText;

        if(answer == correctIndex)
        {
            text.color = correct;
        }
        else
        {
            text.color = incorrect;
        }
    }
}
