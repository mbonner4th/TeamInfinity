using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;

    public void PlaySound(int index)
    {
        //print("playing sound?");
        GetComponent<AudioSource>().PlayOneShot(sounds[index]);
    }

	void Start()
    {
	
	}
	
	void Update()
    {
	
	}
}
