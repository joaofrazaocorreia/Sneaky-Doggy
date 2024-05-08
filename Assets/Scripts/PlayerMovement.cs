using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Sprite defaultSprite;
    public Sprite playDeadSprite;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 moveInput;
    private bool hasObjective;
    [HideInInspector] public bool playingDead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        moveInput = new Vector2();
        hasObjective = false;
        playingDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            playingDead = true;
            rb.velocity = Vector2.zero;
            sr.sprite = playDeadSprite;
        }

        else
        {
            playingDead = false;
            sr.sprite = defaultSprite;
        }
        

        if (!playingDead)
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
    }
        
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy" && !playingDead)
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
            hasObjective = false;
        }
    }  
}
