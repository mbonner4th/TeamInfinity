using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioClip[] music;
    public AudioSource soundPlayer;
    public AudioSource musicPlayer;
    public int fixedMusicIndex;
    public int musicIndex;

    public void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        musicPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        fixedMusicIndex = (int) PlayerPrefs.GetFloat("fixedMusic", -1);
        soundPlayer.mute = PlayerPrefs.GetInt("muteSound", 0) == 1;
        musicPlayer.mute = PlayerPrefs.GetInt("muteMusic", 0) == 1;
        soundPlayer.volume = PlayerPrefs.GetFloat("volumeSetting", 1.0f);
        musicPlayer.volume = PlayerPrefs.GetFloat("volumeSetting", 1.0f);
        PlayNextMusic();
    }

    public void PlayNextMusic()
    {
        if (fixedMusicIndex != -1) {
            musicPlayer.clip = music[fixedMusicIndex];
            musicPlayer.Play();
            musicPlayer.time = 0.01f;
            return;
        }

        musicPlayer.Stop();
        musicIndex++;
        musicPlayer.clip = music[musicIndex % music.Length];
        musicPlayer.Play();
    }

    public void Update()
    {
        if (musicPlayer.time == 0.0f) {
            PlayNextMusic();
        }
    }

    public void PlaySound(int index)
    {
        soundPlayer.PlayOneShot(sounds[index]);
    }
}
