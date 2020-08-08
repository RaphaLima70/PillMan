using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_gatilhoInteragivel : MonoBehaviour
{
    Animator anim;
    public scr_objInteragivel interativo;
    public bool elevadorBtn;
    void Start()
    {
        if(GetComponent<Animator>() != null)
        {
            interativo = GetComponent<scr_objInteragivel>();
            anim = GetComponent<Animator>();
        }
    }

    public void InteragirObj()
    {
        interativo.Interagir();
        if(anim != null)
        {
            anim.Play("interagir");
        }
        if (elevadorBtn)
        {
            gameObject.tag = "un";
        }
    }
}
