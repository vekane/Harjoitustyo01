using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    public float jumpForce;
    public float gravityModifier;
    public bool isGrounded = true;
    public int lifeAmount = 3;

    [Header("Bools")]
    public bool gameOver = false;
    private bool waitingtoStart = true;

    [Header("Physics and Animation")]
    private Rigidbody playerRB;
    private Animator playerAnim;

    [Header("Particle Systems")]
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource playerAudio;

    private void Awake()
    {
        if (waitingtoStart == true)
        {
            Time.timeScale = 0;
        }

    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        GameHandler.instance.UpdateLifeCount(lifeAmount);

    }

    // Update is called once per frame
    void Update()
    {

        //Pelaajan hyppy
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameOver)
        {
            //Jos waitingtoStart on true, muutetaan se falseksi, mik‰ aloittaa pelin
            if (waitingtoStart == true)
            {
                waitingtoStart = false;
                Time.timeScale += 1;
                GameHandler.instance.HideStartText();
            }
           
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Play();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        // S-n‰pp‰imeen asetettu lis‰toiminto, mill‰ pelaaja p‰‰see nopeasti ilmasta maahan
        //Toimii kuten hyppy mutta "k‰‰nteisen‰"
        if(Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            playerRB.AddForce(-Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    //Eri collisionit mill‰ tunnistetaan eri objekteihin merkatut tagit
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (lifeAmount <= 0)
            {
                //Player dead
                GameHandler.instance.GameOverScreen();
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                Debug.Log("Game over");
            }
            else
            {
                lifeAmount--;
                explosionParticle.Play();
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSound, 1.0f);
                GameHandler.instance.UpdateLifeCount(lifeAmount);
            }
        }
    }
}
