using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public AudioSource audioSource;
    private AudioClip clip;
    private Renderer rend;
    public bool enemyBullet;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            audioSource.Play();
            collision.GetComponent<EnemyController>().EnemigoMuerto();
            rend.enabled = false;
            Destroy(gameObject);
        }

        if (enemyBullet && collision.CompareTag("Player"))
        {
            StartCoroutine(collision.GetComponent<PlayerController>().QuitaVida(collision));
        }

        if (collision.CompareTag("Boss") && !enemyBullet)
        {
            if (collision.GetComponent<BossController>().vulnerable)
            {
                if (collision.gameObject.GetComponent<BossController>().vidas-- <= 1)
                {
                    PlayerController player = GameObject.Find("player").GetComponent<PlayerController>();
                    player.disparando = true;
                    player.ganado = true;
                    StartCoroutine(collision.gameObject.GetComponent<BossController>().FinBoss());
                }
                else
                {
                    StartCoroutine(collision.gameObject.GetComponent<BossController>().QuitaVida(collision));
                }
            }
        }
    }
}