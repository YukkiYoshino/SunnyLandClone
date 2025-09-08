using UnityEngine;

public class IA_Bear : MonoBehaviour
{
    public Transform enemy;
    public SpriteRenderer enemySprite;
    public Transform[] position; // Pontos fixos da patrulha
    public float speed = 2f;
    public bool isRight = false;
    public bool chasing = false;

    public Transform playerPosition;

    private int idTarget = 0;

    void Start()
    {
        enemySprite = enemy.GetComponent<SpriteRenderer>();
        enemy.position = position[0].position;
        idTarget = 1;
    }

    void Update()
    {
        if (enemy == null) return;

        if (chasing)
            PerseguirJogador();
        else
            Patrulhar();

        AtualizarDirecao();
    }

    void Patrulhar()
    {
        enemy.position = Vector3.MoveTowards(enemy.position, position[idTarget].position, speed * Time.deltaTime);

        if (Vector3.Distance(enemy.position, position[idTarget].position) < 0.1f)
        {
            idTarget++;
            if (idTarget >= position.Length)
                idTarget = 0;
        }
    }

    void PerseguirJogador()
    {
        enemy.position = Vector3.MoveTowards(enemy.position, playerPosition.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasing = true;
        }
    }

    void AtualizarDirecao()
    {
        Vector3 alvo = chasing ? playerPosition.position : position[idTarget].position;

        if ((alvo.x < enemy.position.x && isRight) || (alvo.x > enemy.position.x && !isRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        isRight = !isRight;
        enemySprite.flipX = !enemySprite.flipX;
    }
}
