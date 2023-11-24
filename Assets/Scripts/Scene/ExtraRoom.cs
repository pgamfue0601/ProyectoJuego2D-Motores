using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraRoom : MonoBehaviour
{
    public Transform enterpoint;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            audioSource.PlayOneShot(audioSource.clip);
            collision.gameObject.transform.position = enterpoint.transform.position;
        }
    }
}
