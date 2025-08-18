using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    private float startPos, imageLenght;
    public GameObject mainCam;
    public float speedParallax;

    private void Start()
    {
        startPos = transform.position.x;
        imageLenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = mainCam.transform.position.x * speedParallax;
        float movement = mainCam.transform.position.x * (1 - speedParallax);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if(movement > startPos + imageLenght)
        {
            startPos += imageLenght;

        }else if(movement < startPos - imageLenght){
            startPos -= imageLenght;
        }

    }
}
