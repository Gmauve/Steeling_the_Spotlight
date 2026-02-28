using UnityEngine;

public class MusicTransitionDetector : MonoBehaviour
{
    [SerializeField] private MusicTrackManager mtManager;

    void Start()
    {
        // Le jeu commence a l'exterieur
        mtManager.SwitchClipOutside();
    }

    // Changer de musique selon le "trigger" active
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Outside"))
        {
            if (mtManager.currentClip != mtManager.outside)
            {
                mtManager.SwitchClipOutside();
            }
        }
        else if (collision.CompareTag("Museum"))
        {
            if (mtManager.currentClip != mtManager.museum)
            {
                mtManager.SwitchClipMuseum();
            }
        }
        else if (collision.CompareTag("SpotLight"))
        {
            if (mtManager.currentClip != mtManager.spotLight)
            {
                mtManager.SwitchClipSpotLight();
            }
        }
    }
}
