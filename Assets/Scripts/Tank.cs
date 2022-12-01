using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] float constSpeed;
    [SerializeField] AudioClip Track;
    AudioSource tankAudio;
    Rigidbody2D tankRigidbody;
    public static Transform tankTransform;
    public enum State { Attack, Stop };
    public static State state = State.Attack;
    void Start()
    {
        tankAudio = GetComponent<AudioSource>();
        tankRigidbody = GetComponent<Rigidbody2D>();
        tankTransform = GetComponent<Transform>();
    }
    void Update()
    {   
        if (state == State.Stop)
        {
            Invoke("Attack", 2f);
        }
        if (state == State.Attack)
        {
            Move();
            Rotation();
            Audio();
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            tankRigidbody.AddRelativeForce(Vector2.right * constSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            tankRigidbody.AddRelativeForce(constSpeed * Time.deltaTime * Vector2.left);
        }
        
    }
    void Rotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            tankRigidbody.AddTorque(constSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            tankRigidbody.AddTorque(-constSpeed * Time.deltaTime);
        }
    }
    void Audio()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            tankAudio.Stop();
            tankAudio.PlayOneShot(Track);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            tankAudio.Stop();
            tankAudio.Play();
        }
    }
    void Attack()
    {
        state = State.Attack;
    }
}
