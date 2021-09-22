using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    
    [SerializeField] float mainThrust =  1000f; 
    [SerializeField] float sideThrust =  100f; 
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftFrontThrusterParticles;
    [SerializeField] ParticleSystem leftBackThrusterParticles;
    [SerializeField] ParticleSystem rightFrontThrusterParticles;
    [SerializeField] ParticleSystem rightBackThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            audioSource.Stop();
            mainThrusterParticles.Stop();
        }
    }

    void StartThrust()
    {
        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void ProcessRotation()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            ThrustLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            ThrustRight();

        }
        else
        {
            StopSideThrustParticles();
        }
    }

    private void ThrustLeft()
    {
        leftBackThrusterParticles.Stop();
        leftFrontThrusterParticles.Stop();
        ApplyRotation(sideThrust);
        if (!rightBackThrusterParticles.isPlaying && !rightFrontThrusterParticles.isPlaying)
        {
            rightBackThrusterParticles.Play();
            rightFrontThrusterParticles.Play();
        }
    }

    private void ThrustRight()
    {
        rightBackThrusterParticles.Stop();
        rightFrontThrusterParticles.Stop();
        ApplyRotation(-sideThrust);
        if (!leftBackThrusterParticles.isPlaying && !leftFrontThrusterParticles.isPlaying)
        {
            leftBackThrusterParticles.Play();
            leftFrontThrusterParticles.Play();
        }
    }
    private void StopSideThrustParticles()
    {
        rightBackThrusterParticles.Stop();
        rightFrontThrusterParticles.Stop();
        leftBackThrusterParticles.Stop();
        leftFrontThrusterParticles.Stop();
    }

    void ApplyRotation(float rotationFrame){
        rb.freezeRotation = true; //freezing rotation to manual rotation
        transform.Rotate(Vector3.forward * rotationFrame * Time.deltaTime);
        rb.freezeRotation = false;
        
    }
}
