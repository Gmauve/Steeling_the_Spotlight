using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;
    private float jumpForce = 18f;
    private bool facingLeft = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    void Update()
    {
        // Mouvements horizontals du joueur
        horizontal = Input.GetAxisRaw("Horizontal");

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Freiner le saut lorsque la touche est retiree avant la hauteur maximale du saut
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();

        Animate();
    }

    private void FixedUpdate()
    {
        // Appliquer les mouvements du joueur
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
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
