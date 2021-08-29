using Chronos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class scr_inimigo_mov : MonoBehaviour
{
    public Transform playerT;
    public Transform[] posicoes;


    scr_campoDeVisao campoLink;
    scr_player_mov playerLink;
    scr_inimigo_atack atackLink;

    Animator anim;
    public bool morreu;
    public bool patrulheiro;
    public bool podeAndar;

    public int achou;
    public float velocidade;

    public int contaPosiAtual = 0;
    public float delayInicial;
    public float delay;
    public float tempoPerdendoPlayer;
    public int posiAtual;

    scr_rewindControll rewindLink;

    NavMeshAgent nav;

    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        rewindLink = FindObjectOfType<scr_rewindControll>();
        playerLink = FindObjectOfType<scr_player_mov>();
        campoLink = GetComponent<scr_campoDeVisao>();
        atackLink = GetComponent<scr_inimigo_atack>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        delay = delayInicial;
        podeAndar = true;
        morreu = false;
    }

    void Update()
    {
        if (achou > 0)
        {
            atackLink.vendoPlayer = true;
        }
        else
        {
            atackLink.vendoPlayer = false;
        }


        if (!morreu)
        {
            GetComponent<CapsuleCollider>().enabled = true;

            if (rewindLink.rebobinando)
            {
                achou = 0;
                tempoPerdendoPlayer = 0;
            }
            else
            {
                achou = campoLink.visibleTargets.Count;
                Procurar();
            }
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
        if (achou > 0 || tempoPerdendoPlayer > 0)
        {
            if (achou <= 0)
            {
                campoLink.viewMeshFilter.gameObject.SetActive(true);
                tempoPerdendoPlayer -= Time.deltaTime;
            }
            else
            {
                campoLink.viewMeshFilter.gameObject.SetActive(false);
                tempoPerdendoPlayer = delayInicial;
                transform.LookAt(playerT);
            }
            if (playerLink.vivo)
            {
                SeguirPlayer();
            }
            else
            {
                //animacao rindo
                tempoPerdendoPlayer = 0;
            }

        }
        else
        {
            campoLink.viewMeshFilter.gameObject.SetActive(true);
            if (patrulheiro)
            {
                Patrulhar(); 
            }
            else
            {
                if (Vector3.Distance(transform.position, posicoes[contaPosiAtual].position) > 1)
                {
                    nav.SetDestination(posicoes[0].position);
                }
                else
                {
                    transform.LookAt(posicoes[1].position);
                }
            }
        }
    }

    void Patrulhar()
    {
        nav.speed = 5;
        nav.SetDestination(posicoes[contaPosiAtual].position);
        if (Vector3.Distance(transform.position, posicoes[contaPosiAtual].position) < 2)
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
            nav.Resume();
            anim.SetInteger("estado", 1);
        }
    }

    void SeguirPlayer()
    {
        if (Vector3.Distance(transform.position, playerT.position) < atackLink.alcance)
        {
            atackLink.Atack();
            anim.SetInteger("estado", 3);
            nav.Stop();
        }
        else
        {
            anim.SetInteger("estado", 2);
            nav.Resume();
            if (!rewindLink.managerLink.slowTempo)
            {
                nav.speed = velocidade;
            }
            else
            {
                Debug.Log("slow");
            }
            nav.SetDestination(playerT.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "porta")
        {
            tempoPerdendoPlayer = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "morte")
        {
            morreu = true;
            anim.SetInteger("estado", 4);
        }
    }
}