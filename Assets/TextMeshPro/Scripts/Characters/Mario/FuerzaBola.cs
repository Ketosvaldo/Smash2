using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuerzaBola : MonoBehaviour
{
    GameObject Jugador;
    public bool P1;
    CircleCollider2D colider;
    DañoRecibido da;
    int contador = 0;
    Rigidbody2D rb;
    Rigidbody2D enemie;
    float tiempo;
    // Start is called before the first frame update
    void Start()
    {
        tiempo = 0;
        contador = 0;
        colider = GetComponent<CircleCollider2D>();
        colider.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        Invoke("darFuerza", .25f);
        Invoke("HabilitarColision", .5f);
    }

    void darFuerza()
    {
        if (P1)
        {
            if (GameObject.Find("MarioP1(Clone)"))
                Jugador = GameObject.Find("MarioP1(Clone)");
            else
                Jugador = GameObject.Find("GokuP1(Clone)");
            rb.velocity = new Vector2(6 * Jugador.transform.localScale.x, rb.velocity.y);
        }
        else
        {
            if (GameObject.Find("MarioP2(Clone)"))
                Jugador = GameObject.Find("MarioP2(Clone)");
            else
                Jugador = GameObject.Find("GokuP2(Clone)");
            rb.velocity = new Vector2(6 * Jugador.transform.localScale.x, rb.velocity.y);
        }
    }

    void HabilitarColision()
    {
        colider.enabled = true;
        rb.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (contador > 4)
            Destroy(gameObject);
        if(collision.gameObject.tag == "Player")
        {
            enemie = collision.gameObject.GetComponent<Rigidbody2D>();
            da = collision.gameObject.GetComponent<DañoRecibido>();
            enemie.AddForce(new Vector2(100, 200));
            da.TotalDamage += 5;
            Destroy(gameObject);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,150));
            contador += 1;
        }
    }

    private void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo > 5)
            Destroy(gameObject);
    }
}
