using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using EasyJoystick;
using UnityEngine.Advertisements;

public class Player : MonoBehaviour, IUnityAdsListener
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
    [SerializeField] private float playerInvincibleDelay = 3f;
    public Joystick joystick;
    private AudioSource audioSource;
    private int numOfHitAudios;
    
    public bool isAlive = true;
    private bool _isGrounded;
    private Rigidbody _rigidBody;
    [HideInInspector] public bool hasWon;
    private Renderer playerRenderer;
    private string gameId = "4124803";
    private string placementId = "rewardedVideo";
    private GameObject adNotReadyText;
    

    private void Start()
    {
        if (!joystick) { return; }
        hasWon = false;
        _rigidBody = GetComponent<Rigidbody>();
        _isGrounded = false;
        if(!dieMenu){return;}
        dieMenu.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        numOfHitAudios = Random.Range(0, hitAudios.Length);
        playerRenderer = GetComponent<Renderer>();
        Physics.IgnoreLayerCollision(8, 6, false);
        adNotReadyText = GameObject.FindGameObjectWithTag("AdNotReady");

        Advertisement.Initialize(gameId, false);
        Advertisement.AddListener(this);
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
        var verticalMove = joystick.Vertical();
        var vertical = new Vector3( 0, 0, verticalMove * verticalMoveSpeed * Time.deltaTime);
        _rigidBody.velocity += vertical;
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

    public void JumpFromButton()
    {
        if (_isGrounded && !FindObjectOfType<PauseMenu>().isPaused)
        {
            _rigidBody.velocity += Vector3.up * jumpHeight;
        }
    }

    private void MoveHorizontal()
    {
        var horizontalMove = joystick.Horizontal();
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

    public void PlayAd()
    {
        if(Advertisement.IsReady(placementId))
        {
            Advertisement.Show(placementId);
        }
        else
        {
            Debug.Log("Ad is Not Ready!");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ad Is Ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            Debug.Log("Reward Player Now!");
            StartCoroutine(ContinueAfterAd());
        }

        if(showResult == ShowResult.Failed)
        {
            StartCoroutine(AdNotReady());
        }
    }

    private IEnumerator ContinueAfterAd()
    {
        Time.timeScale = 1f;
        isAlive = true;
        dieMenu.SetActive(false);
        var playerColor = playerRenderer.material.color;
        playerRenderer.material.color = Color.white;
        Physics.IgnoreLayerCollision(8, 6, true);

        yield return new WaitForSeconds(playerInvincibleDelay);

        playerRenderer.material.color = playerColor;
        Physics.IgnoreLayerCollision(8, 6, false);
    }

    private IEnumerator AdNotReady()
    {
        adNotReadyText.SetActive(true);
        yield return new WaitForSeconds(2);
        adNotReadyText.SetActive(false);
    }
    
    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
