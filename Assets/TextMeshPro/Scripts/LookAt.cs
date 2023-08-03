using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    GameObject[] Player;
    bool found = false;
    void Update()
    {
        if (!found)
        {
            if (GameObject.FindGameObjectsWithTag("Player") != null)
            {
                Player = GameObject.FindGameObjectsWithTag("Player");
                found = true;
            }
        }
        if (Player[0].transform.position.x < 0 && Player[1].transform.position.x > 0)
        {
            float posX = (Player[1].transform.position.x - Player[0].transform.position.x) / 2;
            if (Player[0].transform.position.y < 0 && Player[1].transform.position.y > 0)
                this.transform.position = new Vector2(Player[1].transform.position.x - posX, (Player[1].transform.position.y + Player[0].transform.position.y) / 2 + 1);
            else if (Player[1].transform.position.y < 0 && Player[0].transform.position.y > 0)
                this.transform.position = new Vector2(Player[1].transform.position.x - posX, (Player[0].transform.position.y + Player[1].transform.position.y) / 2 + 1);
            else
                this.transform.position = new Vector2(Player[1].transform.position.x - posX, (Player[0].transform.position.y + Player[1].transform.position.y) / 2 + 1);

        }
        else if(Player[0].transform.position.x > 0 && Player[1].transform.position.x < 0)
        {
            float posX = (Player[0].transform.position.x - Player[1].transform.position.x) / 2;
            if (Player[0].transform.position.y < 0 && Player[1].transform.position.y > 0)
                this.transform.position = new Vector2(Player[0].transform.position.x - posX, (Player[1].transform.position.y + Player[0].transform.position.y) / 2 + 1);
            else if (Player[1].transform.position.y < 0 && Player[0].transform.position.y > 0)
                this.transform.position = new Vector2(Player[0].transform.position.x - posX, (Player[0].transform.position.y + Player[1].transform.position.y) / 2 + 1);
            else
                this.transform.position = new Vector2(Player[0].transform.position.x - posX, (Player[0].transform.position.y + Player[1].transform.position.y) / 2 + 1);
        }
        else
        {
            if (Player[0].transform.position.y < 0 && Player[1].transform.position.y > 0)
                this.transform.position = new Vector2((Player[0].transform.position.x + Player[1].transform.position.x) / 2, (Player[1].transform.position.y + Player[0].transform.position.y) / 2 + 1);
            else if (Player[1].transform.position.y < 0 && Player[0].transform.position.y > 0)
                this.transform.position = new Vector2((Player[0].transform.position.x + Player[1].transform.position.x) / 2, (Player[0].transform.position.y + Player[1].transform.position.y) / 2 + 1);
            else
                this.transform.position = new Vector2((Player[0].transform.position.x + Player[1].transform.position.x) / 2, (Player[0].transform.position.y + Player[1].transform.position.y) / 2 + 1);
        }
    }
}
