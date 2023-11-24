using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextLevel;
    public AudioSource finished;
    private PlayerController player;
    private bool entered;


    private void Awake()
    {
        finished = GetComponent<AudioSource>();
        entered = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            StartCoroutine(NextLevelWin(collision));
        }
    }

    IEnumerator NextLevelWin(Collider2D collision)
    {
        if (!entered)
        {
            entered = true;
            collision.gameObject.GetComponent<PlayerController>().fisicas.velocity = Vector2.zero;
            collision.gameObject.GetComponent<PlayerController>().ganado = true;
            collision.gameObject.GetComponent<PlayerController>().disparando = true;
            collision.gameObject.GetComponent<PlayerController>().SaveData();
            GameObject.Find("Background").GetComponent<AudioSource>().Stop();
            finished.PlayOneShot(finished.clip);
            collision.gameObject.GetComponent<PlayerController>().jumpAudio.Play();
            collision.gameObject.GetComponent<PlayerController>().fisicas.AddForce(Vector2.up * collision.gameObject.GetComponent<PlayerController>().fuerzaSalto, ForceMode2D.Impulse);
            yield return new WaitForSeconds(9f);
            collision.gameObject.GetComponent<PlayerController>().ganado = false;
            collision.gameObject.GetComponent<PlayerController>().disparando = false;
            SceneManager.LoadScene(nextLevel);
        }
        
    }
}
