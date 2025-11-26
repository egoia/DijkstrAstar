using UnityEngine;

public class Wall : MonoBehaviour
{
    public float bonusSpeed;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null)
        {
            if (ball.isDashing)
            {
                ball.speed += bonusSpeed;
            }
            else
            {
                Destroy(ball.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null)
        {
            ball.resetDash();
        }
    }
}
