using UnityEngine;

public class TitleMusicManager : MonoBehaviour
{
    // Variables de controle des sources audio
    public AudioSource[] _audioSources;
    private int audioToggle;

    // Variables des clips audio ainsi que le controle de ceux-ci
    private AudioClip currentClip;
    [SerializeField] private AudioClip title;

    // Variables pour calculer le moment pour faire jouer chaque musique
    private double musicDuration;
    private double goalTime;

    void Start()
    {
        audioToggle = 0;

        currentClip = title;

        goalTime = AudioSettings.dspTime;
    }

    void Update()
    {
        // Jouer le prochain clip audio lorsque le precedent est termine
        if (AudioSettings.dspTime > goalTime - 1)
        {
            PlayScheduledClip();
        }
    }

    /**
    * Faire jouer le prochain clip audio
    * a un moment determine par la variable "goalTime"
    * 
    * @param void
    * @returns void
    */
    private void PlayScheduledClip()
    {
        // Faire jouer le clip audio actuel
        _audioSources[0].clip = currentClip;
        _audioSources[1].clip = currentClip;
        _audioSources[audioToggle].PlayScheduled(goalTime);

        musicDuration = (double)currentClip.samples / currentClip.frequency;
        goalTime += musicDuration;

        // Offset pour la reprise de la meme musique
        if (currentClip == title)
        {
            goalTime -= 0.05;
        }

        audioToggle = 1 - audioToggle;
    }
}
