using UnityEngine;

[RequireComponent (typeof (paddleController))]

public class computerInput : MonoBehaviour {
    //Movement
    float moveSpeed = 3f;  // Movement force
    Vector2 velocity;       // Player velocity

    public GameObject ball;

    public static Vector2 input;

    paddleController controller;

    // Use this for initialization
    void Start () {
        controller = GetComponent<paddleController> ();
    }

    // Update is called once per frame
    void Update () {
        if (ball.transform.position.y > this.transform.position.y) {
            input = new Vector2 (0, 1);
        } else {
            input = new Vector2 (0, -1);
        }

        // Check for collisions and update velocity
        if ((controller.collisions.up) || (controller.collisions.down)) {
            velocity.y = 0f;
        }

        velocity.y = input.y * moveSpeed;
        controller.Move (velocity * Time.deltaTime);
    }
}