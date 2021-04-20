using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float verticalMoveSpeed = 1f;
    [SerializeField] private float horizontalMoveSpeed = 10f;
    [SerializeField] private float fallLimit = 3f;
    [SerializeField] private float jumpHeight = 10f;
    [Header("Other")]
    [SerializeField] private bool isMainMenuElement;
    [SerializeField] public GameObject dieMenu;
    [SerializeField] public AudioClip[] hitAudios;
    [SerializeField] private GameObject spike;
    [SerializeField] private Vector3 spikeOffset;
    private AudioSource audioSource;
    private int numOfHitAudios;
    
    public bool isAlive = true;
    private bool _isGrounded;
    private Rigidbody _rigidBody;
    [HideInInspector] public bool hasWon;

    private void Start()
    {
        hasWon = false;
        _rigidBody = GetComponent<Rigidbody>();
        _isGrounded = false;
        if(!dieMenu){return;}
        dieMenu.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        numOfHitAudios = Random.Range(0, hitAudios.Length);
    }
    
    private void Update()
    {
        FallLimit();
        if (isAlive && !hasWon)
        {
            MoveVertical();
            MoveHorizontal();
            Jump();
        }
    }


    private void MoveVertical()
    {
        var verticalMove = Input.GetAxis("Vertical");
        var vertical = new Vector3( 0, 0, verticalMove * verticalMoveSpeed * Time.deltaTime);
        _rigidBody.velocity += vertical;

        var backMove = Input.GetAxis("Backward");

        if (isMainMenuElement)
        {
            var back = new Vector3(0, 0, backMove * verticalMoveSpeed * Time.deltaTime);
            _rigidBody.velocity += back;
        }
    }

    private void Jump()
    {
        if (isMainMenuElement) return;
        if (!_isGrounded) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody.velocity += Vector3.up;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _rigidBody.velocity += Vector3.up * jumpHeight;
        }
    }

    private void MoveHorizontal()
    {
        var horizontalMove = Input.GetAxis("Horizontal");
        var horizontal = new Vector3(horizontalMove * horizontalMoveSpeed * Time.deltaTime, 0, 0);
        _rigidBody.velocity += horizontal;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        spikeOffset = new Vector3(Random.Range(-4, 5), 10, 20);
        if (otherCollider.CompareTag("Spikes"))
        {
            GameObject spikeClone = Instantiate(spike,
                otherCollider.transform.position + spikeOffset,
                 spike.transform.rotation);
        }
    }
  
    private void OnCollisionEnter(Collision otherCollider)
    {
        if(otherCollider.collider.CompareTag("Obstacles"))
        {
            audioSource.PlayOneShot(hitAudios[numOfHitAudios], .7f);
            Die();
        }
    }

    private void OnCollisionExit(Collision otherCollider)
    {
        _isGrounded = false;
    }

    private void FallLimit()
    {
        if (transform.position.y < -fallLimit && !hasWon)
        {
            FindObjectOfType<SceneLoader>().ReloadLevel();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            GetComponent<Score>().score = 0;
            Time.timeScale = 0f;
            dieMenu.SetActive(true);
        }
    }
}
