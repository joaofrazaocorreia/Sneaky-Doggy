using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool hasObjective;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveInput = new Vector2();
        hasObjective = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        rb.velocity = moveInput * moveSpeed;

        if (moveInput != Vector2.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, moveInput);
            transform.rotation = rotation;
        }
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            gameObject.SetActive(false);
            Debug.Log("You lose");
        }

        if(collision.transform.tag == "Objective")
        {
            Destroy(collision.gameObject);
            hasObjective = true;
            Debug.Log("Got objective");
        }

        if(collision.transform.tag == "Entrance" && hasObjective)
        {
            Debug.Log("You win");
        }
    }  
}
