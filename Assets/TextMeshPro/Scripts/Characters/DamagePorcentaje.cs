using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePorcentaje : MonoBehaviour
{
    GameObject[] Jugador;
    GameObject Luchador;
    DañoRecibido Contador;
    Text UIContador;
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        Jugador = GameObject.FindGameObjectsWithTag("Player");
        UIContador = GetComponent<Text>();
        if (this.gameObject.tag == "P1")
        {
            switch (Jugador[x].name)
            {
                case "MarioP1(Clone)": Luchador = GameObject.Find("MarioP1(Clone)"); break;
                case "GokuP1(Clone)": Luchador = GameObject.Find("GokuP1(Clone)"); break;
            }
        }
        else 
        {
            x += 1;
            switch (Jugador[x].name)
            {
                case "MarioP2(Clone)": Luchador = GameObject.Find("MarioP2(Clone)"); break;
                case "GokuP2(Clone)": Luchador = GameObject.Find("GokuP2(Clone)"); break;
            }
        }
        Contador = Luchador.GetComponent<DañoRecibido>();
    }

    // Update is called once per frame
    void Update()
    {
        Color healthcolor = Color.Lerp(Color.white, Color.red, Contador.TotalDamage / 100);
        UIContador.color = healthcolor;
        UIContador.text = Contador.TotalDamage.ToString("f0") + "%";
    }
}
