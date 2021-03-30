using System;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    public int coins;
    [SerializeField] private TextMeshProUGUI _coinsText;

    private void OnTriggerEnter(Collider other)
    {
        coins++;
    }
}
