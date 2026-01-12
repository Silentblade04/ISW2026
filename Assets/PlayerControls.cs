using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private int leftBoarder;
    [SerializeField] private int rightBoarder;

    [SerializeField] private Transform playerTransform;
    void Start()
    {
        
    }

    void Update()
    { 
        Vector2 playerPos = playerTransform.position;

        //Input.GetAxis("Horizontal");

        playerPos.x = Input.GetAxis("Horizontal");

        //ClampPosition();
    }
    void ClampPosition()
    {
        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, leftBoarder, rightBoarder);

        transform.position = pos;
    }
}
