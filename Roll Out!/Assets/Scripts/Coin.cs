using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, UnityEngine.Random.Range(1, 3), 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<CoinsText>().AddToCoins();
            Destroy(gameObject);
        }
    }
}
