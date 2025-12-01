using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MetroDoppler : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter emitter;
    private EventInstance instance;
    private int reverbIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        emitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
        instance = emitter.EventInstance;
        instance.setReverbLevel(reverbIndex, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
}
