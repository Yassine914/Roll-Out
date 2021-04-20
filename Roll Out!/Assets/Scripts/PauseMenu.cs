using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused = false;

    [SerializeField] private GameObject pauseMenu;
    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        PauseGame();
    }
  
    private void PauseGame()
    {
        var player = FindObjectOfType<Player>();
        
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _isPaused = false;
        }


        if (Input.GetButtonDown("Pause") && !_isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _isPaused = true;
        }
        else if(Input.GetButtonDown("Pause") && _isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _isPaused = false;
        }
    }

    public void ContinueGame()
    {
        var player = FindObjectOfType<Player>();
        if (_isPaused && !player.hasWon && player.isAlive)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _isPaused = false;
        }
    }
}
