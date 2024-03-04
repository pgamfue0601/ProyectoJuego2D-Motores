using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardAds : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string GAME_ID = "5556603"; //replace with your gameID from dashboard. note: will be different for each platform.
    private const string VIDEO_PLACEMENT = "Anuncio1";
    public bool testMode = true;
    public Text t_msj;
    public Text t_puntos;
    private int PUNTOS = 0;
    private bool is_init = false;
    private bool is_load = false;


    //utility wrappers for debuglog
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

    public void Initialize()
    {
        if (!is_init)
        {
            if (Advertisement.isSupported)
            {
                DebugLog(Application.platform + " supported by Advertisement");
            }
            else
            {
                t_msj.text = "No está soportado el sistema UnityAds.";
            }
            t_msj.text = "Inicializando... Espere, por favor.";
            Advertisement.Initialize(GAME_ID, testMode, this);
        }
        else{
            t_msj.text = "Sistema inicializado correctamente";
        }
    }

    public void LoadNonRewardedAd()
    {
        if (is_init) {
            Advertisement.Load(VIDEO_PLACEMENT, this);
        } else {
            t_msj.text = "Inicializa antes de cargar";
        }
    }

    public void ShowNonRewardedAd()
    {
        if (is_load) {
            Advertisement.Show(VIDEO_PLACEMENT, this);
        }
        else {
            t_msj.text = "Carga antes de mostrar.";
        }
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
        t_msj.text = "Sistema inicializado correctamente";
        is_init = true;
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
        t_msj.text = "ERROR: No se puede inicializar.";
        is_init = false;
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
        t_msj.text = "Anuncio cargado. Pulse mostrar para ve el anuncio.";
        is_load = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
        t_msj.text = "ERROR: No se puede cargar el anuncio.";
        is_load = false;
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
        t_msj.text = "ERROR: No se puede mostrar el anuncio";
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        t_msj.text = "Mostrando anuncio...";
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if ($"{showCompletionState}" == "SKIPPED"){
            t_msj.text = "Lo siento, para obtener una vida mira el anuncio completamente";
        }
        if ($"{showCompletionState}" == "COMPLETED"){
            PUNTOS++;
            t_puntos.text = "Puntos: " + PUNTOS;
            t_msj.text = "Enhorabuena... Has ganado una vida";
        }
        is_load = false;
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
    }
    #endregion

     //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}