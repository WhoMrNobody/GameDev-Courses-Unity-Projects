using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    enum State { Alive, Dying, Transending }

    State state = State.Alive;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainThrustSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip succesSFX;

    [SerializeField] ParticleSystem mainThrustPartical;
    [SerializeField] ParticleSystem deathPartical;
    [SerializeField] ParticleSystem succesPartical;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state==State.Alive)
        {
            ProcessInput();
        }
        
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
           

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainThrustSFX);
                mainThrustPartical.Play();


            }
            if (!mainThrustPartical.isPlaying) // SO THE ENGINE PARTICLES DON"T LAYER EITHER
            {
                mainThrustPartical.Play();

            }

            
        }

        else
        {
            audioSource.Stop();
            mainThrustPartical.Stop();
        }


        rigidbody.freezeRotation = true;

       
        float rotationThisFrame = rcsThrust * Time.deltaTime; 

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rotationThisFrame);

        } else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotationThisFrame);
        }

        rigidbody.freezeRotation = false;

    }

     void OnCollisionEnter(Collision collision)
    {
        if (state!=State.Alive)
        {
            return;
        }
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":

                break;

            case "Finish":
                state = State.Transending;
                audioSource.Stop();
                audioSource.PlayOneShot(succesSFX);
                succesPartical.Play();
                Invoke("LoadNextLevel", 1f);
                break;

            default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(deathSFX);
                deathPartical.Play();
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

     void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
