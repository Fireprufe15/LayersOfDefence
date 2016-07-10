using UnityEngine;
using System.Collections;

public class startMusicGame : MonoBehaviour {
    public AudioClip song1;
    public AudioClip song2;
    AudioSource speaker;
	// Use this for initialization
	void Start () {
        speaker = gameObject.GetComponent<AudioSource>();
        switch (Random.Range(1,3))
        {
            case 1:
                speaker.clip = song1;                
                break;
            case 2:
                speaker.clip = song2;
                break;
            default:
                break;
        }
        speaker.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (!speaker.isPlaying)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    speaker.clip = song1;
                    break;
                case 2:
                    speaker.clip = song2;
                    break;
                default:
                    break;
            }
            speaker.Play();
        }
	}
}
