using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TankRed : MonoBehaviour
{
    [SerializeField] Transform barrel;
    [SerializeField] GameObject bulletPrefab;
    float bulletSpeed = 5f;
    [SerializeField] float tankSpeed;
    public float slerp;
    Rigidbody2D tankRedRigidbody;
    public enum State { Navigation, Move, Attack, Choice };
    public static State state = State.Attack;
    [SerializeField] Transform[] point = new Transform[4];

    float navigationTimer = 3f;
    float moveTimer = 2f;
    float attackTimer = 1f;
    float shootTimer = 0f;
    static int position = 2;

    void Start()
    {
        tankRedRigidbody = GetComponent<Rigidbody2D>(); 
    }
    void Update()
    {
        Choice();
        Move();
        RotateAtTank();
        Raycast();
        Navigation();
        Debug.Log(state);
    }
    private void FixedUpdate()
    {

    }
    void Raycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(barrel.transform.position, -transform.up, Mathf.Infinity);
        if (state == State.Attack || state == State.Navigation || state == State.Move)
        {
            if (hit == true)
            {
                if (hit.collider.tag == "Player")
                {
                    Shooting();
                }
                else if (hit.collider.tag == "Stone")
                {
                    Vector2 direction = hit.point - (Vector2)barrel.transform.position;
                    Vector2 normal = hit.normal.normalized;
                    RaycastHit2D hit2 = Physics2D.Raycast(hit.point, Vector2.Reflect(direction, normal), Mathf.Infinity);
                    if (hit2 == true && hit2.collider.tag == "Player")
                    {
                        Shooting();
                    }
                }
            }
        }
    }
    void Navigation()
    {
        if (state == State.Navigation)
        {
            Vector2 directionTankToPoint = (point[position].position - barrel.transform.position).normalized;
            Ray2D directionRay = new Ray2D(barrel.transform.position, -transform.up);
            Vector2 direction = directionRay.direction.normalized;
            Quaternion rotation = new Quaternion();
            rotation.SetFromToRotation(direction, directionTankToPoint);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation * transform.rotation, slerp * Time.deltaTime);
            navigationTimer -= Time.deltaTime;
            Raycast();
            if (navigationTimer < 0)
            {
                state = State.Move;
                navigationTimer = 6f;
            }
        }
    }
    
    void Move()
    {
        if (state == State.Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, point[position].position, tankSpeed * Time.deltaTime);
            moveTimer -= Time.deltaTime;
            if (moveTimer < 0)
            {
                state = State.Attack;
                moveTimer = 4f;
            }
        }
    }
    void RotateAtTank()
    {
        if (state == State.Attack)
        {
            Vector2 directionRedBlue = (Tank.tankTransform.position - barrel.transform.position).normalized;
            Ray2D directionRay = new Ray2D(barrel.transform.position, -transform.up);
            Vector2 direction = directionRay.direction.normalized;
            Quaternion rotation = new Quaternion();
            rotation.SetFromToRotation(direction, directionRedBlue);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation * transform.rotation, slerp * Time.deltaTime);
            attackTimer -= Time.deltaTime;
            if(attackTimer < 0)
            {
                state = State.Choice;
                attackTimer = 3f;
            }
        }
    }
    void Choice()
    {
        if (state == State.Choice)
        {
            System.Random random = new System.Random();
            int choice = random.Next(0, 4);
            while (choice == position)
            {
                choice = random.Next(0, 4);
            }
            state = State.Navigation;
            position = choice;
        }
    }
    void Shooting()
    {
        float fireRate = 0.2f;
        shootTimer += Time.deltaTime;
        if (shootTimer > fireRate)
        {
            GameObject bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(-barrel.up * bulletSpeed, ForceMode2D.Impulse);
            shootTimer = 0;
        }
    }
}
