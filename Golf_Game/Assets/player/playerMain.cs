using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMain : MonoBehaviour
{
    private float direction;
    private bool moving;
    private Vector3 currentTarget;
    private float dir;
    [Range(0, 1)]
    public float power = 1f;

    private Rigidbody2D rb;
    
    private float speed;
    private int strokes;
    private Vector2 move;

    private bool statusAffectEnlarge;
    private bool statusAffectShrink;
    private bool statusAffectSpeedUp;
    private bool statusAffectSpeedSlow;
    private bool statusAffectCanClimb;
    private bool statusAffectNoClimb;

    private uiController myUIController;

    public bool StatusAffectEnlarge { get => statusAffectEnlarge; set => statusAffectEnlarge = value; }
    public bool StatusAffectShrink { get => statusAffectShrink; set => statusAffectShrink = value; }
    public bool StatusAffectSpeedUp { get => statusAffectSpeedUp; set => statusAffectSpeedUp = value; }
    public bool StatusAffectSpeedSlow { get => statusAffectSpeedSlow; set => statusAffectSpeedSlow = value; }
    public bool StatusAffectCanClimb { get => statusAffectCanClimb; set => statusAffectCanClimb = value; }
    public bool StatusAffectNoClimb { get => statusAffectNoClimb; set => statusAffectNoClimb = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Strokes { get => strokes; set => strokes = value; }
    public uiController MyUIController { get => myUIController; set => myUIController = value; }

    private void Awake() {

        speed = 15.0f;
        strokes = 15;

        statusAffectEnlarge = false;
        statusAffectShrink = false;
        statusAffectSpeedUp = false;
        statusAffectSpeedSlow = false;
        statusAffectCanClimb = false;
        statusAffectNoClimb = false;

        myUIController = GameObject.FindGameObjectsWithTag("UI")[0].GetComponent<uiController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Move();
        Vector3 forward = transform.TransformDirection(Vector2.up) * 100;
        Debug.DrawRay(transform.position, forward, Color.white); //uncomment this to see the raycast in the scenes

        
        if(strokes <= 0) {
            myUIController.showFailCase();
        }
    }
    
    void Move()
    {
        dir = Mathf.Atan2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);

        if (Input.GetButtonDown("Fire1"))
        {
            // Debug.Log("fire");

            if (Input.GetAxis("Horizontal") < 0)
            {
                rb.AddForce(transform.TransformDirection(Vector2.up) * (dir * power), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(transform.TransformDirection(-Vector2.up) * (dir * power), ForceMode2D.Impulse);
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }
}
