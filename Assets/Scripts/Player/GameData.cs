using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private int coins;
    private bool ganado;
    private int bullets;
    private int lives;
    public bool hasData;

    public int Coins { get => coins; set => coins = value; }
    public bool Ganado { get => ganado; set => ganado = value; }
    public int Bullets { get => bullets; set => bullets = value; }
    public int Lives { get => lives; set => lives = value; }

    private void Awake()
    {
        int numInstancias = FindObjectsOfType<GameData>().Length;

        if (numInstancias != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        hasData = false;
    }
}
