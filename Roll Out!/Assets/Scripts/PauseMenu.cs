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
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _isPaused = false;
        }
    }

    public void ContinueGame()
    {
        if (_isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _isPaused = false;
        }
    }
}
