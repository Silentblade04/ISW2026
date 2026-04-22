using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;


public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float leftBoarder;
    [SerializeField] private float rightBoarder;

    [SerializeField] GameObject projectileBody;
    [SerializeField] Projectile projectileOBJ;
    [SerializeField] Rigidbody2D projectileRb;

    [SerializeField] float fireDelay;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int health;

    [SerializeField] private TMPro.TextMeshProUGUI healthtext;
    void Start()
    {
        health = 3;
    }

    void Update()
    {
        playerMovment(new Vector2(Input.GetAxis("Horizontal"),0));
        Movement();
        ClampPosition();
        mobileAttack();

        healthtext.text = "Health: " + health;
    }

    private Coroutine atkCoroutine;

    /*public void Attack(InputAction.CallbackContext context)
    {
        // If not already attacking, start attack
        if (atkCoroutine == null)
        {
            Debug.Log("Attack");
            atkCoroutine = StartCoroutine(StartAttackCoroutine());
        }
    }*/
    private void mobileAttack()
    {
        // If not already attacking, start attack
        if (atkCoroutine == null)
        {
           // Debug.Log("Attack");
            atkCoroutine = StartCoroutine(StartAttackCoroutine());
        }
    }

    [Obsolete]
    private IEnumerator StartAttackCoroutine()
    {
       // Debug.Log("Firing");

        GameObject projectile = Instantiate(projectileBody, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        projectile.GetComponent<ProjectileScript>().timer = projectileOBJ.timer;
        projectile.GetComponent<Rigidbody2D>().linearVelocityY = 2f;


        //Debug.Log("Cooling Down!");
        yield return new WaitForSeconds(fireDelay);

        atkCoroutine = null;
    }

    public void damage()
    {
        health -= 1;
        healthtext.text = "Health: 0";
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
    void Movement()
    {
        float x = Input.acceleration.x;//negative = left, positive = right
        if (x > 0)
        {
            playerTransform.Translate(Vector3.right * Input.acceleration.x * 2.5f * Time.deltaTime);
        }
        else if (x < 0)
        {
            playerTransform.Translate(Vector3.left * Input.acceleration.x * 2.5f * Time.deltaTime);

        }

    }

    private void playerMovment(Vector2 direction)
    {
        playerTransform.Translate(direction*2.5f*Time.deltaTime);
    }

    void ClampPosition()
    {
        Vector2 pos = transform.position;

        //Debug.Log("Clamping Position");
        pos.x = Mathf.Clamp(pos.x, leftBoarder, rightBoarder);
        //Debug.Log(pos.x);
        transform.position = pos;
    }
}
