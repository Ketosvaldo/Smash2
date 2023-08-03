using UnityEngine;

public class DañoRecibido : MonoBehaviour
{
    public float TotalDamage;
    public int vidas;
    // Start is called before the first frame update
    void Start()
    {
        vidas = 3;
        TotalDamage = 0;
    }

    public void AplicarDaño(float vida)
    {
        TotalDamage += vida;
    }
}
