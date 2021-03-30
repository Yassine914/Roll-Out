using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Score")]
    public int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int scoreMultiplier;
    [SerializeField] private GameObject multiplierOblect;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private float multiplierHideDelay = 1.5f;

    private void Start()
    {
        scoreMultiplier = 0;
        multiplierOblect.SetActive(false);
    }

    private void Update()
    {
        score = (int) Time.timeSinceLevelLoad + scoreMultiplier;
        if (scoreText is { }) scoreText.text = score.ToString();
        multiplierText.text = "+50";
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Multiplier"))
        {
            StartCoroutine(AddScoreMultiplier());
        }
    }

    IEnumerator AddScoreMultiplier()
    {
        scoreMultiplier += 50;
        multiplierOblect.SetActive(true);
        yield return new WaitForSeconds(multiplierHideDelay);
        multiplierOblect.SetActive(false);
    }
}
