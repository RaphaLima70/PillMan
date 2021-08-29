using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class scr_gameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool pausado;
    public bool controlandoTempo;
    public bool slowTempo;
    public float energiaMax = 12;
    public int faseAtual;

    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject howToPlay;

    public bool comecou;
    bool gameOver;

    public GameObject video;
    public GameObject GameOverSelec;
    public bool venceu;

    public bool trocandoDeCena;

    void Start()
    {
        gameOver = false;
        GameOverSelec = GameObject.Find("Retry");
        trocandoDeCena = false;
        venceu = false;
        if (GameObject.Find("winPanel") != null)
        {
            winPanel = GameObject.Find("winPanel");
            winPanel.SetActive(false);
        }

        if (GameObject.Find("HowToPlay")!= null)
        {
            comecou = false;
            howToPlay = GameObject.Find("HowToPlay");
            howToPlay.SetActive(true);
        }
        else
        {
            comecou = true;
        }
        gameOverPanel = GameObject.Find("PainelGameOver");
        pauseMenu = GameObject.Find("BGPause");
        gameOverPanel.SetActive(false);
        video.SetActive(false);
        Despausar();
    }

    void Update()
    {
        if (GameObject.Find("HowToPlay") != null)
        {
            if (Input.GetButtonDown("Jump"))
            {
                howToPlay.SetActive(false);
                Time.timeScale = 1;
                comecou = true;
            }
        }

        if (Input.GetButtonDown("Submit"))
        {
            Pausar();
        }
        if (venceu)
        {
            if (Input.GetButtonDown("Jump"))
            {
                winPanel.SetActive(false);
                MainMenu();
            }
        }
    }

    public void Vitoria()
    {
        if (faseAtual < 5)
        {
            StartCoroutine(abrirFaseCoroutine("Fase" + (faseAtual + 1)));
        }
        else
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
            venceu = true;
        }
    }

    public void GameOver()
    {      
        Time.timeScale = 0;
        comecou = false;
        if(gameOver == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameOverSelec);
            gameOver = true;
            gameOverPanel.SetActive(true);
        }       
    }

    public void Pausar()
    {
        pausado = !pausado;
        if (pausado)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Despausar();
        }        
    }
    public void Despausar()
    {
        pausado = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        StartCoroutine(abrirFaseCoroutine("MainMenu"));
    }

    public void Restart()
    {
        StartCoroutine(abrirFaseCoroutine("Fase" + faseAtual));
    }

    IEnumerator abrirFaseCoroutine(string cenaNome)
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(false);
        trocandoDeCena = true;
        pausado = false;
        video.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadSceneAsync(cenaNome);
    }

}
