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
                ball.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null)
        {
            ball.resetDash();
            GetComponent<Highlighter>().Highlight(true);
        }
    }
}
