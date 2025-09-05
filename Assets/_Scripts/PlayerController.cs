using NUnit.Framework.Internal;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{

    private Animator        playerAnimator;//Controlar Animações
    private Rigidbody2D     playerRB; //Controlar movimentação
    private SpriteRenderer  playerSprites;
    private GameController  _GM;

    public Transform groundCheck; //OBJ para verificar colisão com o chao
    public bool      isGround = false;

    //movimentação
    public float speed;
    public float run = 0.0f;

    //Pulo
    public bool  jump = false;
    public float jumpForce;
    public int   numberJumps = 0;
    public int   maxJump = 2;
    private float initialPos;

    //atributos
    int  vida = 3;
    bool invulneravel = false;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        playerSprites = GetComponent<SpriteRenderer>();
        _GM = FindAnyObjectByType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("chao"));
        playerAnimator.SetBool("IsGrounded", isGround);

        run = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            initialPos = playerRB.transform.position.y;
            jump = true;
            playerAnimator.SetBool("Jump", jump);

        }
        TrocaAnim();
        
        playerAnimator.SetFloat("EixoY", playerRB.linearVelocityY);
    }

    private void FixedUpdate()
    {
        MovePlayer(run);
        if (jump)
        {
            PlayerJump();
        }
    }

    void MovePlayer(float movimentoH)
    {
        playerRB.linearVelocity = new Vector2(movimentoH * speed, playerRB.linearVelocity.y);

        if (run >= 0)
        {
            playerSprites.flipX = false;
        }
        else
        {
            playerSprites.flipX = true;
        }
    }

    void PlayerJump()
    {
        if (isGround)
        {
            numberJumps = 0;
        }
        if (isGround || numberJumps < maxJump){
            playerRB.AddForce(new Vector2(0f, jumpForce));
            isGround = false;
            numberJumps++;
        }
        jump = false;
    }
    void TrocaAnim()
    {
        playerAnimator.SetBool("Walk", playerRB.linearVelocity.x != 0 && isGround);
        playerAnimator.SetBool("Jump", !isGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "CENOURA":
                _GM.Pontuacao(1);
                Destroy(collision.gameObject);
                break;
            case "ESTRELA":
                _GM.ColetaItem("ESTRELA");
                Destroy(collision.gameObject);
                break;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invulneravel)
        {
            switch (collision.gameObject.tag)
            {
                case "OBSTACULO":
                    invulneravel = true;
                    playerRB.AddForce(new Vector2(0f, jumpForce));
                    vida--;
                    
                    if (vida == 0)
                    {
                        _GM.FimJogo();
                    }
                    StartCoroutine("ivunerabilidade");
                    _GM.SofrerDano(vida);
                    break;
            }
        }
        
    }
    IEnumerator ivunerabilidade()
    {
        for (float i = 0;i < 1;i+= 0.1f)
        {
            playerSprites.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprites.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invulneravel = false;

    }
}
