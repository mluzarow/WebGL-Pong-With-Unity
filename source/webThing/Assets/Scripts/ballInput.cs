using UnityEngine;

[RequireComponent (typeof (ballController))]

public class ballInput : MonoBehaviour {
    // Movement
    float moveSpeed = 3f;  // Movement force
    Vector2 velocity;       // Player velocity

    ballController controller;
    AudioSource audio;

    // Use this for initialization
    void Start () {
        controller = GetComponent <ballController> ();
        audio = GetComponent <AudioSource> ();
    }

    // Update is called once per frame
    void Update () {
        // Check for collisions and update velocity
        if ((controller.collisions.up) || (controller.collisions.down)) {
            audio.Play ();
            moveSpeed += 0.1f;
            velocity.y = -velocity.y;
        }
        // Check for collisions and update velocity
        if ((controller.collisions.left) || (controller.collisions.right)) {
            audio.Play ();
            moveSpeed += 0.1f;
            velocity.x = -velocity.x;
        }

        if (controller.collisions.scoreLeft || controller.collisions.scoreRight) {
            if (controller.collisions.scoreLeft) {
                HUD.scoreComputer += 1;
            } else {
                HUD.scorePlayer += 1;
            }

            if (HUD.scorePlayer < 10 && HUD.scoreComputer < 10) {
                // Reset ball position
                this.transform.position = new Vector2 (0.0f, 0.0f);
                moveSpeed = 3f;
                velocity = -velocity;
            } else {
                this.transform.position = new Vector2 (10.0f, 0.0f);
                velocity = new Vector2 (0.0f, 0.0f);
            }
                
            controller.collisions.reset ();
        }

        velocity.x = Mathf.Sign (velocity.x) * moveSpeed;
        velocity.y = Mathf.Sign (velocity.y) * moveSpeed;
        controller.Move (velocity * Time.deltaTime);
    }
}
