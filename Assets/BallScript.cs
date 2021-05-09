using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform paddle;
    public Transform explosion;
    public Transform powerUp;
    public bool inPlay;
    public float speed;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }
        if (!inPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives (-1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Brick"))
        {
            int alea = Random.Range(1, 101);
            if(alea < 50)
            {
                Instantiate(powerUp, collision.transform.position, collision.transform.rotation);
            }

            Transform newExplosion = Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);


            gm.UpdateScore(collision.gameObject.GetComponent<BrickScript>().points);
            Destroy(collision.gameObject);
            gm.UpdateNumberOfBricks();
        }
    }
}
