using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    GameObject[] Player;
    bool found;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
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
            float distX = Player[1].transform.position.x - Player[0].transform.position.x;
            float distY;

            if (Player[0].transform.position.y < 0 && Player[1].transform.position.y > 0)
                distY = Player[1].transform.position.y - Player[0].transform.position.y;

            else if (Player[0].transform.position.y > 0 && Player[1].transform.position.y < 0)
                distY = Player[0].transform.position.y - Player[1].transform.position.y;
            else
                distY = Mathf.Abs(Player[0].transform.position.y - Player[1].transform.position.y);
            float distTotal = Mathf.Sqrt((distX * distX) + (distY * distY));
            cam.orthographicSize = CamSizeScale(DistToCamSize(distTotal));
        }
        else if (Player[0].transform.position.x > 0 && Player[1].transform.position.x < 0)
        {
            float distX = Player[0].transform.position.x - Player[1].transform.position.x;
            float distY;

            if (Player[0].transform.position.y < 0 && Player[1].transform.position.y > 0)
                distY = Player[1].transform.position.y - Player[0].transform.position.y;

            else if (Player[0].transform.position.y > 0 && Player[1].transform.position.y < 0)
                distY = Player[0].transform.position.y - Player[1].transform.position.y;
            else
                distY = Mathf.Abs(Player[0].transform.position.y - Player[1].transform.position.y);
            float distTotal = Mathf.Sqrt((distX * distX) + (distY * distY));
            cam.orthographicSize = CamSizeScale(DistToCamSize(distTotal));
        }
        else
        {
            float distX = Mathf.Abs(Player[0].transform.position.x - Player[1].transform.position.x);
            float distY;

            if (Player[0].transform.position.y < 0 && Player[1].transform.position.y > 0)
                distY = Player[1].transform.position.y - Player[0].transform.position.y;

            else if (Player[0].transform.position.y > 0 && Player[1].transform.position.y < 0)
                distY = Player[0].transform.position.y - Player[1].transform.position.y;
            else
                distY = Mathf.Abs(Player[0].transform.position.y - Player[1].transform.position.y);
            float distTotal = Mathf.Sqrt((distX * distX) + (distY * distY));
            cam.orthographicSize = CamSizeScale(DistToCamSize(distTotal));
        }
    }

    float DistToCamSize(float dist)
    {
        float distUp = 13; 
        float distDown = 3.9f;

        if (dist > distUp)
            return distUp/13;
        else if (dist < distDown)
            return distDown/13;
        else
            return dist/13;
    }

    float CamSizeScale(float percentage)
    {
        return 7.33f * percentage;
    }
}
