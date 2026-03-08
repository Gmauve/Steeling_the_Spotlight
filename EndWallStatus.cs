using UnityEngine;

public class EndWallStatus : MonoBehaviour
{
    [SerializeField] private GameObject OrbCheck;

    void Update()
    {
        // Retirer le mur lorsque l'orbe a ete obtenue
        if (GameManager.Instance.MissionFinished)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Rappeler au joueur qu'il a besoin de l'orbe avec une animation
        if (collision.gameObject.CompareTag("Player"))
        {
            OrbCheck.GetComponent<Animator>().SetTrigger("NeedOrb");
        }
    }
}
