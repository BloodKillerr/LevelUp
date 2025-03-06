using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Footsteps : MonoBehaviour
{
    public CharacterController charController;

    public AudioSource audioSource;

    public float minV;
    public float maxV;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (charController.isGrounded && charController.velocity.magnitude > 2f && audioSource.isPlaying == false)
        {
            audioSource.volume = Random.Range(minV, maxV);
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.Play();
        }
    }
}
