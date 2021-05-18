using System;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 10f;
    public Vector3 offset;

    private void Start()
    {
        Screen.SetResolution((int) Screen.width, (int) Screen.height, true);
    }

    private void FixedUpdate ()
    {
        var desiredPosition = player.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
