using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelText : MonoBehaviour
{
    private void Start()
    {
        var levelText = GetComponent<TextMeshProUGUI>();
        var levelName = SceneManager.GetActiveScene().name;
        levelText.text = levelName;
    }
}
