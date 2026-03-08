using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Variable indiquant si le jeu peut etre termine
    public bool MissionFinished = false;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Destroy(gameObject);
    }

    /**
    * Rendre la completion du jeu possible
    * 
    * @param void
    * @returns void
    */
    public void MissionAccomplished()
    {
        // Il est maintenant possible de finir le jeu
        MissionFinished = true;
    }
}
