using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor(inspector)

    // CACHE - e.g. references for readability or speed.  Caching a reference to a component

    // STATE - private instance (member) variables

    [SerializeField] float rocketThrust = 1000f;
    [SerializeField] float rocketRotation = 100f;  
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;   

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopAudio();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            LeftRotation();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RightRotation();
        }
    }

    void StartThrusting()
    {
        rigidBody.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
        mainThrustParticles.Play();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopAudio()
    {
        audioSource.Stop();
    }

    void LeftRotation()
    {
        leftBoosterParticles.Play();
        RotationThrust(rocketRotation);
    }

    void RightRotation()
    {
        rightBoosterParticles.Play();
        RotationThrust(-rocketRotation);
    }

    void RotationThrust(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidBody.freezeRotation = false; // unfreesing rotation so the physics system can takeover
    }
}



