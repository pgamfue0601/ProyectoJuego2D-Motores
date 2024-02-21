using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidScript : MonoBehaviour
{
    private PlayerController player;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        #if UNITY_ANDROID
            gameObject.SetActive(true);
        #endif

       
    }

    public void MoverseDerecha()
    {
        player.moverseDerecha = true;
    }

    public void MoverseIzquierda()
    {
        player.moverseIzquierda = true;
    }

    public void StopMoverse()
    {
        
        player.moverseDerecha = false;
        player.moverseIzquierda = false;
    }

    public void Saltar()
    {
        if (player.TocandoSuelo())
        {
            player.jumpAudio.Play();
            player.fisicas.AddForce(Vector2.up * player.fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    public void Disparar()
    {
        GetComponentInParent<WeaponController>().shoot();
    }
}
