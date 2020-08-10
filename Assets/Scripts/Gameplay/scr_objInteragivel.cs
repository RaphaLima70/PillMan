﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_objInteragivel : MonoBehaviour
{
    Animator anim;
    public bool isPorta;
    public bool usou;
    public bool empilhadeira;
    public bool isAutomatica;
    public scr_player_mov playerLink;
    void Start()
    {
        playerLink = FindObjectOfType<scr_player_mov>();
        anim = GetComponent<Animator>();
    }

    public void Interagir()
    {
        StartCoroutine(interagir());
    }

    IEnumerator interagir()
    {
        if (!isPorta && !usou)
        {
            playerLink.Interagir();
            usou = true;
            yield return new WaitForSeconds(0.25f);
            anim.SetBool("usou", true);
        }
        if (isPorta)
        {
            playerLink.Interagir();
            usou = !usou;
            yield return new WaitForSeconds(0.25f);
            anim.SetBool("usou", usou);
        }
        if (empilhadeira)
        {
            playerLink.Interagir();
            yield return new WaitForSeconds(0.25f);
            anim.SetTrigger("desce");
        }
        if (isAutomatica)
        {
            yield return new WaitForSeconds(0.25f);
            GetComponent<Collider>().isTrigger = true;
        }    
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && isAutomatica)
        {
            GetComponent<Collider>().isTrigger = false;
            anim.SetBool("usou", false);
        }
    }

}
