using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    AudioSource as1;
    AudioSource as2;

    public AudioClip[] musics;
    
    
    

    int mainChannel = 1;

    public GameController controller;
    public float lerpSpeed = 1f;

    // Use this for initialization
    void Start () {
        var source = GetComponents<AudioSource>();
        as1 = source[0];
        as2 = source[1];

        as2.volume = 0;

        as1.clip = musics[0];
        as1.Play();
       
	}
	
    public void changeMusic()
    {
        var session = (int)(controller.currentSesion);
        var music=musics[session];

        if (mainChannel == 1)
        {
            as2.clip = music;
            mainChannel = 2;
            StartCoroutine(lerpAudioSource(as2, as1));
        }
        else
        {
            as1.clip = music;
            mainChannel = 1;
            StartCoroutine(lerpAudioSource(as1, as2));
        }
    }

    IEnumerator lerpAudioSource(AudioSource inSource, AudioSource outSource)
    {
        inSource.Play();

        var r = 0f;
        while (r < 1f)
        {
            
            r += Time.deltaTime * lerpSpeed;
            inSource.volume = Mathf.Lerp(0, 100, r);
            outSource.volume = Mathf.Lerp(100, 0, r);
            
            yield return null;
        }
        outSource.Stop();

        yield break;
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void createSoundEffect(AudioClip clip)
    {
        var source=gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        Destroy(source, clip.length + 1);
    }
}
