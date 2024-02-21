using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidScript : MonoBehaviour
{
    private void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
