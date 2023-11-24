using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public AudioSource muerto;
    public AudioSource music;
    private void Awake()
    {
        muerto = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().vulnerable)
        {
            collision.gameObject.GetComponent<PlayerController>().vulnerable = false;
            if (collision.gameObject.GetComponent<PlayerController>().vidas-- <= 1)
            {
                
                StartCoroutine(FinJuego(collision));
            }
            else
            {
                StartCoroutine(QuitaVida(collision));
            }
            collision.gameObject.GetComponent<PlayerController>().hudController.SetVidas(collision.gameObject.GetComponent<PlayerController>().vidas);
        }
    }

    IEnumerator QuitaVida(Collision2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(6f);
        collision.gameObject.GetComponent<PlayerController>().vulnerable = true;
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator FinJuego(Collision2D collision)
    {
        music.Stop();
        muerto.PlayOneShot(muerto.clip);
        Camera.main.transform.parent = null;
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        collision.gameObject.GetComponent<PlayerController>().muerto = true;
        collision.gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        yield return new WaitForSeconds(6f);
        collision.gameObject.GetComponent<PlayerController>().Perder();
    }
}
