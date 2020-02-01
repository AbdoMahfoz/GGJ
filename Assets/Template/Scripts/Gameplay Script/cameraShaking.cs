using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cameraShaking : MonoBehaviour
{

    public float shakeDuration, shakeAmplitude, shakeFrequency;

    float elapseTime;

    public CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin camNoise;

    void Awake()
    {
        camNoise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(elapseTime > 0)
        {
            camNoise.m_AmplitudeGain = shakeAmplitude;
            camNoise.m_FrequencyGain = shakeFrequency;
            elapseTime -= Time.deltaTime;
        }
        else
        {
            camNoise.m_AmplitudeGain = 0;
            elapseTime = 0;
        }
    }

    public void shakeItLow()
    {
        shakeAmplitude = 0.25f;
        elapseTime = shakeDuration;
    }
    public void shakeItMed()
    {
        shakeAmplitude = 0.65f;
        elapseTime = shakeDuration;
    }
}
