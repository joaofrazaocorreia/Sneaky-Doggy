using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveInput = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;
    }
}
