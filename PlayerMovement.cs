using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;
    private float jumpForce = 18f;
    private bool jumpBuffer = false;
    public static bool facingLeft = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    [SerializeField] private ArmTracking ArmTracking;

    void Update()
    {
        // Mouvements horizontals du joueur
        horizontal = Input.GetAxisRaw("Horizontal");

        Jump();

        Sprint();

        Flip();

        Animate();
    }

    private void FixedUpdate()
    {
        // Appliquer les mouvements du joueur
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Mouvement vers le grappin gauche
        if (ArmTracking.LAnchor)
        {
            Vector3 forceDirection = ArmTracking.LArm.position - gameObject.transform.position;
            forceDirection.Normalize();

            forceDirection *= speed * 2;

            rb.AddForce(forceDirection);
        }

        // Mouvement vers le grappin droit
        if (ArmTracking.RAnchor)
        {
            Vector3 forceDirection = ArmTracking.RArm.position - gameObject.transform.position;
            forceDirection.Normalize();

            forceDirection *= speed * 2;

            rb.AddForce(forceDirection);
        }
    }

    // Fonction du saut
    private void Jump()
    {
        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() || jumpBuffer && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Prise d'un appuit anticipe du saut
        if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded())
        {
            StartCoroutine("JumpBuffer");
        }

        // Freiner le saut lorsque la touche est relachee avant la hauteur maximale du saut
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    // Coroutine permettant de sauter avec plus de precision (saut anticipe)
    private IEnumerator JumpBuffer()
    {
        jumpBuffer = true;
        yield return new WaitForSeconds(0.2f);
        jumpBuffer = false;
    }

    // Fonction du sprint
    private void Sprint()
    {
        // Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            speed *= 2f;
        }

        // Fin de sprint
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed /= 2f;
        }
    }

    // Fonction pour verifier que le joueur est au sol
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Fonction pour tourner le sprite du joueur dans la direction du mouvement
    private void Flip()
    {
        if (facingLeft && horizontal > 0f || !facingLeft && horizontal < 0f)
        {
            facingLeft = !facingLeft;
            gameObject.GetComponent<SpriteRenderer>().flipX = facingLeft;
        }
    }

    // Fonction pour animer le personnage
    private void Animate()
    {
        // Animation de marche
        if (IsGrounded())
        {
            if (horizontal != 0f)
            {
                animator.SetBool("walk", true);
            }
            else
            {
                animator.SetBool("walk", false);
            }
        }
    }
}
