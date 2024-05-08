using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public PlayerMovement player;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool sawPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
        sawPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 toPlayer = player.transform.position - transform.position;

        if (toPlayer.magnitude <= 3f)
            sawPlayer = true;


        if (sawPlayer && toPlayer.magnitude > 0.1f)
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

        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
