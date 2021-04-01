using UnityEngine;
using TMPro;

public class CoinsText : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] public TextMeshProUGUI coinsText;
    [HideInInspector] public int coins;
    public static string CoinsSaveKey = "coins";

    private void Start()
    {
        PlayerPrefs.SetInt(CoinsSaveKey, coins);
        coinsText.text = PlayerPrefs.GetInt(CoinsSaveKey, 0).ToString();
    }

    public void AddToCoins()
    {
        coins++;
        PlayerPrefs.SetInt(CoinsSaveKey, coins);
        coinsText.text = PlayerPrefs.GetInt(CoinsSaveKey, 0).ToString();
    }
}
