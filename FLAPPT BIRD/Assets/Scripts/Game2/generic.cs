using UnityEngine;

static public class generic
{
    static public void LimitYVelocity(float limit, Rigidbody2D rb)
    {
        int gravityMultiplier = (int)(Mathf.Abs(rb.gravityScale) / rb.gravityScale);
        if (rb.velocity.y * -gravityMultiplier > limit)
            rb.velocity = Vector2.up * -limit * gravityMultiplier;
    }
    static public void createGamemode(Rigidbody2D rb, MOVEMENT host, bool onGroundRequired, float initalVelocity, float gravityScale, bool canHold = false, bool flipOnClick = false, float rotationMod = 0, float yVelocityLimit = Mathf.Infinity)
    {
        if (!Input.GetMouseButton(0) || canHold && host.OnGround())
            host.clickProcessed = false;

        rb.gravityScale = gravityScale * host.Gravity;
        LimitYVelocity(yVelocityLimit, rb);

        if (Input.GetMouseButton(0))
        {
            if (host.OnGround() && !host.clickProcessed || !onGroundRequired && !host.clickProcessed)
            {
                host.clickProcessed = true;
                rb.velocity = Vector2.up * initalVelocity * host.Gravity;
                host.Gravity *= flipOnClick ? -1 : 1;
            }
        }
    }
}