using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform barrel;
    [SerializeField] GameObject bulletPrefab;
    public static float bulletSpeed = 5f;
    float reload = 2f;
    float timer = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        Shoting();
    }
    void Shoting()
    {
        timer += Time.deltaTime;
        if (timer > reload)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(barrel.right * bulletSpeed, ForceMode2D.Impulse);
                timer = 0f;
            }
        }
    }
}
