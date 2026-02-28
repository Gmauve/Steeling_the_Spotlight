using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CamManager : MonoBehaviour
{
    private Transform camTransform;
    private float lookAhead = 0.05f;
    private float lerpTime = 0.025f;

    [SerializeField] private Transform player;
    [SerializeField] private float limitMinX;
    [SerializeField] private float limitMaxX;
    [SerializeField] private float limitMinY;
    [SerializeField] private float limitMaxY;

    void Start()
    {
        camTransform = gameObject.transform;
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
        gameObject.transform.position = new Vector3(Mathf.Lerp(camTransform.position.x + lookAhead, player.position.x + lookAhead, lerpTime), Mathf.Lerp(camTransform.position.y, player.position.y, lerpTime * 3), camTransform.position.z);


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
