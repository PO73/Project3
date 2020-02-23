using UnityEngine;

public class winCase : MonoBehaviour
{
    public AudioSource sound;

    private void OnTriggerEnter2D(Collider2D collision) {
        sound.Play();
        collision.gameObject.GetComponent<playerMain>().MyUIController.showWinCase();
    }
}
