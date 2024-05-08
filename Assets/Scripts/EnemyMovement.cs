using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float aggroTime = 2f;
    public GameObject[] movementTargets;
    public PlayerMovement player;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float aggroTimer;
    private Vector3 startPos;
    private GameObject currentTarget;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
        aggroTimer = 0f;
        startPos = transform.position;
        //currentTarget = movementTargets[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 toPlayer = player.transform.position - transform.position;
        Vector2 toStart = startPos - transform.position;

        if (toPlayer.magnitude <= 3f && !player.playingDead)
        {
            aggroTimer = aggroTime;
        }


        if (aggroTimer > 0 && toPlayer.magnitude > 0.1f && !player.playingDead)
        {
            movement = toPlayer;
            movement.Normalize();
            rb.velocity = movement * moveSpeed;

            if (movement != Vector2.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movement);
                transform.rotation = rotation;
            }
        }

        else if (aggroTimer > 0 && player.playingDead)
        {
            rb.velocity = Vector2.zero;
            aggroTimer -= Time.deltaTime;
        }

        else if (toStart.magnitude > 0.1f && aggroTimer <= 0)
        {
            movement = toStart;
            
            movement.Normalize();
            rb.velocity = movement * moveSpeed;

            if (movement != Vector2.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movement);
                transform.rotation = rotation;
            }
        }

        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
