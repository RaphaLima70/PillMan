using Chronos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_rewindControll : MonoBehaviour
{
    Clock clockPlayer, clockIni;
    scr_gameManager managerLink;
    public float energia;
    public float delayInicial;
    float delay;
    float custo;
    Image energiaBar;
    GameObject icone;
    scr_player_mov playerLink;
    public bool rebobinando;
    

    void Start()
    {
        energiaBar = GameObject.Find("BarraDeEnergia").GetComponent<Image>();
        icone = GameObject.Find("Icone");
        delay = 0;
        managerLink = FindObjectOfType<scr_gameManager>();
        playerLink = FindObjectOfType<scr_player_mov>();
        energia = managerLink.energiaMax;
        clockPlayer = Timekeeper.instance.Clock("Player");
        clockIni = Timekeeper.instance.Clock("Inimigos");
    }

    void Update()
    {
        energiaBar.fillAmount =  (energia/15) *0.3f;

        if (managerLink.trocandoDeCena)
        {
            icone.SetActive(false);
        }
        if (managerLink.comecou)
        {
            Rewind();
            Slow();
            ControlaPoder();
        }
    }

    public void Rewind()
    {
        if (!managerLink.slowTempo)
        {
            custo = 3;
            if (Input.GetButtonDown("Fire1") && energia > 0 && delay <= 0)
            {
                managerLink.controlandoTempo = true;
                clockPlayer.localTimeScale = -1;
                clockIni.localTimeScale = -1;
                playerLink.rebobinando = true;
                rebobinando = true;
            }

            if (Input.GetButtonUp("Fire1") || energia <= 0)
            {
                if (managerLink.controlandoTempo)
                    delay = delayInicial;
                PararPoder();
            }
        }

    }

    public void Slow()
    {
        if (!managerLink.controlandoTempo && managerLink.faseAtual>1)
        {
            custo = 2.5f;
            if (Input.GetButtonDown("Fire2") && energia > 0 && delay <= 0)
            {
                managerLink.slowTempo = true;
                clockIni.localTimeScale = 0.1f;
            }

            if (Input.GetButtonUp("Fire2") || energia <= 0)
            {
                if (managerLink.slowTempo)
                    delay = delayInicial;
                PararPoder();

            }
        }
    }

    public void ControlaPoder()
    {
        rebobinando = false;
        if (!managerLink.controlandoTempo && !managerLink.slowTempo)
        {
            if (energia < managerLink.energiaMax)
            {
                energia += 0.5f * Time.deltaTime;
            }
            else
            {
                energia = managerLink.energiaMax;
            }

            if (delay > 0)
                delay -= Time.deltaTime;
        }
        else
        {
            energia -= custo * Time.deltaTime;
        }
    }

    public void PararPoder()
    {
        playerLink.rebobinando = false;
        clockPlayer.localTimeScale = 1;
        clockIni.localTimeScale = 1;
        managerLink.controlandoTempo = false;
        managerLink.slowTempo = false;
    }

    public void AumentaEnergia()
    {
        managerLink.energiaMax++;
    }
}
