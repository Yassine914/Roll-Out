using System;
using System.Collections;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float timeToMoveDown = 2.5f;
    
    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
    }

    private void Update()
    {
        if (!isGrounded)
        {
            rb.velocity = new Vector3(0, -transform.position.y - moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            StartCoroutine(MoveUp());
        }
    }

    private IEnumerator MoveUp()
    {
        rb.velocity = new Vector3(0, moveSpeed, 0);
        yield return new WaitForSeconds(timeToMoveDown);
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
