using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _verticalMoveSpeed = 1f;
    [SerializeField] private float _horizontalMoveSpeed = 10f;
    [SerializeField] private float _fallLimit = 3f;
    [SerializeField] private float _jumpHeight = 10f;
    [Header("Other")]
    [SerializeField] private bool _isMainMenuElement = false;
    [SerializeField] public GameObject DieMenu;
    
    private bool _isAlive = true;
    private bool _isGrounded;
    private Rigidbody _rigidBody;
    [HideInInspector] public bool HasWon;

    private void Start()
    {
        HasWon = false;
        _rigidBody = GetComponent<Rigidbody>();
        _isGrounded = false;
        if(!DieMenu){return;}
        DieMenu.SetActive(false);
    }
    
    private void Update()
    {
        FallLimit();
        if (_isAlive)
        {
            MoveVertical();
            MoveHorizontal();
            Jump();
        }
    }


    private void MoveVertical()
    {
        var verticalMove = Input.GetAxis("Vertical");
        var vertical = new Vector3( 0, 0, verticalMove * _verticalMoveSpeed * Time.deltaTime);
        _rigidBody.velocity += vertical;

        var backMove = Input.GetAxis("Backward");

        if (_isMainMenuElement)
        {
            var back = new Vector3(0, 0, backMove * _verticalMoveSpeed * Time.deltaTime);
            _rigidBody.velocity += back;
        }
    }

    private void Jump()
    {
        if (_isMainMenuElement) return;
        if (!_isGrounded) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody.velocity += Vector3.up * _jumpHeight;
        }
    }

    private void MoveHorizontal()
    {
        var horizontalMove = Input.GetAxis("Horizontal");
        var horizontal = new Vector3(horizontalMove * _horizontalMoveSpeed * Time.deltaTime, 0, 0);
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
        if (transform.position.y < -_fallLimit && !HasWon)
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
        DieMenu.SetActive(true);
    }
}
