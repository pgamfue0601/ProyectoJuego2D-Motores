using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemigo;
    public float velocidad;
    public Vector3 posicionFin;
    public Vector3 posicionInicial;
    private SpriteRenderer spriteRenderer;
    private bool moviendoAFin;
    public int moveX;
    public int moveY;
    public AudioSource muerto;
    public AudioSource music;
    public bool salto;
    public bool enemyDead;
    
    // Start is called before the first frame update
    private void Awake()
    {
        muerto = GetComponent<AudioSource>();
    }
    void Start()
    {
        enemyDead = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        posicionInicial = transform.position;
        posicionFin = new Vector3(posicionInicial.x + moveX, posicionInicial.y + moveY, posicionInicial.z);
        moviendoAFin = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoverEnemigo();
    }

    void MoverEnemigo()
    {
        Vector3 posicionDestino = (moviendoAFin) ? posicionFin : posicionInicial;
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);

        if (transform.position == posicionFin)
        {
            if (!salto)
            {
                spriteRenderer.flipX = false;
            }
            
            moviendoAFin = false;
        }

        if (transform.position == posicionInicial)
        {
            if (!salto)
            {
                spriteRenderer.flipX = true;
            }
            moviendoAFin = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().vulnerable && !enemyDead)
        {
            collision.gameObject.GetComponent<PlayerController>().vulnerable = false;
            if (collision.gameObject.GetComponent<PlayerController>().vidas-- <= 1)
            {
                
                StartCoroutine(FinJuego(collision));
            }
            else
            {
                StartCoroutine(collision.gameObject.GetComponent<PlayerController>().QuitaVida(collision));  
            }
            collision.gameObject.GetComponent<PlayerController>().hudController.SetVidas(collision.gameObject.GetComponent<PlayerController>().vidas);
        }
        
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

    public void EnemigoMuerto()
    {
        Destroy(gameObject);
    }
}
