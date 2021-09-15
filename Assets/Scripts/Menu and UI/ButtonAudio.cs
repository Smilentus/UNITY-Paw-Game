using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource aSource;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void StartPlaying()
    {
        aSource.Play();
    }

    public void StopPlaying()
    {
        Invoke("AwaykStop", 1);
    }

    public void AwaykStop()
    {
        aSource.Stop();
    }
}
