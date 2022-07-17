using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public float speed;
    public bool dead;

    [Header("Audio")]
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    [Header("Particle System")]
    public ParticleSystem ps;
    public GameObject body;

    private Animator animator;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.gameOver)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movex, 0.0f, movey);
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime);

        animator.SetFloat("Run", movement.magnitude);
    }

    public void PlaySound(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }

    public void Death()
    {
        dead = true;
        animator.SetBool("Dead", dead);
        gameController.gameOver = true;
        gameController.ContinueButton.gameObject.SetActive(true);
        gameController.GameOver();
        ps.Play();
        body.SetActive(false);
    }
}
