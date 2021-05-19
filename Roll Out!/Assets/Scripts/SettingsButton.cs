using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    private Button settingsButton;
    [SerializeField] private GameObject settingsMenu;

    void Start()
    {
        settingsButton = GetComponent<Button>();
        settingsMenu.SetActive(false);
    }
}
