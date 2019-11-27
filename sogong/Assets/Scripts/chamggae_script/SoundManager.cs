using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public void playSound(string objectName) {
        audioSource = GameObject.Find(objectName).GetComponent<AudioSource> ();
        audioSource.Play();
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
