using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRed : MonoBehaviour
{
    [SerializeField] Transform barrel;
    [SerializeField] GameObject bulletPrefab;
    public static float bulletSpeed = 3000f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = -rb.transform.up * bulletSpeed * Time.deltaTime;
    }
}
