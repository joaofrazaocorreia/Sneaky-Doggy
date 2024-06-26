using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float idleTime = 5f;
    public float aggroTime = 2f;
    public float aggroDistance = 4f;
    public Transform[] movementTargets;
    public PlayerMovement player;

    private Rigidbody2D rb;
    private UIManager UIManager;
    private Vector2 movement;
    private float aggroTimer;
    private float idleTimer;
    private Transform currentTarget;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UIManager = player.UIManager;
        movement = new Vector2();
        aggroTimer = 0f;
        idleTimer = 0f;
        currentTarget = movementTargets[Random.Range(0, movementTargets.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.gameStopped && !UIManager.isPaused)
        {
            Vector2 toTarget = currentTarget.position - transform.position;

            if ((player.transform.position - transform.position).magnitude <= aggroDistance && !player.playingDead)
            {
                aggroTimer = aggroTime;
                idleTimer = 0f;
                currentTarget = player.transform;
            }



            if (aggroTimer > 0 && toTarget.magnitude > 0.1f && !player.playingDead)
            {
                movement = toTarget;
                movement.Normalize();
                rb.velocity = movement * moveSpeed;

                if (movement != Vector2.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movement);
                    transform.rotation = rotation;
                }
            }

            else if (aggroTimer > 0 && player.playingDead && toTarget.magnitude <= 3f)
            {
                rb.velocity = Vector2.zero;
                aggroTimer -= Time.deltaTime;

                if (aggroTimer <= 0)
                {
                    Transform prevTarget = currentTarget;

                    while (currentTarget == prevTarget)
                        currentTarget = movementTargets[Random.Range(0, movementTargets.Length)];
                }

            }

            else if (aggroTimer <= 0 && toTarget.magnitude > 0.2f && idleTimer <= 0)
            {
                movement = toTarget;
                movement.Normalize();
                rb.velocity = movement * moveSpeed;

                if (movement != Vector2.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movement);
                    transform.rotation = rotation;
                }
            }

            else if (aggroTimer <= 0 && toTarget.magnitude <= 0.2f && idleTimer <= 0)
            {
                idleTimer = idleTime;
            }

            else
            {
                rb.velocity = Vector2.zero;
                idleTimer -= Time.deltaTime;

                if (idleTimer <= 0)
                {
                    Transform prevTarget = currentTarget;

                    while (currentTarget == prevTarget)
                        currentTarget = movementTargets[Random.Range(0, movementTargets.Length)];
                }
            }
        }

        else rb.velocity = Vector2.zero;
    }
}
