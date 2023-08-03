using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string nombreNivel;
    bool Player1, Player2;
    private void Start()
    {
        Player1 = false;
        Player2 = false;
    }
    void Update()
    {
        if (Player1 && !Player2)
        {
            StartCoroutine(espera());
        }
        if (Player2)
        {
            StartCoroutine(esperaCargarNivel());
        }
    }

    IEnumerator esperaCargarNivel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nombreNivel);
    }    
    IEnumerator espera()
    {
        yield return new WaitForSeconds(2f);
    }

    public void CargarNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }

    public void SeleccionarPersonaje(string Personaje)
    {
        if (!Player1)
        {
            Player1 = true;
            PlayerPrefs.SetString("Player1", Personaje);
        }
        else
        {
            Player2 = true;
            PlayerPrefs.SetString("Player2", Personaje);
        }
    }

    public void salir()
    {
        Application.Quit();
    }
}
