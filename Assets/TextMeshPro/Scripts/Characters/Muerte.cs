using System.Collections;
using UnityEngine;

public class Muerte : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer imagen;
    CapsuleCollider2D colision;
    DañoRecibido vida;
    [SerializeField]
    GameObject Explosion;
    [SerializeField]
    GameObject PlataformaRespawn;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        imagen = GetComponent<SpriteRenderer>();
        colision = GetComponent<CapsuleCollider2D>();
        vida = GetComponent<DañoRecibido>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            vida.vidas -= 1;
            switch (collision.gameObject.name)
            {
                case "MuerteA": Instantiate(Explosion, this.transform.position, Quaternion.Euler(0,0,180)); break;
                case "MuerteI": Instantiate(Explosion, this.transform.position, Quaternion.Euler(0, 0, -90)); break;
                case "MuerteD": Instantiate(Explosion, this.transform.position, Quaternion.Euler(0, 0, 90)); break;
                case "MuerteB": Instantiate(Explosion, this.transform.position, Quaternion.identity); break;
            }
            StartCoroutine(espera());
        }
    }
    IEnumerator espera()
    {
        colision.enabled = false;
        imagen.enabled = false;
        vida.TotalDamage = 0;
        yield return new WaitForSeconds(2f);
        Instantiate(PlataformaRespawn);
        rb.velocity = new Vector2(0, 0);
        colision.enabled = true;
        imagen.enabled = true;
        this.gameObject.SetActive(true);
        if(PlataformaRespawn.name == "PlataformaDeRespawnP1")
            this.gameObject.transform.position = new Vector2(-4.55f,3.5f);       
        else
            this.gameObject.transform.position = new Vector2(3.75f,3.5f);
    }
}
