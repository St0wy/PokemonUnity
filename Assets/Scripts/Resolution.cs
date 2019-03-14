using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    [SerializeField]
    private int screenWidth = 160;
    [SerializeField]
    private int screenHeight = 144;
    [SerializeField]
    private bool isFullscreen = false;
    [SerializeField]
    private int preferredRefreshRate = 60;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(screenWidth, screenHeight, isFullscreen, preferredRefreshRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
