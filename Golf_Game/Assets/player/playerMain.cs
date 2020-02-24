using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

enum PowerLevel
{
    Low = 1,
    Medium = 2,
    High = 3
}

public class playerMain : MonoBehaviour
{
    private float direction;
    private bool moving;
    private Vector3 currentTarget;
    private float dir;
    
    [Range(0, 1)]
    public float power = 0.1f;
    public float ballSpeed;

    public Sprite plainSprite;
    public Sprite lowSprite;
    public Sprite medSprite;
    public Sprite highSprite;

    private SpriteRenderer arrow;
    private bool selectingPowerLevel = false;
    PowerLevel currentPowerLevel = PowerLevel.Low;
    bool gotLevel = false;
    
    private Rigidbody2D rb;
    
    private float speed;
    private int strokes;
    public int strokeMaxForLevel;
    public AudioSource strokeSound;
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
        strokes = 0;
        //strokeMaxForLevel = 0;

        statusAffectEnlarge = false;
        statusAffectShrink = false;
        statusAffectSpeedUp = false;
        statusAffectSpeedSlow = false;
        statusAffectCanClimb = false;
        statusAffectNoClimb = false;

        myUIController = GameObject.FindGameObjectsWithTag("UI")[0].GetComponent<uiController>();
        rb = GetComponent<Rigidbody2D>();
        arrow = transform.Find("Arrow").GetComponent<SpriteRenderer>();
    }

    private void Update() {
        Move();
        Vector3 forward = transform.TransformDirection(Vector2.up) * 100;
        //Debug.DrawRay(transform.position, forward, Color.white); //uncomment this to see the raycast in the scenes

        ballSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;

        if(strokes > strokeMaxForLevel) {
            myUIController.showFailCase();
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            gotLevel = true;
        }
    }
    
    void Move()
    {
        dir = Mathf.Atan2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);

        if (Input.GetButtonDown("Fire1") && !selectingPowerLevel)
        {

            StartCoroutine(GetPowerLevel());
            selectingPowerLevel = true;
        }

    }

    IEnumerator GetPowerLevel()
    {
        gotLevel = false;
        arrow.sprite = highSprite;
        currentPowerLevel = PowerLevel.High;


        while (!gotLevel)
        {
            IncrementLevel();
            yield return new WaitForSeconds(1);
        }
        
        strokes += 1;
        strokeSound.Play();

        float newPower = power * (int) currentPowerLevel;
        Debug.Log(newPower);
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(transform.TransformDirection(Vector2.up) * (dir > 1 ? (dir * newPower) : newPower), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(transform.TransformDirection(-Vector2.up) * (dir < 1 ? (dir * newPower) : newPower), ForceMode2D.Impulse);
        }

        arrow.sprite = plainSprite;
        selectingPowerLevel = false;

        yield return new WaitForSeconds(5);
        if (ballSpeed < 1f)
        {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
        }
        
    }

    void IncrementLevel()
    {
        switch (currentPowerLevel)
        {
            case PowerLevel.Low:
                arrow.sprite = medSprite;
                currentPowerLevel = PowerLevel.Medium;
                break;
            case PowerLevel.Medium:
                arrow.sprite = highSprite;
                currentPowerLevel = PowerLevel.High;
                break;
            case PowerLevel.High:
                arrow.sprite = lowSprite;
                currentPowerLevel = PowerLevel.Low;
                break;
        }
    }
    
}
