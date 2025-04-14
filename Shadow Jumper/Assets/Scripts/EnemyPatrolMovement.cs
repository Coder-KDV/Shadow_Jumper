using UnityEngine;
using System.Collections;

public class EnemyPatrolMovement : MonoBehaviour
{
    [SerializeField]
    private AudioClip footstepsSound;

    public float moveSpeed = 0.5f;
    public float idleDuration = 0f;
    public Transform leftPoint;
    public Transform rightPoint;

    private Animator animator;

    private bool movingRight = true;
    private bool isIdle = false;
    private float idleTimer;

    private Vector3 originalScale;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;

        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayFootstepsWhileWalking());
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

    private IEnumerator PlayFootstepsWhileWalking()
    {
        while (true)
        {
            if (!isIdle && animator.GetBool("isWalking") && footstepsSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(footstepsSound);
            }
            yield return new WaitForSeconds(.8f);
        }
    }
}
