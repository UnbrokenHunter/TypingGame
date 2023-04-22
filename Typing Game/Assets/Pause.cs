using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public static GameObject pMenu;

    void Start()
    {
        pMenu = GameObject.Find("Pause Menu"); pMenu.SetActive(false);
    }

    public void TogglePause()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
        if (pMenu.activeSelf)
        {
            pMenu.gameObject.SetActive(false);
        }
        else
        {
            pMenu.gameObject.SetActive(true);
        }
    }
    public void OnButtonPress()
    {
        Debug.Log("Button clicked ");
    }
    public void Update()
    {
        GetComponent<Button>().onClick.AddListener(TogglePause);

    }
}
