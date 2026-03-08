using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables de deplacement general
    private float horizontal;
    private float speed = 5f;
    [SerializeField] private ArmTracking ArmTracking;
    [SerializeField] private Rigidbody2D rb;

    // Variables verifiant si le personnage est au sol
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Variables regulant le saut
    private float jumpForce = 20f;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferTimeCounter;
    private float fallSpeed = 0;

    // Variable qui modifie la direction a laquelle le joueur fait face
    public static bool facingLeft = false;

    // Animator du personnage pour l'animer
    [SerializeField] private Animator animator;

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
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
        else if (!IsGrounded())
        {
            // Appliquer les mouvements differemment lorsque dans les airs
            if (rb.linearVelocity.y >= 0)
            {
                rb.AddForceX(horizontal * 2, ForceMode2D.Impulse);
            }
            else if (rb.linearVelocity.y < 0)
            {
                rb.AddForceX(horizontal * 0.8f, ForceMode2D.Impulse);
            }
            
            // Ralentissement horizontal a l'apogee du saut
            if ((rb.linearVelocity.y < 0.5f || rb.linearVelocity.y > -0.5f) && (rb.linearVelocity.x > speed || rb.linearVelocity.x < -speed))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.965f, rb.linearVelocity.y);
            }
        }

        // Appliquer la force de chute
        rb.AddForceY(fallSpeed, ForceMode2D.Impulse);

        // Mouvement vers le grappin gauche
        if (ArmTracking.LAnchor)
        {
            Vector3 forceDirection = ArmTracking.LArm.position - transform.position;
            forceDirection.Normalize();

            forceDirection *= speed;

            rb.AddForce(forceDirection, ForceMode2D.Impulse);
        }

        // Mouvement vers le grappin droit
        if (ArmTracking.RAnchor)
        {
            Vector3 forceDirection = ArmTracking.RArm.position - transform.position;
            forceDirection.Normalize();

            forceDirection *= speed;

            rb.AddForce(forceDirection, ForceMode2D.Impulse);
        }
    }

    /**
    * Fonction regulant le saut
    * incluant une prise d'appuits tardifs et anticipes
    * ainsi que la chute rapide
    * 
    * @param void
    * @returns void
    */
    private void Jump()
    {
        // Prise d'un appuit tardif du saut losrque le joueur tombe d'une plateforme
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Prise d'un appuit anticipe du saut pour plus de precision
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimeCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferTimeCounter -= Time.deltaTime;
        }

        // Saut
        if (jumpBufferTimeCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            jumpBufferTimeCounter = 0;
        }

        // Freiner le saut lorsque la touche est relachee avant la hauteur maximale du saut
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

            coyoteTimeCounter = 0;
        }

        // Chute plus rapide lorsque la touche de chute est appuyee
        if (!IsGrounded())
        {
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && fallSpeed > -3f)
            {
                fallSpeed -= 0.1f;
            }
            else if (fallSpeed < 0)
            {
                fallSpeed += 0.2f;
            }
        }
        else
        {
            // Vitesse de chute annulee lorsque au sol
            fallSpeed = 0;
        }
        // Empecher la vitesse de chute de devenir positive
        if (fallSpeed > 0)
        {
            fallSpeed = 0;
        }
    }

    /**
    * Fonction du sprint
    * modifiant la vitesse du joueur
    * 
    * @param void
    * @returns void
    */
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

    /**
    * Verifie que le joueur est au sol
    * 
    * @param void
    * @returns void
    */
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    /**
    * Tourne le sprite du joueur dans la direction du mouvement
    * 
    * @param void
    * @returns void
    */
    private void Flip()
    {
        if (facingLeft && horizontal > 0f || !facingLeft && horizontal < 0f)
        {
            facingLeft = !facingLeft;
            GetComponent<SpriteRenderer>().flipX = facingLeft;
        }
    }

    /**
    * Fonction qui anime le personnage
    * 
    * @param void
    * @returns void
    */
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
