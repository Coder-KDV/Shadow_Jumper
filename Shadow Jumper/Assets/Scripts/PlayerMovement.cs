using UnityEngine;
using System.Collections;

public class Playermovement : MonoBehaviour
{
    public string inputAxis;

    private bool isJumping = false;

    private bool isSprinting = false;

    private bool jumpInPlace = false;

    private bool isGrounded = false;

    public bool isShadow = false;

    public bool isLight = true;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip longJumpSound;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float jumpForce;

    private AudioSource audioSource;

    Rigidbody2D rbody;
    Animator anim;
    SpriteRenderer rend;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckForm();
        CheckFall();
        if (isGrounded)
        {
            CheckPlayerMovement();
        }
    }

    private void CheckPlayerMovement()
    {
        float move = Input.GetAxis(inputAxis) * moveSpeed * Time.deltaTime;

        if (move == 0 && isJumping == false && isGrounded)
        {
            isSprinting = false;
            anim.SetFloat("Speed", 0);
            CheckJump(move);
        }
        else
        {
            if (isJumping == false && isGrounded)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StartPlayerRun(move);
                    isSprinting = true;
                }
                else
                {
                    StartPlayerWalk(move);
                    isSprinting = false;
                }
                RotatePlayer(move);
            }
        }
    }

    private void CheckJump(float move)
    {
        jumpInPlace = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            anim.SetBool("Jumping", true);
            anim.SetTrigger("Jump");
            float ForceX = 52f, ForceY = 60f;

            if (isSprinting == true)
            {
                ForceX = 80f;
                ForceY = 80f;
            }

            if (move == 0)
            {
                jumpInPlace = true;
            }

            if (transform.localScale.x < 0 && !jumpInPlace)
            {
                rbody.AddForce(new Vector2(-ForceX, ForceY), ForceMode2D.Impulse);
                if (ForceX == 80f)
                {
                    PlaySound(longJumpSound);
                }
                else
                {
                    PlaySound(jumpSound);
                }
            }
            else if (jumpInPlace)
            {
                rbody.AddForce(new Vector2(0, ForceY * 1.5f), ForceMode2D.Impulse);
                PlaySound(jumpSound);
            }
            else if (transform.localScale.x > 0 && !jumpInPlace)
            {
                rbody.AddForce(new Vector2(ForceX, ForceY), ForceMode2D.Impulse);
                if (ForceX == 80f)
                {
                    PlaySound(longJumpSound);
                }
                else
                {
                    PlaySound(jumpSound);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Jumping", false);
            isJumping = false;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            isGrounded = false;
        }
    }

    private void StartPlayerRun(float move)
    {
        CheckJump(move);
        move *= 1.5f;
        anim.SetFloat("Speed", 0.51f);
        transform.Translate(move, 0, 0);
    }

    private void StartPlayerWalk(float move)
    {
        CheckJump(move);
        anim.SetFloat("Speed", 0.11f);
        transform.Translate(move, 0, 0);
    }

    private void RotatePlayer(float move)
    {
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
    }

    private void CheckFall()
    {
        if (isJumping)
        {
            anim.SetBool("Jumping", true);
        }
        else
        {
            anim.SetBool("Jumping", false);
        }
    }

    private void CheckForm()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isShadow)
            {
                rend.color = Color.white;
                isShadow = false;
                isLight = true;
            }
            else if (isLight)
            {
                rend.color = new Color32(80, 80, 80, 255);
                isLight = false;
                isShadow = true;
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
