using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class scr_mainMenu : MonoBehaviour
{
    public Animator anim;
    public GameObject painelHome;
    public GameObject painelAbout;
    public GameObject painelFases;
    public GameObject canvas;
    public VideoPlayer video;

    private void Start()
    {
        anim.gameObject.SetActive(true);
        FechaMenus();
        painelHome.SetActive(true);
        anim.SetBool("abriu", true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void AbrirMenu(GameObject menuAbrindo)
    {
        StartCoroutine(abrirMenu(menuAbrindo));
    }

    IEnumerator abrirMenu(GameObject menuAbrindo)
    {
        anim.SetBool("abriu",false);
        yield return new WaitForSeconds(1);
        FechaMenus();
        menuAbrindo.SetActive(true);
        anim.SetBool("abriu",true);
    }

    IEnumerator abrirFaseCoroutine(string cenaNome)
    {
        canvas.SetActive(false);
        video.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(cenaNome);
    }

    public void AbrirFase(string cenaNome)
    {
        StartCoroutine(abrirFaseCoroutine(cenaNome));
    }

    public void FechaMenus()
    {
        painelAbout.SetActive(false);
        painelFases.SetActive(false);
        painelHome.SetActive(false);
    }
}
