using System;
using UnityEngine;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;

    private void Start()
    {
        var quality = QualitySettings.GetQualityLevel();
        qualityDropdown.value = quality;
    }

    public void SetQualtiy(int qualityIndex)
    {
       QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ResetLevelProgress()
    {
        PlayerPrefs.DeleteKey("levelReached");
    }
}
