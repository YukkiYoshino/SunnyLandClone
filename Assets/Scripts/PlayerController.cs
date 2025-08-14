using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnimator;//Controlar Animações
    private Rigidbody2D playerRB; //Controlar movimentação
    private SpriteRenderer playerSprites;

    public Transform groundCheck;//OBJ para verificar colisão com o chao
    public bool isGround = false;

    //movimentação
    public float speed;
    public float run = 0.0f;

    //Pulo
    public bool jump = false;
    public float jumpForce;
    public int numberJumps = 0;
    public int maxJump = 2;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        playerSprites = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("chao"));
        playerAnimator.SetBool("IsGrounded", isGround);

        run = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {

            jump = true;
            playerAnimator.SetBool("Jump", jump);

        }
        TrocaAnim();

        //playerAnimator.SetFloat("EixoY", playerAnimator.transform.localPosition.y);
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
}
