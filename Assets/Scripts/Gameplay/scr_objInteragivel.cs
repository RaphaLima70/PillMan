using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_objInteragivel : MonoBehaviour
{
    Animator anim;
    public bool isPorta;
    public bool usou;
    public bool empilhadeira;
    public bool isAutomatica;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interagir()
    {
        StartCoroutine(interagir());
    }

    IEnumerator interagir()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isPorta && !usou)
        {
            usou = true;
            anim.SetBool("usou", true);
        }
        if (isPorta)
        {
            usou = !usou;
            anim.SetBool("usou", usou);
        }
        if (empilhadeira)
        {
            anim.SetTrigger("desce");
        }
        if (isAutomatica)
        {
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
