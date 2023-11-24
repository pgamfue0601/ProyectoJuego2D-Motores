using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int velocidad;
    public float fuerzaSalto;
    public Rigidbody2D fisicas;
    float entradaX = 0f;
    public SpriteRenderer spriteRenderer;
    private Animator animator;
    public int coinCounter, disparos, vidas;
    public Transform puntoDisparo;
    public Transform playerTransform;
    public AudioSource jumpAudio;
    public bool vulnerable, disparando, muerto, ganado;
    public HUDController hudController;
    public float tiempoNivel, tiempoEmpleado, tiempoInicio;
    private GameData datosJuego;
    public bool sueloForzado;
    private bool dataReceived;
    // Start is called before the first frame update
    void Start()
    {
        sueloForzado = false;
        animator = GetComponent<Animator>();
        velocidad = 7;
        fuerzaSalto = 10.5f;
        fisicas = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        disparando = false;
        playerTransform = GetComponent<Transform>();
        vulnerable = true;
        muerto = false;
        ganado = false;
        tiempoInicio = Time.time;
        datosJuego = GameObject.Find("DatosJuego").GetComponent<GameData>();
        if (datosJuego.hasData)
        {
            disparos = datosJuego.Bullets;
            coinCounter = datosJuego.Coins;
            vidas = datosJuego.Lives;
        }
        else
        {
            vidas = 2;
            disparos = 3;
            coinCounter = 0;
        }

        hudController.SetVidas(vidas);
        hudController.SetBalas(disparos);
        hudController.SetMonedas(coinCounter);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocidadX", Mathf.Abs(fisicas.velocity.x));
        animator.SetFloat("velocidadY", fisicas.velocity.y);

        entradaX = Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo())
        {
            jumpAudio.Play();
            fisicas.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
        Flip();
        TiempoEmpleado();
        if (!ganado)
        { 
            CheatsSkip();
        }

        if (!vulnerable)
        {
            StartCoroutine(VulnerableFix());
        }
    }

    private void FixedUpdate()
    {
        if (muerto || ganado)
        {
            return;
        }
        fisicas.velocity = new Vector3(entradaX * velocidad, fisicas.velocity.y);
    }

    private void TiempoEmpleado()
    {
        if (muerto || ganado)
        {
            return;
        }

        tiempoEmpleado = Time.time - tiempoInicio;
            hudController.SetTiempoTxt((int)(tiempoNivel - tiempoEmpleado));
            if (tiempoNivel - tiempoEmpleado < 0)
            {
                Perder();
            }
        
    }

    public bool TocandoSuelo()
    {
        if (!sueloForzado)
        {
            RaycastHit2D toca = Physics2D.Raycast(transform.position + new Vector3(0, -1f), Vector2.down, 0.2f);
            return toca.collider != null;
        } else
        {
            return true;
        }
        
    }

    private void Flip()
    {
        if (fisicas.velocity.x > 0f)
        {
            Quaternion nuevaRotacion = Quaternion.Euler(0, 0, 0f);
            puntoDisparo.rotation = nuevaRotacion;
            puntoDisparo.transform.position = new Vector3(playerTransform.position.x + 1, playerTransform.position.y, playerTransform.position.z);
            spriteRenderer.flipX = false;
        }
        else if (fisicas.velocity.x < 0f)
        {
            Quaternion nuevaRotacion = Quaternion.Euler(0, 180f, 0f);
            puntoDisparo.rotation = nuevaRotacion;
            puntoDisparo.transform.position = new Vector3(playerTransform.position.x - 1, playerTransform.position.y, playerTransform.position.z);
            spriteRenderer.flipX = true;
        }
    }

    private IEnumerator VulnerableFix()
    {
        yield return new WaitForSeconds(2f);
        spriteRenderer.color = Color.white;
        vulnerable = true;
    }

    public void Perder()
    {
        //datosJuego.Ganado = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void AddLife()
    {
        vidas ++;
        hudController.SetVidas(vidas);
    }

    public void AddCoin()
    {
        coinCounter++;
        hudController.SetMonedas(coinCounter);
    }

    public void SaveData()
    {
        datosJuego.Bullets = disparos;
        datosJuego.Lives = vidas;
        datosJuego.Coins = coinCounter;
        datosJuego.hasData = true;
    }

    public IEnumerator QuitaVida(Collision2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(3f);
        collision.gameObject.GetComponent<PlayerController>().vulnerable = true;
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public IEnumerator QuitaVida(Collider2D collision)
    {
        collision.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(2f);
        collision.GetComponent<PlayerController>().vulnerable = true;
        collision.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void CheatsSkip()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveData();
            SceneManager.LoadScene("Scene1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SaveData();
            SceneManager.LoadScene("Scene2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SaveData();
            SceneManager.LoadScene("Scene3");
        }
    }
}
