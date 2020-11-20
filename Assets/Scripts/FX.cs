using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    public AudioSource Fx;
    public AudioClip clickFx;

    public void ClickSound()
    {
        Fx.PlayOneShot (clickFx);
    }
}
