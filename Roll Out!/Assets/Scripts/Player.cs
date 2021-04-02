using UnityEngine;

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
    
    private bool _isAlive = true;
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
    }
    
    private void Update()
    {
        FallLimit();
        if (_isAlive && !hasWon)
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
            _rigidBody.velocity += Vector3.up * jumpHeight;
        }
    }

    private void MoveHorizontal()
    {
        var horizontalMove = Input.GetAxis("Horizontal");
        var horizontal = new Vector3(horizontalMove * horizontalMoveSpeed * Time.deltaTime, 0, 0);
        _rigidBody.velocity += horizontal;
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if(otherCollider.collider.CompareTag("Obstacles"))
        {
            Die();
        }
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

    private void OnCollisionExit()
    {
        _isGrounded = false;
    }
    
    private void Die()
    {
        _isAlive = false;
        GetComponent<Score>().score = 0;
        Time.timeScale = 0f;
        dieMenu.SetActive(true);
    }
}
