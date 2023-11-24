using System.Collections;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public GameObject bloqueoSalida;
    public AudioSource background;
    public AudioClip battleAudio;
    public AudioSource roarBoss;
    private bool entered;

    private void Awake()
    {
        roarBoss = GetComponent<AudioSource>();
        entered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!entered)
        {
            StartCoroutine(BossBattleFunction(collision));
        }

    }

    IEnumerator BossBattleFunction(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            entered = true;
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.fisicas.velocity = Vector3.zero;
            player.disparando = true;
            player.ganado = true;
            background.Stop();
            roarBoss.Play();
            yield return new WaitForSeconds(5f);
            bloqueoSalida.SetActive(true);
            background.clip = battleAudio;
            background.Play();
            player.disparando = false;
            player.ganado = false;
            BossController boss = GameObject.Find("Boss").GetComponent<BossController>();
            yield return new WaitForSeconds(1f);
            boss.start = true;
        }

    }

}
