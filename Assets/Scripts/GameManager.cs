using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] adsButtons;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        

    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button button in adsButtons)
        {
            //button.GetComponent<RewardAds>().LoadAd();
            //button.GetComponent<RewardAds>().OnUnityAdsAdLoaded("Anuncio1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
