using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarConTiempo : MonoBehaviour
{
    [SerializeField]
    GameObject ObjectoActivar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(xd());
    }

    IEnumerator xd()
    {
        yield return new WaitForSeconds(1f);
        ObjectoActivar.SetActive(true);
    }
}
