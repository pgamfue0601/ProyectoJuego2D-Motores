using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitExtra : MonoBehaviour
{
    public Transform exitpoint;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            audioSource.PlayOneShot(audioSource.clip);
            collision.gameObject.transform.position = exitpoint.transform.position;
        }
    }
}
