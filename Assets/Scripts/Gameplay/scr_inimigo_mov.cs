using Chronos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class scr_inimigo_mov : MonoBehaviour
{
    public Transform playerT;
    public Transform[] posicoes;
    Timeline time;
    scr_campoDeVisao campoLink;
    scr_player_mov playerLink;
    Animator anim;
    public bool morreu;
    public bool patrulheiro;
    public bool podeAndar;

    public int contaPosiAtual = 0;
    public float delayInicial;
    public float delay;
    public float tempoPerdendoPlayer;
    public float alcance;
    public int posiAtual;

    scr_rewindControll rewindLink;

    NavMeshAgent nav;

    private void Start()
    {
        rewindLink = FindObjectOfType<scr_rewindControll>();
        morreu = false;
        delay = delayInicial;
        time = GetComponent<Timeline>();
        anim = GetComponent<Animator>();
        playerLink = FindObjectOfType<scr_player_mov>();
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        campoLink = GetComponent<scr_campoDeVisao>();
        nav = GetComponent<NavMeshAgent>();
        podeAndar = true;
    }

    void Update()
    {
        if (!morreu)
        {
            GetComponent<CapsuleCollider>().enabled = true;
           
            Procurar();
        }
        else
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<scr_campoDeVisao>().viewMeshFilter.mesh = null;
            nav.Stop();
        }
    }

    void Procurar()
    {
        if (campoLink.visibleTargets.Count > 0 || tempoPerdendoPlayer > 0)
        {
            if (campoLink.visibleTargets.Count <= 0)
            {
                campoLink.viewMeshFilter.gameObject.SetActive(true);
                tempoPerdendoPlayer -= Time.deltaTime;
            }
            else
            {
                campoLink.viewMeshFilter.gameObject.SetActive(false);
                tempoPerdendoPlayer = delayInicial;
            }
            if (playerLink.vivo)
            {               
                SeguirPlayer();
            }
            else
            {
                tempoPerdendoPlayer = 0;
            }
            
        }
        else
        {
            if (patrulheiro)
            {
                Patrulhar();
            }
        }
    }

    void Patrulhar()
    {
        nav.SetDestination(posicoes[contaPosiAtual].position);
        
        if (Vector3.Distance(transform.position, posicoes[contaPosiAtual].position) < 2f)
        {
            if (delay <= 0)
            {
                do
                {
                    contaPosiAtual = Random.Range(0, posicoes.Length);

                } while (contaPosiAtual == posiAtual);
                posiAtual = contaPosiAtual;
                delay = delayInicial;
            }
            else
            {
                anim.SetInteger("estado", 0);
                delay -= Time.deltaTime;
            }
        }
        else
        {
            anim.SetInteger("estado", 1);
        }

    }

    void SeguirPlayer()
    {
        if (Vector3.Distance(transform.position, playerT.position) < alcance)
        {
            playerLink.MorrerCacetete();
            anim.SetInteger("estado", 3);
            nav.Stop();
        }
        else
        {
            anim.SetInteger("estado", 2);
            nav.Resume();
            nav.SetDestination(playerT.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "porta")
        {
            tempoPerdendoPlayer = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "morte")
        {
            morreu = true;
            anim.SetInteger("estado", 4);
        }

    }
}