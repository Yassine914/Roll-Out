using UnityEngine;
using TMPro;
public class SettingMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private GameObject deleteProgressPrompt;
    private AudioSource uiAudio;

    private void Start()
    {
        var quality = QualitySettings.GetQualityLevel();
        qualityDropdown.value = quality;
        deleteProgressPrompt.SetActive(false);
        uiAudio = FindObjectOfType<SceneLoader>().uiAudio;
    }

    public void SetQualtiy(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
    }

    public void ResetLevelProgress()
    {
        uiAudio.Play();
        PlayerPrefs.DeleteKey("levelReached");
    }
}
