using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusViewer : MonoBehaviour
{
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject credits;

    /**
    * Commencer le jeu
    * 
    * @param void
    * @returns void
    */
    public void StartGame()
    {
        GameManager.Instance.MissionFinished = false;
        SceneManager.LoadScene("MainArea");
    }

    /**
    * Faire apparaitre le menu des controles
    * 
    * @param void
    * @returns void
    */
    public void ShowControls()
    {
        controls.SetActive(true);
    }

    /**
    * Refermer le menu des controles
    * 
    * @param void
    * @returns void
    */
    public void HideControls()
    {
        controls.SetActive(false);
    }

    /**
    * Faire apparaitre les credits
    * 
    * @param void
    * @returns void
    */
    public void ShowCredits()
    {
        credits.SetActive(true);
    }

    /**
    * Refermer les credits
    * 
    * @param void
    * @returns void
    */
    public void HideCredits()
    {
        credits.SetActive(false);
    }

    /**
    * Quitter le jeu
    * 
    * @param void
    * @returns void
    */
    public void QuitGame()
    {
        Application.Quit();
    }
}
