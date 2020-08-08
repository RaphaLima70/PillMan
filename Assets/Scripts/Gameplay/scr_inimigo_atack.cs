using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_inimigo_atack : MonoBehaviour
{
    public scr_player_mov player;
    scr_inimigo_mov movIni;

    public float fireRateIni;
    public float fireRateAtual;

    public bool atacando;
    scr_gameManager managerLink;

    // Start is called before the first frame update
    void Start()
    {
        movIni = GetComponent<scr_inimigo_mov>();
        player = FindObjectOfType<scr_player_mov>();
        managerLink = FindObjectOfType<scr_gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Atack()
    {
        managerLink.GameOver();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            atacando = true;
            Atack();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            atacando = false;

            fireRateAtual = fireRateIni;
        }
    }
}
