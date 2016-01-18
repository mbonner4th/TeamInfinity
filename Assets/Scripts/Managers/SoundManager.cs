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
        musicPlayer = GetComponentInChildren<AudioSource>();
        fixedMusicIndex = PlayerPrefs.GetInt("fixedMusic", -1);
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
        }
        musicPlayer.Stop();
        musicIndex++;
        musicPlayer.clip = music[musicIndex % music.Length];
        musicPlayer.Play();
    }

    public void Update()
    {
        if (!musicPlayer.isPlaying) {
            PlayNextMusic();
        }
    }

    public void PlaySound(int index)
    {
        //print("playing sound?");
        soundPlayer.PlayOneShot(sounds[index]);
    }
}
