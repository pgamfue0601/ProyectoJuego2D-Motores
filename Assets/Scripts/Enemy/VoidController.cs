using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidController : MonoBehaviour
{
    public AudioSource muerto;
    public AudioSource music;
    private void Awake()
    {
        muerto = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().vulnerable)
        {
            collision.gameObject.GetComponent<PlayerController>().vulnerable = false;
            StartCoroutine(FinJuego(collision));
            collision.gameObject.GetComponent<PlayerController>().hudController.SetVidas(collision.gameObject.GetComponent<PlayerController>().vidas);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossController>().FinBoss();
        }

    }

    IEnumerator FinJuego(Collider2D collision)
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
