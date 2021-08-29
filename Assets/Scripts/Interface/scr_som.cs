using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class scr_som 
{
    public string nome;
    public AudioClip clip;

    [Range(0, 1)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool playOnAwake;
    public bool mute;
    [HideInInspector]
    public AudioSource source;

}
