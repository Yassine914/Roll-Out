using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    

    private void Start()
    {
       _sceneLoader =  FindObjectOfType<SceneLoader>();
    }

    public void LoadLevel()
    {
        Time.timeScale = 1f;
        var levelText = GetComponentInChildren<TextMeshProUGUI>().text;
        StartCoroutine(LoadLevelTransition(levelText));
    }

    IEnumerator LoadLevelTransition(string levelText)
    {
        _sceneLoader.transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(_sceneLoader.transitionTime);
        SceneManager.LoadScene("Level " + levelText);
    }

    
}
