using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character_Movement : MonoBehaviour
{
  public CharacterController controller;
  private Animator anim;
  public bool isGrounded;
  public float groundCheckDistance;
  public LayerMask groundMask;
  public float speed = 7f;
  public float gravity;
  private Vector3 velocity;
  private Vector3 moveDirection;

  public bool Win = false;

  public int lives;

  public Image lives1;
  public Image lives2;
  public Image lives3;
  public Sprite lostHealth;

  public AudioClip hit;
  AudioSource audioSource;
  
  void Start()
  {
    audioSource = GetComponent<AudioSource>();

    lives = 3;

    anim = GetComponentInChildren<Animator>();
  }
   // Update is called once per frame
  void Update()
  {
    float x = Input.GetAxis ("Horizontal");
    float z = Input.GetAxis ("Vertical");

    moveDirection = new Vector3(x, 0, z);

    isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

    if(isGrounded && velocity.y < 0)
    {
      velocity.y = -2f;
    }

    if(moveDirection != Vector3.zero)
    {
      anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
      else if(moveDirection == Vector3.zero)
    {
      anim.SetFloat("Speed",0, 0.1f, Time.deltaTime);
    }

    if(Input.GetKeyDown(KeyCode.X))
    {
      anim.SetTrigger("Crouch");
    }
    if(Input.GetKeyUp(KeyCode.X))
    {
      anim.SetTrigger("Stand");
    }

    Vector3 move = transform.right * x + transform.forward * z;
    controller.Move(move * speed * Time.deltaTime);

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Key"))
    {
      Destroy(other.gameObject);

      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;

      SceneManager.LoadScene("WinScene");
    }

    if (other.gameObject.CompareTag("Enemy"))
    {
      lives -= 1;
      audioSource.Play();

      if(lives == 2)
      {
        lives1.sprite = lostHealth;
        lives1.color = Color.red;
      }

      if(lives == 1)
      {
        lives2.sprite = lostHealth;
        lives2.color = Color.red;
      }

      if(lives == 0)
      {
        lives3.sprite = lostHealth;
        lives3.color = Color.red;

      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;

        SceneManager.LoadScene("LossScene");
      }
    }
  }
}