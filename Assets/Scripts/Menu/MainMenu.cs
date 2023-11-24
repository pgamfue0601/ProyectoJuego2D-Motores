using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.loop = true;
    }

    public void StartGame()
    {
        m_AudioSource.Stop();
        SceneManager.LoadScene("Scene1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
