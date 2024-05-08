using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public UIManager UIManager;
    public Sprite defaultSprite;
    public Sprite playDeadSprite;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 moveInput;
    private bool hasObjective;
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool playingDead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        moveInput = new Vector2();
        hasObjective = false;
        gameOver = false;
        playingDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
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
                    float rotZ;
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, moveInput); ;

                    if (Mathf.Sqrt(Mathf.Pow((rotation.eulerAngles.z - transform.rotation.eulerAngles.z) * Time.deltaTime * 20, 2)) < 0.5f)
                        rotZ = rotation.eulerAngles.z;

                    else
                        rotZ = transform.rotation.eulerAngles.z + (rotation.eulerAngles.z - transform.rotation.eulerAngles.z) * Time.deltaTime * 20;

                    transform.rotation = Quaternion.Euler(0, 0, rotZ);
                }
            }
        }

        else rb.velocity = Vector2.zero;
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameOver)
        {
            if (collision.transform.tag == "Enemy" && !playingDead)
            {
                gameOver = true;
                gameObject.SetActive(false);
                UIManager.Lose();
            }

            if (collision.transform.tag == "Objective")
            {
                hasObjective = true;
                UIManager.GetObjective(collision.gameObject);
            }

            if (collision.transform.tag == "Entrance" && hasObjective)
            {
                gameOver = true;
                UIManager.Win();
            }
        }
    }  
}
