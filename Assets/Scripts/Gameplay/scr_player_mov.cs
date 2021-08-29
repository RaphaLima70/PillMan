using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_mov : MonoBehaviour
{
    float velocidade;
    Vector3 movimento;
    Rigidbody rigid;
    scr_gameManager managerLink;
    public bool vivo;
    public Animator anim;
    public bool rebobinando;
    public float tempoReviver;
    public float tempoReviverMax;
    public bool podeInteragir;
    public LayerMask layerInt;

    private void Start()
    {
        velocidade = 23;
        rebobinando = false;
        tempoReviverMax = 2;
        tempoReviver = tempoReviverMax;
        vivo = true;
        managerLink = FindObjectOfType<scr_gameManager>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        VerificaInteracao();

        if (tempoReviver >= 0 && vivo == false && rebobinando)
        {
            tempoReviver += Time.deltaTime * 2;
            //anim.SetInteger("estado", 0);
            if (tempoReviver > 0)
            {
                tempoReviver = 2;
                vivo = true;
            }
            movimento = Vector3.zero;
        }

        if (vivo)
        {
            if (!managerLink.controlandoTempo && managerLink.comecou)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                if (h != 0 || v != 0)
                {
                    Rotacionar();
                }
                Movimentacao(h, v);
            }
        }
        else
        {
            if (tempoReviver >= 0)
            {
                tempoReviver -= Time.deltaTime;
            }
        }
        if (tempoReviver <= 0)
        {
            managerLink.GameOver();
        }
    }

    void Movimentacao(float h, float v)
    {
        movimento.Set(h, 0, v) ;
        if (h != 0 || v != 0)
        {
            anim.SetInteger("estado", 1);
        }
        else
        {
            anim.SetInteger("estado", 0);
        }
        movimento = movimento.normalized * velocidade * Time.fixedDeltaTime;

        rigid.velocity = (movimento * velocidade);

    }

    void VerificaInteracao()
    {
        RaycastHit hit;
        podeInteragir = Physics.Raycast(transform.position, transform.forward, out hit, 1.5f, layerInt);
        Debug.DrawRay(transform.position, transform.forward * 1.2f);
        if (podeInteragir)
        {
            if (Input.GetButtonDown("Jump"))
            {
                hit.transform.GetComponent<scr_gatilhoInteragivel>().InteragirObj();
            }
        }
    }

    void Rotacionar()
    {
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(
        Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 180 / Mathf.PI, 0);
    }

    public void Interagir()
    {
        anim.SetTrigger("interagir");
    }

    public void MorrerCacetete()
    {
        anim.SetInteger("estado", 4);
        vivo = false;
    }
    public void MorrerTiro()
    {
        anim.SetInteger("estado", 3);
        vivo = false;
    }

    public void MorrerEsmagado()
    {
        anim.SetInteger("estado", 5);
        vivo = false;
    }

    public void MorrerLaser()
    {
        anim.SetInteger("estado", 3);
        vivo = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "vitoria")
        {
            managerLink.Vitoria();
        }

        if (other.tag == "laser")
        {
            MorrerLaser();
        }
    }
}

