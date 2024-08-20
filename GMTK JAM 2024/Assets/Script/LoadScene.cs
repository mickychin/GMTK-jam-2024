using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void LoadGame()
    {
        SceneManager.LoadScene("ActualGame");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSetting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Togglemusic(bool TurnOn)
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().enabled = TurnOn;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = volume;
    }
}
