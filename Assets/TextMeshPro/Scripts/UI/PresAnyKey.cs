using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresAnyKey : MonoBehaviour
{
    bool xd;
    // Start is called before the first frame update
    void Start()
    {
        xd = false;
        StartCoroutine(espera());
    }

    // Update is called once per frame
    void Update()
    {
        if (xd)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(2f);
        xd = true;
    }
}
