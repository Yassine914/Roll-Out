using UnityEngine;
using TMPro;

public class CoinsText : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] public TextMeshProUGUI coinsText;
    public int coins;
    public static string CoinsSaveKey = "coins";

    private void Start()
    {
        PlayerPrefs.SetInt(CoinsSaveKey, coins);
        coinsText.text = PlayerPrefs.GetInt(CoinsSaveKey).ToString();
    }

    public void AddToCoins()
    {
        PlayerPrefs.SetInt(CoinsSaveKey, coins++);
        coinsText.text = PlayerPrefs.GetInt(CoinsSaveKey).ToString();
    }
}
