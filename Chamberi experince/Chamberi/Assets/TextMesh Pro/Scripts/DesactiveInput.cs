using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class DesactiveInput : MonoBehaviour
{
    public TMP_InputField inputField;
    private XRDeviceSimulator xrSim;

    void Start()
    {
        // Buscar el XRDeviceSimulator en la escena
        xrSim = FindObjectOfType<XRDeviceSimulator>();

        if (xrSim == null)
        {
            Debug.LogWarning("XRDeviceSimulator no encontrado en la escena.");
            return;
        }

        // Escuchar eventos de focus del input
        inputField.onSelect.AddListener(OnFocus);
        inputField.onDeselect.AddListener(OnUnfocus);
    }

    void OnFocus(string _)
    {
        if (xrSim != null)
            xrSim.enabled = false; // desactiva controles
    }

    void OnUnfocus(string _)
    {
        if (xrSim != null)
            xrSim.enabled = true; // vuelve a activar controles
    }
}
