using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CamController : MonoBehaviour
{
    // Variables pour le positionnement de la camera
    private Transform camTransform;
    private float lookAhead = 0.16f;
    private float lerpTime = 0.16f;

    // Variables des limites de deplacement de la camera
    [SerializeField] private Transform player;
    [SerializeField] private float limitMinX;
    [SerializeField] private float limitMaxX;
    [SerializeField] private float limitMinY;
    [SerializeField] private float limitMaxY;

    void Start()
    {
        camTransform = transform;
    }

    void Update()
    {
        // LookAhead change de valeur selon la direction a laquelle le joueur fait face
        if (PlayerMovement.facingLeft)
        {
            // Regard vers la gauche
            lookAhead = -Mathf.Abs(lookAhead);
        }
        else if (!PlayerMovement.facingLeft)
        {
            // Regard vers la droite
            lookAhead = Mathf.Abs(lookAhead);
        }

        // Systeme de suivi de la camera
        camTransform.position = new Vector3(Mathf.Lerp(
                                                camTransform.position.x + lookAhead, player.position.x + lookAhead, lerpTime * 0.35f),
                                                Mathf.Lerp(camTransform.position.y, player.position.y + 1f, lerpTime),
                                                camTransform.position.z);

        // Limites de mouvements de la camera dans l'axe X
        if (camTransform.position.x < limitMinX)
        {
            camTransform.position = new Vector3(limitMinX, camTransform.position.y, camTransform.position.z);
        }

        if (camTransform.position.x > limitMaxX)
        {
            camTransform.position = new Vector3(limitMaxX, camTransform.position.y, camTransform.position.z);
        }

        // Limites de mouvements de la camera dans l'axe Y
        if (camTransform.position.y < limitMinY)
        {
            camTransform.position = new Vector3(camTransform.position.x, limitMinY, camTransform.position.z);
        }

        if (camTransform.position.y > limitMaxY)
        {
            camTransform.position = new Vector3(camTransform.position.x, limitMaxY, camTransform.position.z);
        }
    }
}
