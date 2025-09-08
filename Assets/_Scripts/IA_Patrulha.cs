using UnityEngine;

public class IA_Patrulha : MonoBehaviour
{

    public Transform      enemy;
    public SpriteRenderer enemySprite;
    public Transform[]    position;
    public float          speed;
    public bool           isRight = false;

    private int idTarget;

    void Start() 
    {
        enemySprite = enemy.gameObject.GetComponent<SpriteRenderer>();
        enemy.position = position[0].position;
        idTarget = 1;
    }
    void Update()
    {
        if(enemy != null)
        {
            enemy.position = Vector3.MoveTowards(enemy.position, position[idTarget].position, speed * Time.deltaTime);
            
            if(enemy.position == position[idTarget].position){
                idTarget += 1;
                if(idTarget == position.Length)
                {
                    idTarget = 0;
                }
            }
        }

        if (position[idTarget].position.x < enemy.position.x && isRight == true)
        {
            Flip();

        } else if (position[idTarget].position.x > enemy.position.x && isRight == false)
        {
            Flip();
        }
    }

    void Flip()
    {
        isRight = !isRight;

        if (isRight)
        {
            enemySprite.flipX = false;
        }
        else
        {
            enemySprite.flipX = true;
        }
        
    }
}
