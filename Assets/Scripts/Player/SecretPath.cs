using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretPath : MonoBehaviour
{
    public GameObject canvas;
    public Transform backpoint;
    private GameObject player;
    public AudioSource background;
    private AudioSource secret;

    private void Awake()
    {
        secret = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        background.Pause();
        secret.Play();
        canvas.SetActive(true);
        player = collision.gameObject;
        player.GetComponent<PlayerController>().ganado = true;
        player.GetComponent<PlayerController>().disparando = true;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        secret.Stop();
        background.Play();
        canvas.SetActive(false);
        collision.gameObject.GetComponent<PlayerController>().ganado = false;
        collision.gameObject.GetComponent<PlayerController>().disparando = false;
    }

    public void SecretWarp()
    {
        player.GetComponent<PlayerController>().SaveData();
        SceneManager.LoadScene("Scene3");
    }

    public void GoBack()
    {
        player.transform.position = new Vector3(backpoint.position.x,backpoint.position.y, player.transform.position.z);
    }
}
