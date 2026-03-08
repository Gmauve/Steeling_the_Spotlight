using UnityEngine;

public class ArmTracking : MonoBehaviour
{
    public Transform LArm;
    public Transform RArm;

    // Variables de la mecanique de grappin
    [SerializeField] private Camera mainCamera;
    private Vector3 mousePos;
    public bool LArmGrapple, RArmGrapple;
    public bool LAnchor, RAnchor;

    // Variables de positionnement du bras gauche
    private Vector3 LArmOrigin;
    private float LPosX, LPosY, LRotX, LRotY, LRotZ, LScale;

    // Variables de positionnement du bras droit
    private Vector3 RArmOrigin;
    private float RPosX, RPosY, RRotX, RRotY, RRotZ, RScale;

    private float lerpTimeL = 0.16f;
    private float lerpTimeR = 0.16f;
    private float lerpTimeRotation = 0.208f;

    void Update()
    {
        // Repositionnement des bras lorsque le joueur se deplace vers la gauche
        if (PlayerMovement.facingLeft)
        {
            if (!LArmGrapple)
            {
                // Valeurs des variables du bras gauche
                LPosX = -0.88f;
                LPosY = 0.162f;
                LRotX = -203f;
                LRotY = 78.2f;
                LRotZ = 136.8f;
                LScale = -125f;

                LArmOrigin = transform.position;
            }

            if (!RArmGrapple)
            {
                // Valeurs des variables du bras droit
                RPosX = 0.55f;
                RPosY = 0.072f;
                RRotX = -46.6f;
                RRotY = -43.6f;
                RRotZ = -224f;
                RScale = 150f;

                RArmOrigin = transform.position;
            }
        }

        // Repositionnement des bras lorsque le joueur se deplace vers la droite
        if (!PlayerMovement.facingLeft)
        {
            if (!LArmGrapple)
            {
                // Valeurs des variables du bras gauche
                LPosX = 0.82f;
                LPosY = 0.175f;
                LRotX = -150f;
                LRotY = 128f;
                LRotZ = -32f;
                LScale = 125f;

                LArmOrigin = transform.position;
            }

            if (!RArmGrapple)
            {
                // Valeurs des variables du bras droit
                RPosX = -0.54f;
                RPosY = 0.028f;
                RRotX = 106f;
                RRotY = 32f;
                RRotZ = -172f;
                RScale = -150f;

                RArmOrigin = transform.position;
            }
        }


        // Positionnement du bras gauche en grappin
        if (LArmGrapple)
        {
            // Origine de la position pour le suivi de la souris
            LArmOrigin = Vector3.zero;

            // Angle du bras selon sa position par rapport au joueur
            // et la direction a laquelle le joueur fait face
            float LArmRotOffsetY;
            if (LArm.position.x > transform.position.x)
            {
                LArmRotOffsetY = 90f;
            }
            else
            {
                LArmRotOffsetY = -90f;
            }

            float LArmRotOffsetX;
            if (PlayerMovement.facingLeft)
            {
                LArmRotOffsetX = 0;
            }
            else
            {
                LArmRotOffsetX = -180f;
            }

            // Suivi de la souris
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 LDirection = RArm.position - mousePos;

            LPosX = mousePos.x;
            LPosY = mousePos.y;
            LRotX = Quaternion.Euler(LDirection).x + LArmRotOffsetX;
            LRotY = Quaternion.Euler(LDirection).y + LArmRotOffsetY;
            LRotZ = Quaternion.Euler(LDirection).z;
        }

        // Positionnement du bras droit en grappin
        if (RArmGrapple)
        {
            // Origine de la position pour le suivi de la souris
            RArmOrigin = Vector3.zero;

            // Angle du bras selon sa position par rapport au joueur
            // et la direction a laquelle le joueur fait face
            float RArmRotOffsetY;
            if (RArm.position.x > transform.position.x)
            {
                RArmRotOffsetY = 90f;
            }
            else
            {
                RArmRotOffsetY = -90f;
            }

            float RArmRotOffsetX;
            if (PlayerMovement.facingLeft)
            {
                RArmRotOffsetX = -180f;
            }
            else
            {
                RArmRotOffsetX = 0;
            }

            // Suivi de la souris
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 RDirection = RArm.position - mousePos;

            RPosX = mousePos.x;
            RPosY = mousePos.y;
            RRotX = Quaternion.Euler(RDirection).x + RArmRotOffsetX;
            RRotY = Quaternion.Euler(RDirection).y + RArmRotOffsetY;
            RRotZ = Quaternion.Euler(RDirection).z;
        }
    }

    private void FixedUpdate()
    {
        if (!LAnchor)
        {
            // Positionnement du bras gauche
            LArm.SetPositionAndRotation(
                Vector3.Lerp(
                    LArm.position,
                    new Vector3(LArmOrigin.x + LPosX, LArmOrigin.y + LPosY, -2),
                    lerpTimeL),
                Quaternion.Lerp(
                    LArm.rotation,
                    Quaternion.Euler(LRotX, LRotY, LRotZ),
                    lerpTimeRotation));
            LArm.localScale = new Vector3(LScale, LScale, LScale);
        }

        if (!RAnchor)
        {
            // Positionnement du bras droit
            RArm.SetPositionAndRotation(
                Vector3.Lerp(
                    RArm.position,
                    new Vector3(RArmOrigin.x + RPosX, RArmOrigin.y + RPosY, -2),
                    lerpTimeR),
                Quaternion.Lerp(
                    RArm.rotation,
                    Quaternion.Euler(RRotX, RRotY, RRotZ),
                    lerpTimeRotation));
            RArm.localScale = new Vector3(RScale, RScale, RScale);
        }
    }

    /**
    * Activation du grappin gauche
    * 
    * @param void
    * @returns void
    */
    public void GrappleLArm()
    {
        LArmGrapple = true;
        lerpTimeL = 0.13f;

        // Detacher le bras du joueur pour qu'il reste sur la souris
        LArm.SetParent(null);
    }

    /**
    * Desactivation du grappin gauche
    * 
    * @param void
    * @returns void
    */
    public void StopGrappleLArm()
    {
        LArmGrapple = false;
        LAnchor = false;
        lerpTimeL = 0.16f;

        // Ratacher le bras au joueur pour qu'il le suive
        LArm.SetParent(transform);
    }

    /**
    * Changement vers le statut ancre du grappin gauche
    * 
    * @param void
    * @returns void
    */
    public void AnchorLArm()
    {
        LAnchor = true;
    }

    /**
    * Activation du grappin droit
    * 
    * @param void
    * @returns void
    */
    public void GrappleRArm()
    {
        RArmGrapple = true;
        lerpTimeR = 0.13f;

        // Detacher le bras du joueur pour qu'il reste sur la souris
        RArm.SetParent(null);
    }

    /**
    * Desactivation du grappin droit
    * 
    * @param void
    * @returns void
    */
    public void StopGrappleRArm()
    {
        RArmGrapple = false;
        RAnchor = false;
        lerpTimeR = 0.16f;

        // Ratacher le bras au joueur pour qu'il le suive
        RArm.SetParent(transform);
    }

    /**
    * Changement vers le statut ancre du grappin droit
    * 
    * @param void
    * @returns void
    */
    public void AnchorRArm()
    {
        RAnchor = true;
    }
}
