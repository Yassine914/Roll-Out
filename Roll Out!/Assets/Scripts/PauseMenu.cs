using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    private Player player;

    [SerializeField] private GameObject pauseMenu;
    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        PauseGame();
    }
  
    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }


        if (Input.GetButtonDown("Pause") && !isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else if(Input.GetButtonDown("Pause") && isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void PauseGameFromButton()
    {
        if (!isPaused && !player.hasWon && player.isAlive)
        {
            FindObjectOfType<SceneLoader>().uiAudio.Play();
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
    public void ContinueGame()
    {
        var player = FindObjectOfType<Player>();
        if (isPaused && !player.hasWon && player.isAlive)
        {
            FindObjectOfType<SceneLoader>().uiAudio.Play();
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }
}
