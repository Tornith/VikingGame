using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour {
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other) {
        audioSource.Play();
    }
}
