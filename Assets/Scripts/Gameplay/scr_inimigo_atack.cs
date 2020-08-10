using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class scr_inimigo_atack : MonoBehaviour
{
    public bool isRanged;
    scr_player_mov playerLink;
    public float alcance;
    scr_gameManager managerLink;
    public bool vendoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerLink = FindObjectOfType<scr_player_mov>();
        managerLink = FindObjectOfType<scr_gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Atack()
    {
        if (vendoPlayer)
        {
            if (isRanged)
            {

                playerLink.MorrerTiro();
            }
            else
            {
                playerLink.MorrerCacetete();
            }
        }
    }
}
