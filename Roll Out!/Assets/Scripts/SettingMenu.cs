using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public void SetQualtiy(int qualityIndex)
    {
       QualitySettings.SetQualityLevel(qualityIndex);
    }
}
