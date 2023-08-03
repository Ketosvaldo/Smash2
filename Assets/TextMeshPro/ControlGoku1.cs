using UnityEngine;

public class ControlGoku1 : MonoBehaviour
{
    PauseMenu pauseMenu;
    float tiempo;
    bool timer = false;
    public bool g = false;
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
    Animator anim;
    //Variables de ataque
    public int idAtaque;
    public bool isAttacking;
    public float jabTiempo;
    float attackDelay;
    //----------------------------//
    public string currentState;
    const string Player_caminar = "Goku_Walk";
    const string Player_correr = "Goku_Run";
    const string Player_idle = "Goku_Idle";
    const string Player_jump = "Goku_Jump";
    //const string Player_DoubleJump = "MarioJump0";
    const string Player_ForwardAir = "Goku_ForwardAir";
    const string Player_UpAir = "Goku_UpAir";
    const string Player_DownAir = "Goku_DownAir";
    const string Player_BackAir = "Goku_BackAir";
    const string Player_Nair = "Goku_Nair";
    const string Player_FowardTilt = "Goku_ForwardTilt";
    const string Player_UpTilt = "Goku_UpTilt";
    const string Player_DownTilt = "Goku_DownTilt";
    const string Player_B = "Goku_SpecialB";
    const string Player_SideB = "Goku_SideB";
    const string Player_DownB = "Goku_DownB";
    const string Player_UpB = "Goku_Recovery";
    const string Player_Jab1 = "Goku_Jab1";
    const string Player_Jab2 = "Goku_Jab2";
    const string Player_Falling = "Goku_Falling";
    const string Player_Intro = "Goku_Intro";
    const string Player_Hit1 = "Goku_Hit1";
    const string Player_Hit2 = "Goku_Hit2";
    const string Player_Hit3 = "Goku_Hit3";

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
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
        if (!activadorStun)
        {
            golpe = 0;
        }
        if (espera <= 2)
        {
            espera += Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                puedoMoverme = true;
                isAttacking = false;
            }
            if (isIntro)
            {
                ChangeAnimationState(Player_Intro);
                Invoke("IntroComplete", 1f);
            }
            if (timer)
                tiempo += Time.deltaTime;
            if (tiempo >= 0.55f && g)
            {
                rb.transform.Translate(new Vector2(0f, 3f));
                g = false;
            }
            else if (tiempo >= .65)
            {
                timer = false;
                rb.gravityScale = 1;
                tiempo = 0;
            }


            if (rb.velocity.y < -6.5)
                rb.velocity = new Vector2(rb.velocity.x, -6.5f);
            
          
            if (rb.velocity.x > -8 && rb.velocity.x < 8 && !activadorStun && !pauseMenu.GameIsPaused)
                Moverse();
            else
            {
                recuperarControl();
            }
            //Salto
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
                    ChangeAnimationState(Player_jump);
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(0, salto));
                    dobleSalto = false;
                }
            }
            ataquesNormales();
            ataquesEspeciales();
            switch (currentState)
            {
                case Player_Jab1: if (jabTiempo >= 0.4) { isAttacking = false; xd = false; ChangeAnimationState(Player_idle); jabTiempo = 0; } break;
                case Player_Jab2: if (jabTiempo >= 0.8) { isAttacking = false; xd = false; ChangeAnimationState(Player_idle); jabTiempo = 0; } break;
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
            isGround = true;
            dobleSalto = true;
            isRecovery = true;
            timer = false;
            tiempo = 0;
            g = false;
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
                    attackDelay = .7f;
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
                        ChangeAnimationState(Player_ForwardAir);
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
                        ChangeAnimationState(Player_ForwardAir);
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
                    rb.AddForce(new Vector2(0, -recovery));
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
                default: Invoke("AttackComplete", attackDelay); break;
            }
        }
        else if (ataco && xd)
        {
            switch (currentState)
            {
                case Player_Jab1: ChangeAnimationState(Player_Jab2); break;
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
                    timer = true;
                    rb.velocity = new Vector2(0f, 0f);
                    ChangeAnimationState(Player_UpB);
                    if (tiempo >= 2f)
                    {
                        rb.AddForce(new Vector2(0, recovery));
                        rb.transform.Translate(new Vector2(0f, 7f));

                    }
                    isRecovery = false;
                    dobleSalto = false;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    attackDelay = .65f;
                    ChangeAnimationState(Player_DownB);
                    velocidad++;
                }
                else if (Input.GetKey(KeyCode.O))
                {
                    attackDelay = .68f;
                    ChangeAnimationState(Player_B);
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
                    if (this.transform.localScale.x > 0 && Input.GetKey(KeyCode.LeftArrow) || this.transform.localScale.x < 0 && Input.GetKey(KeyCode.RightArrow))
                        this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
                    timer = true;
                    rb.velocity = new Vector2(0f, 0f);
                    ChangeAnimationState(Player_UpB); ///////////////////////////////////////////////
                    rb.gravityScale = 0;
                    isRecovery = false;
                    dobleSalto = false;
                    g = true;
                }

                else if (Input.GetKey(KeyCode.S))
                {
                    attackDelay = .73f;
                    ChangeAnimationState(Player_DownB); //DownB                  
                }
                else
                {
                    attackDelay = .73f;
                    ChangeAnimationState(Player_B); // Special_B
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

    void Moverse()
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
            bool iz = Input.GetKey(KeyCode.A);
            bool de = Input.GetKey(KeyCode.D);
            if (de)
                rb.AddForce(new Vector2(20, 0));
            if (iz)
                rb.AddForce(new Vector2(-20, 0));
        }
    }
}
