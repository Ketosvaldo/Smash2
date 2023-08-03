using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] CharacterPrefabs;
    public GameObject[] Spawners;
    public GameObject Loading;
    GameObject[] jugadores;
    DañoRecibido P1;
    DañoRecibido P2;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("Player1") == "Mario")
        {
            Instantiate(CharacterPrefabs[0], new Vector2(-3.36f, 0.43f), Quaternion.identity);
        }
        else
            Instantiate(CharacterPrefabs[2], new Vector2(-3.36f, 0.43f), Quaternion.identity);
        if (PlayerPrefs.GetString("Player2") == "Goku")
        {
            Instantiate(CharacterPrefabs[3], new Vector2(3.36f, 0.43f), Quaternion.identity);
        }
        else
            Instantiate(CharacterPrefabs[1], new Vector2(3.36f, 0.43f), Quaternion.identity);
        jugadores = GameObject.FindGameObjectsWithTag("Player");
        P1 = jugadores[0].GetComponent<DañoRecibido>();
        P2 = jugadores[1].GetComponent<DañoRecibido>();
        Application.targetFrameRate = 60;
        StartCoroutine(Espera());
    }
    private void Update()
    {
        if(P1.vidas <= 0)
        {
            switch (P2.name)
            {
                case "MarioP2(Clone)": PlayerPrefs.SetString("Win", "MarioP2(Clone)"); break;
                case "GokuP2(Clone)": PlayerPrefs.SetString("Win", "GokuP2(Clone)"); break;
            }
            SceneManager.LoadScene("Victory");
        }
        if(P2.vidas <= 0)
        {
            switch (P1.name)
            {
                case "MarioP1(Clone)": PlayerPrefs.SetString("Win", "MarioP1(Clone)"); break;
                case "GokuP1(Clone)": PlayerPrefs.SetString("Win", "GokuP1(Clone)"); break;
            }
            SceneManager.LoadScene("Victory");
        }
    }
    IEnumerator Espera()
    {
        yield return new WaitForSeconds(2f);
        Loading.SetActive(false);
    }
}
