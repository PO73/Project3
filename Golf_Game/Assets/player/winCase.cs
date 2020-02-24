using UnityEngine;

public class winCase : MonoBehaviour
{
    public AudioSource sound;
    public int maxBallSpeed = 6;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float ballSpeed = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        if (ballSpeed < maxBallSpeed)
        {
            sound.Play();
            collision.gameObject.GetComponent<playerMain>().MyUIController.showWinCase();
            Destroy(collision.gameObject);
        }
    }
}
