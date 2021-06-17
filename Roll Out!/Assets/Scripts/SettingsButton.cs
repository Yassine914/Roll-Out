using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

    void Start()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenCloseSettingsMenu()
    {
        FindObjectOfType<SceneLoader>().uiAudio.Play();
        if (!settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(true);
        }
        else if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
        }
    }
}