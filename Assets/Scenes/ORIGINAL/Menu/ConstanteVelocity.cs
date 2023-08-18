using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstanteVelocity : MonoBehaviour
{
    Rigidbody2D rb;
    float randomX, randomY;
    float timer, actualTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randomX = Random.Range(-10, 10);
        randomY = Random.Range(-10, 10);
        timer = Random.Range(4,10);
        actualTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(randomX, randomY);

        actualTimer -= Time.deltaTime;

        if(actualTimer < 0)
        {
            randomX = Random.Range(-10, 10);
            randomY = Random.Range(-10, 10);
            timer = Random.Range(4, 10);
            actualTimer = timer;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BorderUI")
        {
            randomX = Random.Range(-10, 10);
            randomY = Random.Range(-10, 10);
        }
    }
}
