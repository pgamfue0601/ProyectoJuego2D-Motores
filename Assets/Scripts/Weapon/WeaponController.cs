using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public PlayerController playerController;
    public AudioSource disparoSound;
    public GameObject bulletPrefab;
    public Transform firepoint;
    // Start is called before the first frame update
    private void Awake()
    {
        disparoSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if (playerController.disparos > 0 && playerController.disparando == false)
        {
            playerController.disparos--;
            playerController.hudController.SetBalas(playerController.disparos);
            disparoSound.PlayOneShot(disparoSound.clip);
            playerController.disparando = true;
            GameObject instanciaPrefab = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            Invoke("SinDisparar", 1f);
            Destroy(instanciaPrefab, 3f);
        }
    }
    private void SinDisparar()
    {
        playerController.disparando = false;
    }
}
