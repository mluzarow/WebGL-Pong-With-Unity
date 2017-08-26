using System.Collections;
using UnityEngine;

public class ballController : raycaster {
    public collisionInfo collisions;
    
    /// <summary>
	/// The collision mask for colliding with the ground layer.
    /// </summary>
	public LayerMask collisionMask;
    /// <summary>
    /// The collision mask for colliding with the goals on either side.
    /// </summary>
    public LayerMask pointMask;

    public override void Start () {//Must be override to use base start method
        // Change the number of rays as we do not need all 8 for a tiny ball
        horizontalRays = 4;
        verticalRays = 4;
        base.Start ();
    }

    public void Move (Vector2 velocity) {
        updateRaycasts ();
        collisions.reset ();

        //velocity in the x plane is NOT 0; check for collisions
        horizontalCollisions (ref velocity);
        //velocity in the y plane is NOT 0; check for collisions
        verticalCollisions (ref velocity);

        //Move player with updated velocity value
        transform.Translate (velocity);
    }

    //velocity in the x plane is NOT 0; check for collisions
    void horizontalCollisions (ref Vector2 velocity) {
        float directionX = Mathf.Sign (velocity.x);
        float rayLength = Mathf.Abs (velocity.x) + inset;

        //Extend rays to detect walls while not moving towards them
        if (Mathf.Abs (velocity.x) < inset) {
            rayLength = 2 * inset;
        }

        for (int i = 0; i < horizontalRays; i++) {
            //If directionX is negative (moving left), raycast from the bottom left
            //If directionX is positive (moving right), raycast from the bottom right
            Vector2 rayOrigin = (directionX == -1) ? raycasts.bottomLeft : raycasts.bottomRight;
            //Add horizontal ray spacing to raycast position
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            RaycastHit2D score = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, pointMask);

            Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit) {
                if (hit.distance == 0) {
                    continue;
                }

                velocity.x = (hit.distance - inset) * directionX;
                rayLength = hit.distance;

                //Set collision flag for collision left or right
                collisions.left = (directionX == -1);
                collisions.right = (directionX == 1);
            } else if (score) {
                // On the left side, scored on player
                
                if (this.transform.position.x < 0) {
                    collisions.scoreLeft = true;
                } else {
                    collisions.scoreRight = true;
                }

                //if (HUD.scorePlayer < 10 && HUD.scoreComputer < 10) {
                //    // Reset ball position
                //    
                //} else {
                //    this.transform.position = new Vector2 (10.0f, 0.0f);
                //    velocity = new Vector2 (0.0f, 0.0f);
                //}
            }
        }
    }

    void verticalCollisions (ref Vector2 velocity) {
        float directionY = Mathf.Sign (velocity.y);
        float rayLength = Mathf.Abs (velocity.y) + inset;

        for (int i = 0; i < verticalRays; i++) {
            Vector2 rayOrigin = (directionY == -1) ? raycasts.bottomLeft : raycasts.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
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
        public bool left;
        public bool right;
        public bool scoreLeft;
        public bool scoreRight;

        public void reset () {
            up = false;
            down = false;
            left = false;
            right = false;
            scoreLeft = false;
            scoreRight = false;
        }
    }
}
