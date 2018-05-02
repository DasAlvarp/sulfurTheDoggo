using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicMan : MonoBehaviour {
    public AudioMixer audioGroup;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        if(GameObject.FindGameObjectWithTag("Singleton"))
        {
            if(gameObject != GameObject.FindGameObjectWithTag("Singleton"))
            {
                Destroy(gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        print(SceneManager.GetActiveScene().name);
		if(SceneManager.GetActiveScene().name != "javier test")
        {
            audioGroup.TransitionToSnapshots(new AudioMixerSnapshot[] { audioGroup.FindSnapshot("faded")}, new float[]{ 1f}, 0f);
        }
        else
        {
            audioGroup.TransitionToSnapshots(new AudioMixerSnapshot[] { audioGroup.FindSnapshot("normal") }, new float[] { 1f }, 0f);
        }
    }
}
