using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public float speed;


    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
}
