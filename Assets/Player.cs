using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forcaMovimento = 1f;
    public float forcaPulo = 7f;
    public float velocidadeMaxima = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float direcao = Input.GetAxisRaw("Horizontal");

        // Aplica impulso horizontal se ainda não estiver na velocidade máxima
        if (Mathf.Abs(rb.linearVelocity.x) < velocidadeMaxima)
        {
            rb.AddForce(Vector2.right * direcao * forcaMovimento, ForceMode2D.Impulse);
        }

        // Pulo com impulso ao pressionar espaço
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
        }

        // Virar o personagem para a direção do movimento
        if (direcao != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direcao), 1, 1);
        }
    }
}
