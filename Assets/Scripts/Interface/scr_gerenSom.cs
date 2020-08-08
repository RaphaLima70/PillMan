using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_gerenSom : MonoBehaviour
{
    public AudioSource[] musicas;
    public AudioSource[] efeitos;
    public bool mute;
    public GameObject mutadoObj;
    public GameObject desmutadoObj;

    private void Update()
    {
        if (PlayerPrefs.GetString("mute").Equals("mute"))
        {
            mute = true;
            mutadoObj.SetActive(true);
            desmutadoObj.SetActive(false);
        }
        else
        {
            mute = false;
            mutadoObj.SetActive(false);
            desmutadoObj.SetActive(true);
        }
        AtualizarSom();
    }
    public void TocarEfeito(int num)
    {
        efeitos[num].Play();
    }

    public void AtualizarSom()
    {
        foreach (AudioSource som in musicas)
        {
            som.mute = mute;
        }

        foreach (AudioSource som in efeitos)
        {
            som.mute = mute;
        }
    }

    public void Mutar()
    {
        if (PlayerPrefs.GetString("mute").Equals("mute"))
        {
            PlayerPrefs.SetString("mute", "unmute");
        }
        else
        {
            PlayerPrefs.SetString("mute", "mute");
        }
        AtualizarSom();
    }
}
