using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offsetX = 3f;
    public float smooth = 0.1f;

    public float limitedUp = 2;
    public float limitedDown = 0f;
    public float limitedLeft = 0;
    public float limitedRight = 100f;

    private Transform player;
    private float playerX;
    private float playerY;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
    }


    void FixedUpdate()
    {
        if (player != null)
        {
            playerX = Mathf.Clamp(player.position.x, limitedLeft, limitedRight);
            playerY = Mathf.Clamp(player.position.y, limitedDown, limitedUp);

            transform.position = Vector3.Lerp(transform.position, new Vector3(playerX, playerY, transform.position.z), smooth);
        }
    }
}
