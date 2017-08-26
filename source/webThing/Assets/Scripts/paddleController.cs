using UnityEngine;

public class paddleController : raycaster {
    public collisionInfo collisions;

    /// <summary>
	/// The collision mask for colliding with the ground layer.</summary>
	public LayerMask collisionMask;

    public override void Start () {
        // Must be override to use base start method
        base.Start ();
    }

    public void Move (Vector2 velocity) {
        updateRaycasts ();
        collisions.reset ();

        //velocity in the y plane is NOT 0; check for collisions
        if (velocity.y != 0) {
            verticalCollisions (ref velocity);
        }

        //Move player with updated velocity value
        transform.Translate (velocity);
    }

    void verticalCollisions (ref Vector2 velocity) {
        float directionY = Mathf.Sign (velocity.y);
        float rayLength = Mathf.Abs (velocity.y) + inset;

        for (int i = 0; i < verticalRays; i++) {
            Vector2 rayOrigin = (directionY == -1) ? raycasts.bottomLeft : raycasts.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit) {
                velocity.y = (hit.distance - inset) * directionY;
                rayLength = hit.distance;

                //Set collision flag for collision up or down
                collisions.up = (directionY == 1);
                collisions.down = (directionY == -1);
            }
        }
    }

    //struct for telling which direction you are colliding in
    public struct collisionInfo {
        public bool up;
        public bool down;

        public void reset () {
            up = false;
            down = false;
        }
    }
}
