using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float direction;
    private bool moving;
    private Vector3 currentTarget;
    private float dir;
    [Range(0, 1)]
    public float power = 1f;

    // Update is called once per frame
    void Update()
    {
        Move();
        Vector3 forward = transform.TransformDirection(Vector2.up) * 100;
        Debug.DrawRay(transform.position, forward, Color.white); //uncomment this to see the raycast in the scenes
    }

    void Move()
    {
        dir = Mathf.Atan2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("fire");

            
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            if (Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector2.up) * (dir * power), ForceMode2D.Impulse);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(-Vector2.up) * (dir * power), ForceMode2D.Impulse);
            }
        } 
    }
}
