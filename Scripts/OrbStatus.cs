using UnityEngine;
using UnityEngine.UI;

public class OrbStatus : MonoBehaviour
{
    [SerializeField] private GameObject OrbCheck;
    [SerializeField] private Sprite OrbCheckDone;

    /**
    * Evenements enchaines par la collection de l'orbre
    * 
    * @param void
    * @returns void
    */
    public void OrbCollect()
    {
        // Activer l'animation de collecte de l'orbe
        GetComponent<Animator>().SetTrigger("OrbCollected");

        // Confirmer que le jeu peu etre fini
        GameManager.Instance.MissionAccomplished();

        // Mettre a jour le UI pour demontrer que l'orbe est obtenue
        OrbCheck.GetComponent<Image>().sprite = OrbCheckDone;
    }

    /**
    * Destruction de l'orbe lorsque l'animation de collecte est terminee
    * 
    * @param void
    * @returns void
    */
    public void DestroyOrb()
    {
        Destroy(gameObject);
    }
}
