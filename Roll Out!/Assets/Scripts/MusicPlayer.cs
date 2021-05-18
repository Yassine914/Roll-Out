using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
   private AudioSource audioSource;
   public GameObject objectMusic;
   private float musicVolume = 1f;
   public Slider volumeSlider;
   
   private void Start()
   {
      if(volumeSlider == null){return;}
      
      objectMusic = GameObject.FindWithTag("Music");
      audioSource = objectMusic.GetComponent<AudioSource>();
      
      musicVolume = PlayerPrefs.GetFloat("Volume");
      audioSource.volume = musicVolume;
      volumeSlider.value = musicVolume;
   }
   
   private void Update()
   {
     if (!audioSource) {return;}
     audioSource.volume = musicVolume;
     PlayerPrefs.SetFloat("Volume", musicVolume);
   }
   
   public void SetVolume(float volume)
   {
     musicVolume = volume;
   }
}
