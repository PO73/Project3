using UnityEngine;

public class orbitPull : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private GameObject spawnPoint;

    private void Awake() {
        myRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        spawnPoint = GameObject.FindGameObjectsWithTag("Respawn")[0];
    }

    private Vector3 pullBallIn(Rigidbody2D playerRigidbody) {
        Vector3 pointToMe = myRigidbody.position - playerRigidbody.position;
        float distance = pointToMe.magnitude;

        float appliedForce = (playerRigidbody.mass * myRigidbody.mass) / Mathf.Pow(distance, 2.0f);
        return pointToMe.normalized * appliedForce;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (string.Compare("Player", collision.transform.tag) == 0) {
            if (!collision.gameObject.GetComponent<playerMain>().StatusAffectEnlarge) {
                Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                playerRigidbody.AddForce(pullBallIn(playerRigidbody));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (string.Compare("Player", collision.transform.tag) == 0) {
            if (!collision.gameObject.GetComponent<playerMain>().StatusAffectEnlarge) {
                collision.gameObject.transform.position = spawnPoint.transform.position;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (string.Compare("Player", collision.transform.tag) == 0) {
            if (!collision.gameObject.GetComponent<playerMain>().StatusAffectEnlarge) {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
            }
        }
    }
}
