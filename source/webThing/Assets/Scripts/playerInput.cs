using UnityEngine;

[RequireComponent (typeof (paddleController))]

public class playerInput : MonoBehaviour {
    //Movement
    float moveSpeed = 7.5f;  // Movement force
    Vector2 velocity;       // Player velocity

    public static Vector2 input;

    paddleController controller;

    // Use this for initialization
    void Start () {
        controller = GetComponent <paddleController> ();
    }
	
	// Update is called once per frame
	void Update () {
        // Get raw direction - Keyboard W for up S for down
        input = new Vector2 (0, (Input.GetKey (KeyCode.W) ? 1 : 0) + (Input.GetKey (KeyCode.S) ? -1 : 0));

        // Check for collisions and update velocity
        if ((controller.collisions.up) || (controller.collisions.down)) {
            velocity.y = 0f;
        }

        velocity.y = input.y * moveSpeed;
        controller.Move (velocity * Time.deltaTime);
    }
}
