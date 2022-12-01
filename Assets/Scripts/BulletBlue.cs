using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BulletBlue : MonoBehaviour
{
    Vector2 min;
    Vector2 max;
    Rigidbody2D rb;
    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (transform.position.x > max.x || transform.position.x < min.x || transform.position.y > max.y || transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "CPU":
                ScoresController.ScoreBlue++;
                Destroy(gameObject);
                SceneManager.LoadScene(0);
                TankRed.state = TankRed.State.Choice;
                break;
            case "Player":
                Tank.state = Tank.State.Stop;
                Destroy(gameObject);
                break;
            case "BulletBlue":
                Destroy(gameObject);
                break;
            case "BulletRed":
                Destroy(gameObject);
                break;
            case "Stone":
                Vector2 derection = -collision.relativeVelocity.normalized;
                Vector2 normal = collision.contacts[0].normal;
                Vector2 newDerection = Vector2.Reflect(derection, normal);
                rb.AddForce(newDerection * Cannon.bulletSpeed, ForceMode2D.Impulse);
                break;
        }
    }
}
