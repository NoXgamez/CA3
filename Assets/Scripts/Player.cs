using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 checkpointPosition;

    #region Existing Variables
    Rigidbody2D body;
    float horizontal;
    Vector2 horizontalForce;
    Vector2 verticalForce;
    bool isOnGround;
    Vector2 stoppedVelocity;
    float clampedXVelocity;
    Vector2 clampedVelocity;
    float MovementSpeed = 800;
    float AirMovementSpeed = 200;
    float JumpForce = 7;
    float MaxMovementSpeed = 10;
    bool isAttacking = false;
    SpriteRenderer spriteRenderer;
    Animator animator;
    #endregion

    public GameObject AttachPoint;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = AttachPoint.GetComponent<Animator>();

        verticalForce.y = JumpForce;
        checkpointPosition = transform.position;
    }

    void Update()
    {
        UpdateMovement();
        UpdateCharacterDirection();
        UpdateAttack();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    void UpdateAttack()
    {
        if(Input.GetButtonDown("Fire1") && !isAttacking)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("swing"))
            {
                animator.Play("swing");
                Invoke("ResetAttack", animator.GetCurrentAnimatorStateInfo(0).length);
                isAttacking = true;
            }
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
        animator.Play("idle");
    }

    void UpdateCharacterDirection()
    {
        if (horizontal > 0)
        {
            transform.localScale= new Vector3(1, 1, 1);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void UpdateMovement() 
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            if (isOnGround)
            {
                horizontalForce.x = horizontal * MovementSpeed * Time.deltaTime;
            }
            else
            {
                horizontalForce.x = horizontal * AirMovementSpeed * Time.deltaTime;
            }

            body.AddForce(horizontalForce);
        }
        else
        {
            stoppedVelocity.y = body.velocity.y;
            stoppedVelocity.x = body.velocity.x * 0.75f ;

            body.velocity = stoppedVelocity;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            body.AddForce(verticalForce, ForceMode2D.Impulse);
        }

        clampedXVelocity = Vector2.ClampMagnitude(body.velocity, MaxMovementSpeed).x;
        clampedVelocity.x = clampedXVelocity;
        clampedVelocity.y = body.velocity.y;

        body.velocity = clampedVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnGround(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckIfOnGround(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CheckIfOnGround(collision);
    }

    void CheckIfOnGround(Collision2D collision)
    {
        if (!isOnGround)
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];
                //how close does the normal match the up direction
                float dot = Vector2.Dot(contact.normal, Vector2.up);
                isOnGround = dot >= 0.5f;
            }
            else
            {
                isOnGround = false;
            }
    }
}
