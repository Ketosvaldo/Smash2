using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMario : MonoBehaviour
{
    PauseMenu pauseMenu;
    FuerzaBola seleccionarPlayer;
    [SerializeField]
    GameObject BolaDeFuego;
    GameObject spawnBola;
    public bool activadorStun;
    public float stun;
    public int golpe;
    private float espera;
    bool xd = false; //temporizador jab
    //Variables de fisicas
    bool isIntro;
    public bool isGround;
    bool dobleSalto;
    public bool isRecovery;
    Rigidbody2D rb;
    //Variables de movimiento
    public bool puedoMoverme;
    bool correr;
    public float velocidad, salto, recovery;
    //Variables de animator
    public Animator anim;
    //Variables de ataque
    public int idAtaque;
    public bool isAttacking;
    public float jabTiempo;
    float attackDelay;
    //----------------------------//
    public string currentState;
    const string Player_caminar = "MarioWalk";
    const string Player_correr = "MarioRun";
    const string Player_idle = "MarioIdle";
    const string Player_jump = "MarioJump";
    const string Player_DoubleJump = "MarioJump0";
    const string Player_Falling = "MarioFalling";
    const string Player_Intro = "MarioIntro";
    const string Player_Hit2 = "MarioHit2";
    const string Player_Hit3 = "MarioHit3";
    //Animaciones de ataque
    const string Player_DownAir = "MarioDownAir";
    const string Player_BackAir = "MarioBackAir";
    const string Player_Nair = "MarioNair";
    const string Player_FowardTilt = "MarioForwardTilt";
    const string Player_UpTilt = "MarioUpTilt";
    const string Player_UpAir = "MarioUpAir";
    const string Player_ForwardAir = "MarioForwardAir";
    const string Player_DownTilt = "MarioDownTilt";
    const string Player_B = "MarioSpecialB";
    const string Player_SideB = "Mario_SideB";
    const string Player_DownB = "MarioDownB";
    const string Player_UpB = "MarioRecovery";
    const string Player_Jab1 = "MarioTilt";
    const string Player_Jab2 = "MarioTilt2";
    const string Player_Jab3 = "MarioTilt3";
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        spawnBola = GameObject.Find("BolaSpawnP1");
        activadorStun = false;
        stun = 0;
        golpe = 0;
        espera = 0;
        idAtaque = 0;
        attackDelay = .5f;
        isIntro = true;
        isAttacking = false;
        puedoMoverme = true;
        isRecovery = true;
        jabTiempo = 0;
        //Valores por defecto
        correr = false;
        isGround = true;
        dobleSalto = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isAttacking = false;
            puedoMoverme = true;
        }
        if (espera <= 2)
        {
            espera += Time.deltaTime;
        }
        else
        {
            if (isIntro)
            {
                ChangeAnimationState(Player_Intro);
                Invoke("IntroComplete", 1f);
            }
            if (rb.velocity.y < -6.5 && stun <= 0)
                rb.velocity = new Vector2(rb.velocity.x, -6.5f);
            //Movimiento
            if (rb.velocity.x > -8 && rb.velocity.x < 8 && !activadorStun && !pauseMenu.GameIsPaused)
                moverse();
            else
            {
                recuperarControl();
            }
            //Salto
            Salto();
            //Ataques
            ataquesNormales();
            ataquesEspeciales();
            switch (currentState)
            {
                case Player_Jab1: if (jabTiempo >= 0.4) { isAttacking = false; xd = false; ChangeAnimationState(Player_idle); jabTiempo = 0; } break;
                case Player_Jab2: if (jabTiempo >= 0.8) { isAttacking = false; xd = false; ChangeAnimationState(Player_idle); jabTiempo = 0; } break;
                case Player_Jab3: if (jabTiempo >= 1.2) { isAttacking = false; xd = false; ChangeAnimationState(Player_idle); jabTiempo = 0; } break;
            }
            if (xd)
            {
                jabTiempo += Time.deltaTime;
            }
            if (activadorStun)
            {
                stun -= Time.deltaTime;
            }
            if (stun <= 0)
                activadorStun = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Piso")
        {
            rb.velocity = new Vector2(0, 0);
            isGround = true;
            dobleSalto = true;
            isRecovery = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Piso")
        {
            isGround = false;
        }
    }

    void ataquesNormales()
    {
        bool ataco = Input.GetKeyDown(KeyCode.P);
        if (ataco && !isAttacking)
        {
            //Movimientos de ataque si me encuentro en el suelo
            if (isGround && isRecovery)
            {
                isAttacking = true;
                puedoMoverme = false;
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    ChangeAnimationState(Player_FowardTilt);
                    attackDelay = .5f;
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    ChangeAnimationState(Player_UpTilt);
                    attackDelay = .5f;

                }
                else if (Input.GetKey(KeyCode.S))
                {
                    ChangeAnimationState(Player_DownTilt);
                    attackDelay = .5f;
                }
                else
                {
                    ChangeAnimationState(Player_Jab1);
                    xd = true;
                }
            }
            else if (!isAttacking && isRecovery)
            {
                isAttacking = true;
                if (Input.GetKey(KeyCode.D))
                {
                    //BackAir
                    if (this.transform.localScale.x < 0)
                    {
                        ChangeAnimationState(Player_BackAir);
                        attackDelay = .6f;
                    }
                    else
                    {
                        ChangeAnimationState(Player_ForwardAir); //ForwardAir
                        attackDelay = 1.1f;
                    }
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    //BackAir
                    if (this.transform.localScale.x > 0)
                    {
                        ChangeAnimationState(Player_BackAir);
                        attackDelay = .52f;
                    }
                    else
                    {
                        ChangeAnimationState(Player_ForwardAir); //ForwardAir
                        attackDelay = 1.1f;
                    }
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    ChangeAnimationState(Player_UpAir); //UpAir
                    attackDelay = .6f;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    attackDelay = .73f;
                    ChangeAnimationState(Player_DownAir); //DownAir
                }
                else
                {
                    attackDelay = .73f;
                    ChangeAnimationState(Player_Nair); //Nair
                }
            }
            switch (currentState)
            {
                case Player_Jab1: break;
                case Player_Jab2: break;
                case Player_Jab3: break;
                default: Invoke("AttackComplete", attackDelay); break;
            }
        }
        else if (ataco && xd)
        {
            switch (currentState)
            {
                case Player_Jab1: ChangeAnimationState(Player_Jab2); break;
                case Player_Jab2: ChangeAnimationState(Player_Jab3); break;
            }
        }
    }

    void ataquesEspeciales()
    {
        bool ataco = Input.GetKeyDown(KeyCode.O);
        if (ataco && isRecovery && !isAttacking)
        {
            //Movimientos de ataque si me encuentro en el suelo
            if (isGround)
            {
                isAttacking = true;
                puedoMoverme = false;
                if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
                {
                    ChangeAnimationState(Player_SideB);
                    attackDelay = .8f;
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    rb.velocity = new Vector2(0f, 0f);
                    rb.AddForce(new Vector2(200 * this.transform.localScale.x, recovery));
                    stun = 1f;
                    activadorStun = true;
                    golpe = 0;
                    ChangeAnimationState(Player_UpB);
                    isRecovery = false;
                    dobleSalto = false;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    attackDelay = .65f;
                    ChangeAnimationState(Player_DownB);
                }
                else if (Input.GetKey(KeyCode.O))
                {
                    attackDelay = .68f;
                    ChangeAnimationState(Player_B);
                    Instantiate(BolaDeFuego, spawnBola.transform.position, spawnBola.transform.rotation);
                    seleccionarPlayer = BolaDeFuego.GetComponent<FuerzaBola>();
                    seleccionarPlayer.P1 = true;
                }
            }
            else if (!isAttacking && isRecovery)
            {
                isAttacking = true;
                if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
                {
                    //BackAir
                    if (this.transform.localScale.x < 0)
                    {
                        this.transform.localScale = new Vector2(1, this.transform.localScale.y);
                        ChangeAnimationState(Player_SideB);
                        attackDelay = .8f;
                        //rb.gravityScale = 0;
                    }
                    else
                    {
                        ChangeAnimationState(Player_SideB); //ForwardAir
                        attackDelay = .8f;
                        //rb.gravityScale = 0;
                    }

                }
                else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
                {
                    //BackAir
                    if (this.transform.localScale.x > 0)
                    {
                        this.transform.localScale = new Vector2(-1, this.transform.localScale.y);
                        ChangeAnimationState(Player_SideB);
                        attackDelay = .52f;
                    }
                    else
                    {
                        ChangeAnimationState(Player_SideB); //ForwardAir
                        attackDelay = 1.1f;
                    }
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    puedoMoverme = false;
                    if (this.transform.localScale.x > 0 && Input.GetKey(KeyCode.A) || this.transform.localScale.x < 0 && Input.GetKey(KeyCode.D))
                        this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
                    rb.velocity = new Vector2(0f, 0f);
                    rb.AddForce(new Vector2(200 * this.transform.localScale.x, recovery));
                    stun = 1f;
                    activadorStun = true;
                    golpe = 0;
                    ChangeAnimationState(Player_UpB); ///////////////////////////////////////////////
                    attackDelay = .6f;
                    isRecovery = false;
                    dobleSalto = false;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    attackDelay = .73f;
                    ChangeAnimationState(Player_DownB); //DownAir
                }
                else
                {
                    attackDelay = .73f;
                    ChangeAnimationState(Player_B); // Special_B
                    Instantiate(BolaDeFuego, spawnBola.transform.position, spawnBola.transform.rotation);
                    seleccionarPlayer = BolaDeFuego.GetComponent<FuerzaBola>();
                    seleccionarPlayer.P1 = true;
                }

            }
            Invoke("AttackComplete", attackDelay);
        }
    }

    void AttackComplete()
    {
        isAttacking = false;
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
    void IntroComplete()
    {
        isIntro = false;
    }

    void moverse()
    {
        if (Input.GetKey(KeyCode.D) && puedoMoverme && !isIntro)
        {
            if (this.transform.localScale.x < 0 && isGround)
                this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
            if (correr && isGround && !isAttacking)
            {
                rb.velocity = new Vector2(velocidad * 1.5f, rb.velocity.y);
                ChangeAnimationState(Player_correr);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && isGround)
                    correr = true;
                rb.velocity = new Vector2(velocidad, rb.velocity.y);
                if (isGround && !isAttacking)
                    ChangeAnimationState(Player_caminar);
                else if (!isGround && !isAttacking && isRecovery)
                    ChangeAnimationState(Player_Falling);
            }

        }
        else if (Input.GetKey(KeyCode.A) && puedoMoverme && !isIntro)
        {
            if (this.transform.localScale.x > 0 && isGround)
                this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
            if (correr && isGround && !isAttacking)
            {
                rb.velocity = new Vector2(-velocidad * 1.5f, rb.velocity.y);
                ChangeAnimationState(Player_correr);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && isGround)
                    correr = true;
                rb.velocity = new Vector2(-velocidad, rb.velocity.y);
                if (isGround && !isAttacking)
                    ChangeAnimationState(Player_caminar);
                else if (!isGround && !isAttacking && isRecovery)
                    ChangeAnimationState(Player_Falling);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            correr = false;
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && isGround && !isAttacking && !isIntro)
                ChangeAnimationState(Player_caminar);
            else if (isGround && !isAttacking && !isIntro)
                ChangeAnimationState(Player_idle);
            else if (!isGround && !isAttacking && isRecovery)
                ChangeAnimationState(Player_Falling);
        }
    }
    void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isIntro)
        {
            if (isGround && !isAttacking)
            {
                isGround = false;
                dobleSalto = true;
                correr = false;
                ChangeAnimationState(Player_jump);
                rb.AddForce(new Vector2(0, salto));
            }
            else if (dobleSalto && !isAttacking)
            {
                ChangeAnimationState(Player_DoubleJump);
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, salto));
                dobleSalto = false;
            }
        }
    }
    void recuperarControl()
    {
        switch (golpe)
        {
            case 1:
            case 2: ChangeAnimationState(Player_Hit2); break;
            case 3: ChangeAnimationState(Player_Hit3); break;
        }
        if (stun <= 0)
            activadorStun = false;
        if (rb.velocity.x > 7 || rb.velocity.x < -7)
        {
            bool iz = Input.GetKey(KeyCode.LeftArrow);
            bool de = Input.GetKey(KeyCode.RightArrow);
            if (de)
                rb.AddForce(new Vector2(20, 0));
            if (iz)
                rb.AddForce(new Vector2(-20, 0));
        }
    }
}