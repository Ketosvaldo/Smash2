using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float Damage;
    public float LaunchRate;
    DañoRecibido totalDamage;
    DañoRecibido Vida;
    ControlMario MarioP1;
    ControlMario1 MarioP2;
    ControlGoku1 GokuP1;
    ControlGoku2 GokuP2;
    Rigidbody2D rb;
    private void Start()
    {
        switch (transform.parent.name)
        {
            case "MarioP1(Clone)": MarioP1 = GetComponentInParent<ControlMario>(); break;
            case "GokuP1(Clone)": GokuP1 = GetComponentInParent<ControlGoku1>(); break;
            case "MarioP2(Clone)": MarioP2 = GetComponentInParent<ControlMario1>();break;
            case "GokuP2(Clone)": GokuP2 = GetComponentInParent<ControlGoku2>(); break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            totalDamage = other.GetComponent<DañoRecibido>();
            Vida = other.GetComponent<DañoRecibido>();
            Vida.AplicarDaño(Damage);
            rb = other.GetComponent<Rigidbody2D>();
            float fuerzaX = 1, fuerzaY = 1;
            if (this.transform.parent.localScale.x == -1)
                fuerzaX = -fuerzaX;
            //-----------//
            switch (this.transform.parent.name)
            {
                case "MarioP1(Clone)":
                    if (other.name == "MarioP2(Clone)")
                    {
                        ControlMario1 golpearPersonaje;
                        golpearPersonaje = FindObjectOfType<ControlMario1>();
                        Mario1GolpeaMario2(golpearPersonaje, fuerzaX, fuerzaY, other.gameObject);
                    }
                    else
                    {
                        ControlGoku2 golpearPersonaje;
                        golpearPersonaje = FindObjectOfType<ControlGoku2>();
                        Mario1GolpeaGoku2(golpearPersonaje, fuerzaX, fuerzaY, other.gameObject);
                    }
                    break;
                case "MarioP2(Clone)":
                    if (other.name == "MarioP1(Clone)")
                    {
                        ControlMario golpearPersonaje;
                        golpearPersonaje = FindObjectOfType<ControlMario>();
                        Mario2GolpeaMario1(golpearPersonaje, fuerzaX, fuerzaY, other.gameObject);
                    }
                    else
                    {
                        ControlGoku1 golpearPersonaje;
                        golpearPersonaje = FindObjectOfType<ControlGoku1>();
                        Mario2GolpeaGoku1(golpearPersonaje, fuerzaX, fuerzaY, other.gameObject);
                    }
                    break;
                case "GokuP1(Clone)":
                    if (other.name == "MarioP2(Clone)")
                    {
                        ControlMario1 golpearPersonaje;
                        golpearPersonaje = FindObjectOfType<ControlMario1>();
                        Goku1GolpeaMario2(golpearPersonaje, fuerzaX, fuerzaY, other.gameObject);
                    }
                    else
                    {
                        ControlGoku2 golpearPersonaje;
                        golpearPersonaje = FindObjectOfType<ControlGoku2>();
                        Goku1GolpeaGoku2(golpearPersonaje, fuerzaX, fuerzaY, other.gameObject);
                    }
                    break;
                case "GokuP2(Clone)":
                    if (other.name == "MarioP1(Clone)")
                    {
                        ControlMario golpearPersonaje;
                        golpearPersonaje = FindObjectOfType<ControlMario>();
                        Goku2GolpeaMario1(golpearPersonaje, fuerzaX, fuerzaY, other.gameObject);
                    }
                    else
                    {
                        ControlGoku1 golpearPersonaje;
                        golpearPersonaje = FindObjectOfType<ControlGoku1>();
                        Goku2GolpeaGoku1(golpearPersonaje, fuerzaX, fuerzaY, other.gameObject);
                    }
                    break;
            }
        }
    }

    void Mario1GolpeaMario2(ControlMario1 stun, float fuerzaX, float fuerzaY, GameObject other)
    {
        bool lanzamiento = true;
        switch (MarioP1.currentState)
        {
            case "MarioDownTilt": fuerzaX *= 5; fuerzaY = 30; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 3; break;
            case "MarioDownAir": fuerzaX *= 20; fuerzaY = 200; lanzamiento = false; stun.activadorStun = true; stun.golpe = 2; stun.stun = .3f; break;
            case "MarioBackAir": fuerzaX *= -60; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "MarioForwardTilt": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "MarioUpTilt": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "MarioUpAir": fuerzaX *= 5; fuerzaY = 30; stun.golpe = 3; stun.activadorStun = true; stun.stun = .3f; break;
            case "MarioForwardAir": fuerzaX *= 0; fuerzaY = -60; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Mario_SideB": other.transform.localScale = new Vector2(-other.transform.localScale.x, other.transform.localScale.y); break;
            case "MarioDownB": other.transform.position = this.transform.position; stun.golpe = 2; stun.activadorStun = true; stun.stun = .15f; break;
            case "MarioRecovery": lanzamiento = false; other.transform.position = new Vector2(this.transform.position.x + .5f, this.transform.position.y + .1f); stun.activadorStun = true; stun.golpe = 2; stun.stun = .15f; fuerzaX = 10; fuerzaY = 10; break;
            case "MarioTilt": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.stun = .7f; stun.golpe = 1; stun.activadorStun = true; break;
            case "MarioTilt2": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 2; break;
            case "MarioTilt3": fuerzaX *= 50; fuerzaY = 10; stun.golpe = 3; stun.stun = 1f; break;
            case "MarioNair": fuerzaX *= 20; fuerzaY = 15; stun.golpe = 3; stun.stun = .3f; stun.activadorStun = true; break;
        }
        if (lanzamiento)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY) * totalDamage.TotalDamage * LaunchRate);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }
    void Mario1GolpeaGoku2(ControlGoku2 stun, float fuerzaX, float fuerzaY, GameObject other)
    {
        bool lanzamiento = true;
        switch (MarioP1.currentState)
        {
            case "MarioDownTilt": fuerzaX *= 5; fuerzaY = 30; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 3; break;
            case "MarioDownAir": fuerzaX *= 20; fuerzaY = 200; lanzamiento = false; stun.activadorStun = true; stun.golpe = 2; stun.stun = .3f; break;
            case "MarioBackAir": fuerzaX *= -60; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "MarioForwardTilt": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "MarioUpTilt": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "MarioUpAir": fuerzaX *= 5; fuerzaY = 30; stun.golpe = 3; stun.activadorStun = true; stun.stun = .3f; break;
            case "MarioForwardAir": fuerzaX *= 0; fuerzaY = -60; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Mario_SideB": other.transform.localScale = new Vector2(-other.transform.localScale.x, other.transform.localScale.y); break;
            case "MarioDownB": other.transform.position = this.transform.position; stun.golpe = 2; stun.activadorStun = true; stun.stun = .15f; break;
            case "MarioRecovery": lanzamiento = false; other.transform.position = new Vector2(this.transform.position.x + .5f, this.transform.position.y + .1f); stun.activadorStun = true; stun.golpe = 2; stun.stun = .15f; fuerzaX = 10; fuerzaY = 10; break;
            case "MarioTilt": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.stun = .7f; stun.golpe = 1; stun.activadorStun = true; break;
            case "MarioTilt2": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 2; break;
            case "MarioTilt3": fuerzaX *= 50; fuerzaY = 10; stun.golpe = 3; stun.stun = 1f; break;
            case "MarioNair": fuerzaX *= 20; fuerzaY = 15; stun.golpe = 3; stun.stun = .3f; stun.activadorStun = true; break;
        }
        if (lanzamiento)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY) * totalDamage.TotalDamage * LaunchRate);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }
    void Mario2GolpeaMario1(ControlMario stun, float fuerzaX, float fuerzaY, GameObject other)
    {
        bool lanzamiento = true;
        switch (MarioP2.currentState)
        {
            case "MarioDownTilt": fuerzaX *= 5; fuerzaY = 30; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 3; break;
            case "MarioDownAir": fuerzaX *= 20; fuerzaY = 200; lanzamiento = false; stun.activadorStun = true; stun.golpe = 2; stun.stun = .3f; break;
            case "MarioBackAir": fuerzaX *= -60; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "MarioForwardTilt": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "MarioUpTilt": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "MarioUpAir": fuerzaX *= 5; fuerzaY = 30; stun.golpe = 3; stun.activadorStun = true; stun.stun = .3f; break;
            case "MarioForwardAir": fuerzaX *= 0; fuerzaY = -60; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Mario_SideB": other.transform.localScale = new Vector2(-other.transform.localScale.x, other.transform.localScale.y); break;
            case "MarioDownB": other.transform.position = this.transform.position; stun.golpe = 2; stun.activadorStun = true; stun.stun = .15f; break;
            case "MarioRecovery": lanzamiento = false; other.transform.position = new Vector2(this.transform.position.x + .5f, this.transform.position.y + .1f); stun.activadorStun = true; stun.golpe = 2; stun.stun = .15f; fuerzaX = 10; fuerzaY = 10; break;
            case "MarioTilt": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.stun = .7f; stun.golpe = 1; stun.activadorStun = true; break;
            case "MarioTilt2": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 2; break;
            case "MarioTilt3": fuerzaX *= 50; fuerzaY = 10; stun.golpe = 3; stun.stun = 1f; break;
            case "MarioNair": fuerzaX *= 20; fuerzaY = 15; stun.golpe = 3; stun.stun = .3f; stun.activadorStun = true; break;
        }
        if (lanzamiento)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY) * totalDamage.TotalDamage * LaunchRate);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }
    void Mario2GolpeaGoku1(ControlGoku1 stun, float fuerzaX, float fuerzaY, GameObject other)
    {
        bool lanzamiento = true;
        switch (MarioP2.currentState)
        {
            case "MarioDownTilt": fuerzaX *= 5; fuerzaY = 30; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 3; break;
            case "MarioDownAir": fuerzaX *= 20; fuerzaY = 200; lanzamiento = false; stun.activadorStun = true; stun.golpe = 2; stun.stun = .3f; break;
            case "MarioBackAir": fuerzaX *= -60; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "MarioForwardTilt": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "MarioUpTilt": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "MarioUpAir": fuerzaX *= 5; fuerzaY = 30; stun.golpe = 3; stun.activadorStun = true; stun.stun = .3f; break;
            case "MarioForwardAir": fuerzaX *= 0; fuerzaY = -60; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Mario_SideB": other.transform.localScale = new Vector2(-other.transform.localScale.x, other.transform.localScale.y); break;
            case "MarioDownB": other.transform.position = this.transform.position; stun.golpe = 2; stun.activadorStun = true; stun.stun = .15f; break;
            case "MarioRecovery": lanzamiento = false; other.transform.position = new Vector2(this.transform.position.x + .5f, this.transform.position.y + .1f); stun.activadorStun = true; stun.golpe = 2; stun.stun = .15f; fuerzaX = 10; fuerzaY = 10; break;
            case "MarioTilt": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.stun = .7f; stun.golpe = 1; stun.activadorStun = true; break;
            case "MarioTilt2": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 2; break;
            case "MarioTilt3": fuerzaX *= 50; fuerzaY = 10; stun.golpe = 3; stun.stun = 1f; break;
            case "MarioNair": fuerzaX *= 20; fuerzaY = 15; stun.golpe = 3; stun.stun = .3f; stun.activadorStun = true; break;
        }
        if (lanzamiento)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY) * totalDamage.TotalDamage * LaunchRate);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }

    void Goku1GolpeaMario2(ControlMario1 stun, float fuerzaX, float fuerzaY, GameObject other)
    {
        bool lanzamiento = true;
        switch (GokuP1.currentState)
        {
            case "Goku_DownTilt": fuerzaX *= 5; fuerzaY = 30; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 3; break;
            case "Goku_DownAir": fuerzaX *= 20; fuerzaY = 200; lanzamiento = false; stun.activadorStun = true; stun.golpe = 2; stun.stun = .3f; break;
            case "Gouk_BackAir": fuerzaX *= -60; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Goku_ForwardTilt": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "Goku_UpTilt": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_UpAir": fuerzaX *= 5; fuerzaY = 30; stun.golpe = 3; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_ForwardAir": fuerzaX *= 0; fuerzaY = -60; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Goku_SideB": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;            case "Goku_Recovery": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_Jab1": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.stun = .7f; stun.golpe = 1; stun.activadorStun = true; break;
            case "Goku_Jab2": fuerzaX *= 50; fuerzaY = 10; stun.golpe = 3; stun.stun = 1f; break;
            case "Goku_Nair": fuerzaX *= 20; fuerzaY = 15; stun.golpe = 3; stun.stun = .3f; stun.activadorStun = true; break;
        }
        if (lanzamiento)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY) * totalDamage.TotalDamage * LaunchRate);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }
    void Goku1GolpeaGoku2(ControlGoku2 stun, float fuerzaX, float fuerzaY, GameObject other)
    {
        bool lanzamiento = true;
        switch (GokuP1.currentState)
        {
            case "Goku_DownTilt": fuerzaX *= 5; fuerzaY = 30; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 3; break;
            case "Goku_DownAir": fuerzaX *= 20; fuerzaY = 200; lanzamiento = false; stun.activadorStun = true; stun.golpe = 2; stun.stun = .3f; break;
            case "Gouk_BackAir": fuerzaX *= -60; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Goku_ForwardTilt": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "Goku_UpTilt": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_UpAir": fuerzaX *= 5; fuerzaY = 30; stun.golpe = 3; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_ForwardAir": fuerzaX *= 0; fuerzaY = -60; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Goku_SideB": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "Goku_Recovery": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_Jab1": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.stun = .7f; stun.golpe = 1; stun.activadorStun = true; break;
            case "Goku_Jab2": fuerzaX *= 50; fuerzaY = 10; stun.golpe = 3; stun.stun = 1f; break;
            case "Goku_Nair": fuerzaX *= 20; fuerzaY = 15; stun.golpe = 3; stun.stun = .3f; stun.activadorStun = true; break;
        }
        if (lanzamiento)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY) * totalDamage.TotalDamage * LaunchRate);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }
    void Goku2GolpeaMario1(ControlMario stun, float fuerzaX, float fuerzaY, GameObject other)
    {
        bool lanzamiento = true;
        switch (GokuP2.currentState)
        {
            case "Goku_DownTilt": fuerzaX *= 5; fuerzaY = 30; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 3; break;
            case "Goku_DownAir": fuerzaX *= 20; fuerzaY = 200; lanzamiento = false; stun.activadorStun = true; stun.golpe = 2; stun.stun = .3f; break;
            case "Gouk_BackAir": fuerzaX *= -60; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Goku_ForwardTilt": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "Goku_UpTilt": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_UpAir": fuerzaX *= 5; fuerzaY = 30; stun.golpe = 3; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_ForwardAir": fuerzaX *= 0; fuerzaY = -60; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Goku_SideB": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "Goku_Recovery": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_Jab1": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.stun = .7f; stun.golpe = 1; stun.activadorStun = true; break;
            case "Goku_Jab2": fuerzaX *= 50; fuerzaY = 10; stun.golpe = 3; stun.stun = 1f; break;
            case "Goku_Nair": fuerzaX *= 20; fuerzaY = 15; stun.golpe = 3; stun.stun = .3f; stun.activadorStun = true; break;
        }
        if (lanzamiento)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY) * totalDamage.TotalDamage * LaunchRate);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }
    void Goku2GolpeaGoku1(ControlGoku1 stun, float fuerzaX, float fuerzaY, GameObject other)
    {
        bool lanzamiento = true;
        switch (GokuP2.currentState)
        {
            case "Goku_DownTilt": fuerzaX *= 5; fuerzaY = 30; stun.activadorStun = true; stun.stun = .7f; stun.golpe = 3; break;
            case "Goku_DownAir": fuerzaX *= 20; fuerzaY = 200; lanzamiento = false; stun.activadorStun = true; stun.golpe = 2; stun.stun = .3f; break;
            case "Gouk_BackAir": fuerzaX *= -60; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Goku_ForwardTilt": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "Goku_UpTilt": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_UpAir": fuerzaX *= 5; fuerzaY = 30; stun.golpe = 3; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_ForwardAir": fuerzaX *= 0; fuerzaY = -60; stun.golpe = 3; stun.activadorStun = true; stun.stun = .5f; break;
            case "Goku_SideB": fuerzaX *= 30; fuerzaY = 20; stun.golpe = 3; stun.activadorStun = true; stun.stun = .8f; break;
            case "Goku_Recovery": fuerzaX *= 5; fuerzaY = 40; stun.golpe = 2; stun.activadorStun = true; stun.stun = .3f; break;
            case "Goku_Jab1": fuerzaX *= 20; fuerzaY = 150; lanzamiento = false; stun.stun = .7f; stun.golpe = 1; stun.activadorStun = true; break;
            case "Goku_Jab2": fuerzaX *= 50; fuerzaY = 10; stun.golpe = 3; stun.stun = 1f; break;
            case "Goku_Nair": fuerzaX *= 20; fuerzaY = 15; stun.golpe = 3; stun.stun = .3f; stun.activadorStun = true; break;
        }
        if (lanzamiento)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY) * totalDamage.TotalDamage * LaunchRate);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(fuerzaX, fuerzaY));
        }
    }

}
