using UnityEngine;

public class EnemyPatrolMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float idleDuration = 0f; // Updated to 3 seconds
    public Transform leftPoint;
    public Transform rightPoint;

    private Animator animator;

    private bool movingRight = true;
    private bool isIdle = false;
    private float idleTimer;

    private Vector3 originalScale;

    void Start()
    {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;

        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);
    }

    void Update()
    {
        if (isIdle)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                isIdle = false;
                movingRight = !movingRight;

                // Flip scale immediately on direction change
                Vector3 newScale = originalScale;
                newScale.x *= movingRight ? 1f : -1f;
                transform.localScale = newScale;

                // Start walking animation right after flip
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", true);
            }
        }
    }

    void FixedUpdate()
    {
        if (isIdle) return;

        float moveDir = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * moveDir * moveSpeed * Time.fixedDeltaTime);

        if (movingRight && transform.position.x >= rightPoint.position.x)
            StopAndIdle(rightPoint.position.x);
        else if (!movingRight && transform.position.x <= leftPoint.position.x)
            StopAndIdle(leftPoint.position.x);
    }

    void StopAndIdle(float clampX)
    {
        isIdle = true;
        transform.position = new Vector2(clampX, transform.position.y);
    }
}
