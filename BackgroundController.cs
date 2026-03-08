using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Calculer la distance de deplacement du fond selon la camera
        Mathf.Clamp(parallaxEffect, 0, 1);
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        // Appliquer l'effet parallax
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // Si le fond atteint la fin de sa taille, repositionner celui-ci pour un defilement infini
        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
