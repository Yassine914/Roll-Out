using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUi;
    [SerializeField] private float endTutorialDelay = 4f;
    private Collider collider;

    private void Start()
    {
        tutorialUi.SetActive(false);
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(OpenTutorial());
            collider.enabled = false;
        }
    }

    private void Update()
    {
        if ( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && tutorialUi.activeSelf == true)
        {
            StopCoroutine(OpenTutorial());
            Time.timeScale = 1f;
            tutorialUi.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(OpenTutorial());
            Time.timeScale = 1f;
            tutorialUi.SetActive(false);
        }
    }

    IEnumerator OpenTutorial()
    {
        tutorialUi.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(endTutorialDelay);
        Time.timeScale = 1f;
        tutorialUi.SetActive(false);
    }
}