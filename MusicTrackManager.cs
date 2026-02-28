using UnityEngine;

public class MusicTrackManager : MonoBehaviour
{
    public AudioSource[] _audioSources;
    private int audioToggle;

    public AudioClip currentClip;
    public AudioClip outside;
    public AudioClip museum;
    public AudioClip spotLight;

    private double musicDuration;
    private double goalTime;

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

        // Transition de volume
        if (changeMusic)
        {
            // Volume diminue
            _audioSources[0].volume -= 0.004f;
            _audioSources[1].volume -= 0.004f;
        }
        else if (!changeMusic && _audioSources[0].volume != 1 && _audioSources[1].volume != 1)
        {
            // Volume augmente
            _audioSources[0].volume += 0.004f;
            _audioSources[1].volume += 0.004f;
        }
    }

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
        else if (currentClip == spotLight)
        {
            goalTime -= 3;
        }

        audioToggle = 1 - audioToggle;
    }

    // Changer le clip audio pour "outside"
    public void SwitchClipOutside()
    {
        currentClip = outside;
    }

    // Changer le clip audio pour "museum"
    public void SwitchClipMuseum()
    {
        currentClip = museum;
    }

    // Changer le clip audio pour "spotLight"
    public void SwitchClipSpotLight()
    {
        currentClip = spotLight;
    }
}
