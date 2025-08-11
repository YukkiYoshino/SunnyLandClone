using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnimator;//Controlar Animações
    private Rigidbody2D playerRB; //Controlar movimentação
    private SpriteRenderer playerSprites;

    public Transform groundCheck;//OBJ para verificar colisão com o chao
    public bool isGround = false;

    public float speed;

    public float run = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        playerSprites = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        run = Input.GetAxisRaw("Horizontal");
        TrocaAnim();

        if (Input.GetKey(KeyCode.Space))
        {
            playerAnimator.SetBool("Jump", true);
        }
        playerAnimator.SetBool("Jump", false);
    }

    private void FixedUpdate()
    {
        MovePlayer(run);
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

    void TrocaAnim()
    {
        playerAnimator.SetBool("Walk", playerRB.linearVelocity.x != 0);
    }
}
