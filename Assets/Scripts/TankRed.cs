using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRed : MonoBehaviour
{
    [SerializeField] float constSpeed;
    Rigidbody2D tankRedRigidbody;
    PolygonCollider2D tankRedPolygonCollider;
    public float distance = 1;
    static public Transform player;
    static public Transform cpu;

    //вектор к красному танку
    //Vector2 blueTank = new Vector2(player.position.magnitude, cpu.position.magnitude);
    //вектор к синему танку
    Vector2 redTank = new Vector2(cpu.position.magnitude, player.position.magnitude);

    void Start()
    {
        tankRedRigidbody = GetComponent<Rigidbody2D>();
        tankRedPolygonCollider = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cpu = GameObject.FindGameObjectWithTag("CPU").transform;
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > distance)
        {
            while (Vector2.Dot(player.transform.position, redTank) != Vector2.Dot(redTank, player.transform.position))
            {
                tankRedRigidbody.AddTorque(constSpeed * Time.deltaTime);
            }
            tankRedRigidbody.AddRelativeForce(Vector2.down * constSpeed * Time.deltaTime);
        }
    }
}
