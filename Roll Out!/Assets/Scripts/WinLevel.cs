using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    [SerializeField] private float winLevelDelay = 5f;
    [SerializeField] private GameObject winParticleVFX;
    [SerializeField] private float particleVFXDestroyDelay = 10f;
    [SerializeField] private Vector3 particleOffset;
    [SerializeField] private GameObject winMenu;

    private int levelToUnlock = 2;

    private AudioSource winSFX;

    private void Start()
    {
        winMenu.SetActive(false);
        winSFX = GetComponent<AudioSource>();
        levelToUnlock = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            StartCoroutine(LevelWin());
        }
    }

    private IEnumerator LevelWin()
    {
        if (PlayerPrefs.GetInt("levelReached") < levelToUnlock)
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
        winSFX.Play();
        FindObjectOfType<Player>().hasWon = true;
        InstantiateParticle();
        yield return new WaitForSeconds(winLevelDelay);
        winMenu.SetActive(true);
    }

    private void InstantiateParticle()
    {
        GameObject newParticleEffect =
            Instantiate(winParticleVFX,
                transform.position + particleOffset,
                Quaternion.identity);
        
        newParticleEffect.transform.parent = transform;
        
        Destroy(newParticleEffect, particleVFXDestroyDelay);

    }
}
