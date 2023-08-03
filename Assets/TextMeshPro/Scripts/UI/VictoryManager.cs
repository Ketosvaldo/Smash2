using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject[] victorias;
    void Start()
    {
        switch (PlayerPrefs.GetString("Win"))
        {
            case "MarioP1(Clone)":
            case "MarioP2(Clone)": victorias[0].SetActive(true); break;
            case "GokuP1(Clone)": 
            case "GokuP2(Clone)": victorias[1].SetActive(true); break;
        }
    }
}
