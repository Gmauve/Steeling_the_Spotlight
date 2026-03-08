using UnityEngine;

public class MusicTrackManager : MonoBehaviour
{
    // Variables de controle des sources audio
    public AudioSource[] _audioSources;
    private int audioToggle;

    // Variables des clips audio ainsi que le controle de ceux-ci
    public AudioClip currentClip;
    public AudioClip outside;
    public AudioClip museum;
    /*
    // Musique pour une eventuelle continuation du jeu
    public AudioClip spotLight;
    */

    // Variables pour calculer le moment pour faire jouer chaque musique
    private double musicDuration;
    private double goalTime;

    // Variable de changement de musique
    private bool changeMusic;

    void Start()
    {
        audioToggle = 0;

        goalTime = AudioSettings.dspTime;

        changeMusic = false;
    }

    void Update()
    {
        // Jouer le prochain clip audio lorsque le precedent est termine
        if (AudioSettings.dspTime > goalTime - 1)
        {
            PlayScheduledClip();
        }

        // Activer l'effet de transition
        if (_audioSources[0].clip != currentClip && _audioSources[1].clip != currentClip)
        {
            changeMusic = true;
            Invoke("NextMusic", 1.25f);
        }
    }

    private void FixedUpdate()
    {
        // Transition de volume
        if (changeMusic)
        {
            // Volume diminue
            _audioSources[0].volume -= 0.025f;
            _audioSources[1].volume -= 0.025f;
        }
        else if (!changeMusic && _audioSources[0].volume != 1 && _audioSources[1].volume != 1)
        {
            // Volume augmente
            _audioSources[0].volume += 0.025f;
            _audioSources[1].volume += 0.025f;
        }
    }

    /**
    * Passer directement au clip audio suivant en modifiant "goalTime"
    * et en arretant les sources audio momentanement
    * tout en activant l'augmentation de volume avec la variable "changeMusic"
    * pour une transition fluide
    * 
    * @param void
    * @returns void
    */
    private void NextMusic()
    {
        // Passer au prochain clip audio
        if (changeMusic)
        {
            _audioSources[0].Stop();
            _audioSources[1].Stop();
            goalTime = AudioSettings.dspTime;
        }
        changeMusic = false;
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
        if (currentClip == outside)
        {
            goalTime -= 2.182;
        }
        else if (currentClip == museum)
        {
            goalTime -= 2.152;
        }
        /*
        // Musique pour une eventuelle continuation du jeu
        else if (currentClip == spotLight)
        {
            goalTime -= 3;
        }
        */

        audioToggle = 1 - audioToggle;
    }

    /**
    * Changer le clip audio pour "outside"
    * 
    * @param void
    * @returns void
    */
    public void SwitchClipOutside()
    {
        currentClip = outside;
    }

    /**
    * Changer le clip audio pour "museum"
    * 
    * @param void
    * @returns void
    */
    public void SwitchClipMuseum()
    {
        currentClip = museum;
    }

    // Pour une eventuelle continuation du jeu
    /**
    * Changer le clip audio pour "spotLight"
    * 
    * @param void
    * @returns void
    */
    /*
    public void SwitchClipSpotLight()
    {
        currentClip = spotLight;
    }
    */
}
