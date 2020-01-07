using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioSource audioSource;


    public void playSound() {
        audioSource.Play();
    }
    public void setSound(string sound)
    {
        audioSource.clip = Resources.Load<AudioClip>("sound/"+sound);
    }

    void Start() {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}