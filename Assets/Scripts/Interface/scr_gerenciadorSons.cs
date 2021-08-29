using UnityEngine;
using System;
using UnityEngine.UI;

public class scr_gerenciadorSons : MonoBehaviour
{
    public scr_som[] sons;

    public Slider sliderMusica;
    public Slider sliderSons;

    private void Awake()
    {
        foreach (scr_som s in sons)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.mute = s.mute;
        }
    }

    private void Start()
    {
        Play("Musica");
    }

    public void Play(string nome)
    {
        scr_som s = Array.Find(sons, som => som.nome == nome);
        if (s == null)
        {
            Debug.LogWarning("Som: " + nome + "não encontrado!");
            return;
        }
        s.source.Play();
    }
}
