using UnityEngine;
using UnityEngine.EventSystems;

public class scr_selecBotao : MonoBehaviour
{
    public GameObject proxSelecionado;
    public scr_gerenSom linkSom;

    private void Start()
    {
        linkSom = FindObjectOfType<scr_gerenSom>();
    }
    public void apertarBotao()
    {
        linkSom.TocarEfeito(0);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(proxSelecionado);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }
}
