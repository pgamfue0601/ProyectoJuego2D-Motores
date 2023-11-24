using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioSource audioSource;
    private bool entrado;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        entrado = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (!entrado)
                {
                    if (collision.gameObject.GetComponent<PlayerController>().coinCounter > 0 && collision.gameObject.GetComponent<PlayerController>().coinCounter % 10 == 0)
                    {
                        collision.gameObject.GetComponent<PlayerController>().AddLife();
                    }
                    collision.gameObject.GetComponent<PlayerController>().hudController.SetBalas(collision.gameObject.GetComponent<PlayerController>().disparos += 1);
                    audioSource.PlayOneShot(audioSource.clip);
                    collision.gameObject.GetComponent<PlayerController>().AddCoin();
                    Destroy(gameObject, 0.5f);
                    entrado = true;
                }

            }
        }
    }
}
