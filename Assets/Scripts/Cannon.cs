using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform barrel;
    [SerializeField] GameObject bulletPrefab;
    public static float bulletSpeed = 3000f;
    void Start()
    {
        
    }

    void Update()
    {
        Shoting();
    }
    void Shoting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = rb.transform.right * bulletSpeed * Time.deltaTime;
        }
    }
}
