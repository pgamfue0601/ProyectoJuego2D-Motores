using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public bool start;
    public int vidas;
    private Rigidbody2D rb;
    public GameObject enemyBullet;
    public Transform firepoint;
    public AudioSource music;
    public AudioSource winner;
    public AudioSource deathScream;
    public AudioSource muerto;
    public bool vulnerable;
    private SpriteRenderer spriteRenderer;
    private bool eliminado;
    // Start is called before the first frame update
    void Start()
    {
        vidas = 3;
        vulnerable = true;
        start = false;
        eliminado = false;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start && !eliminado)
        {
            StartCoroutine(BossFight());
        }
    }

    private IEnumerator BossFight()
    {
        start = false;
        rb.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        GameObject instanciaPrefab = Instantiate(enemyBullet, firepoint.position, firepoint.rotation);
        Destroy(instanciaPrefab, 3f);
        yield return new WaitForSeconds(1.8f);
        start = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().vulnerable)
        {
            collision.gameObject.GetComponent<PlayerController>().vulnerable = false;
            if (collision.gameObject.GetComponent<PlayerController>().vidas-- <= 0)
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
        start = false;
        eliminado = true;
        music.Stop();
        muerto.PlayOneShot(muerto.clip);
        Camera.main.transform.parent = null;
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        collision.gameObject.GetComponent<PlayerController>().muerto = true;
        collision.gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        yield return new WaitForSeconds(2f);
        collision.gameObject.GetComponent<PlayerController>().Perder();
    }

    public IEnumerator QuitaVida(Collider2D collision)
    {
        collision.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(2.2f);
        collision.GetComponent<BossController>().vulnerable = true;
        collision.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public IEnumerator QuitaVida()
    {
        vidas--;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(2.2f);
        gameObject.GetComponent<BossController>().vulnerable = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public IEnumerator FinBoss()
    {
        spriteRenderer.color = Color.red;
        GameObject.Find("player").GetComponent<PlayerController>().ganado = true;
        GameObject.Find("player").GetComponent<PlayerController>().disparando = true;
        start = false;
        music.Stop();
        deathScream.Play();
        yield return new WaitForSeconds(2f);
        spriteRenderer.enabled = false;
        winner.Play();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
