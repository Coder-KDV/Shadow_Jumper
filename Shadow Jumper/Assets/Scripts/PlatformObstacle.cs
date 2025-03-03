using UnityEngine;

public class PlatformObstacle : MonoBehaviour
{
    private BoxCollider2D bcol;

    private SpriteRenderer srender;

    private Color ogColor;

    [SerializeField]
    private bool isPlatformLight = true;

    private void Start()
    {
        bcol = GetComponent<BoxCollider2D>();
        srender = GetComponent<SpriteRenderer>();
        ogColor = srender.color;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Playermovement playerMovement = collision.gameObject.GetComponent<Playermovement>();
            if (playerMovement.isLight && isPlatformLight)
            {
                gameObject.tag = "Ground";
            }
            else if (!playerMovement.isLight)
            {
                bcol.isTrigger = true;
                gameObject.tag = "Untagged";
                srender.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        srender.color = ogColor;
        bcol.isTrigger = false;
    }
}
