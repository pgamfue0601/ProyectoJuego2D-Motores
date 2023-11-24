using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI vidasTxt;
    public TextMeshProUGUI monedasTxt;
    public TextMeshProUGUI balasTxt;
    public TextMeshProUGUI tiempoTxt;
    
    public void SetVidas(int vidas)
    {
        vidasTxt.text = "Vidas: " + vidas;
    }

    public void SetMonedas(int monedas)
    {
        monedasTxt.text = "Monedas: " + monedas;
    }

    public void SetBalas(int balas)
    {
        balasTxt.text = "Balas: " + balas;
    }

    public void SetTiempoTxt(int tiempo)
    {
        int minutos, segundos;
        minutos = tiempo / 60;
        segundos = tiempo % 60;
        string formatoMinSeg = string.Format("{0:00}:{1:00}", minutos, segundos);
        tiempoTxt.text = "Tiempo: " + formatoMinSeg;
    }

}
