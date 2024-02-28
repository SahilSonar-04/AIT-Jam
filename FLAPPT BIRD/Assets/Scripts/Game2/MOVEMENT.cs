using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };
public enum Gamemodes { Cube = 0, Ship = 1, Ball = 2, UFO = 3, Wave = 4, Spider = 5 };

public class MOVEMENT : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public Gamemodes CurrentGamemode;
    //                       0      1      2       3      4
    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };
    [System.NonSerialized] public int[] screenHeightValues = { 11, 10, 8, 10, 9 };
    [System.NonSerialized] public float yLastPortal = -2.3f;

    public float GroundCheckRadius;
    public LayerMask GroundMask;
    public Transform Sprite;

    public Rigidbody2D rb;

    public int Gravity = 1;
    public bool clickProcessed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
        Invoke(CurrentGamemode.ToString(), 0);
    }

    public bool OnGround()
    {
        return Physics2D.OverlapBox(transform.position + Vector3.down * Gravity * 0.5f, Vector2.right * 1.1f + Vector2.up * GroundCheckRadius, 0, GroundMask);
    }

    bool TouchingWall()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + (Vector2.right * 0.55f), Vector2.up * 0.8f + (Vector2.right * GroundCheckRadius), 0, GroundMask);
    }

    void Cube()
    {
        generic.createGamemode(rb, this, true, 19.5269f, 4.57f, true, false, 409.1f);
    }

    void Ship()
    {
        rb.gravityScale = 2.93f * (Input.GetMouseButton(0) ? -1 : 1) * Gravity;
        generic.LimitYVelocity(9.95f, rb);
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2);
    }

    void Ball()
    {
        generic.createGamemode(rb, this, true, 0, 6.2f, false, true);
    }

    void UFO()
    {
        generic.createGamemode(rb, this, false, 10.841f, 4.1483f, false, false, 0, 10.841f);
    }

    void Wave()
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, SpeedValues[(int)CurrentSpeed] * (Input.GetMouseButton(0) ? 1 : -1) * Gravity);
    }

    void Spider()
    {
        generic.createGamemode(rb, this, true, 238.29f, 6.2f, false, true, 0, 238.29f);
    }

    public void ChangeThroughPortal(Gamemodes Gamemode, Speeds Speed, int gravity, int State , float yPortal)
    {
        switch (State)
        {
            case 0:
                CurrentSpeed = Speed;
                break;
            case 1:
                yLastPortal = yPortal;
                CurrentGamemode = Gamemode;
                break;
            case 2:
                Gravity = gravity;
                rb.gravityScale = Mathf.Abs(rb.gravityScale) * gravity;
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PortalScript portal = collision.gameObject.GetComponent<PortalScript>();
        if(portal)
            portal.initiatePortal(this);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the Rigidbody2D component of the colliding object
        //Rigidbody2D collidingRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        // Check if the colliding object has a Rigidbody2D component
        //if (collidingRigidbody != null)
        //{
        // Nullify the velocity in the x-direction
        //collidingRigidbody.velocity = new Vector2(0f, collidingRigidbody.velocity.y);
        //}
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "block")
            {
                // Stop the bird's movement by setting its velocity to zero
                rb.velocity = Vector2.zero;
                Debug.Log("Collided with block");

                // Trigger game over logic
                //GameOver();
            }
        }
    }

}