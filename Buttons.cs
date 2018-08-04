using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public GameObject m_on, m_off;
    public Sprite layer_blue, layer_red;

    private void Start()
    {
        if (gameObject.name == "Musik")
        {
            if (PlayerPrefs.GetString("Musik") == "no")
            {
                m_on.SetActive(false);
                m_off.SetActive(true);
            }
            else
            {
                m_on.SetActive(true);
                m_off.SetActive(false);
            }
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = layer_red;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = layer_blue;
    }

    private void OnMouseUpAsButton()
    {
        if (PlayerPrefs.GetString("Musik") != "no")
            GameObject.Find("Click Audio").GetComponent<AudioSource>().Play();
        switch (gameObject.name)
        {
            case "Play":
                SceneManager.LoadScene("play");
                break;
            case "Rating":
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.TigroMed.Colors");
                break;
            case "Replay":
                SceneManager.LoadScene("play");
                break;
            case "Home":
                SceneManager.LoadScene("main");
                break;
            case "Facebook":
                Application.OpenURL("http://facebook.com");
                break;
            case "How To":
                SceneManager.LoadScene("howTo");
                break;
            case "Close":
                SceneManager.LoadScene("main");
                break;
            case "Musik":
                if (PlayerPrefs.GetString("Musik") != "no")
                {
                    PlayerPrefs.SetString("Musik", "no");
                    m_on.SetActive(false);
                    m_off.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetString("Musik", "yes");
                    m_on.SetActive(true);
                    m_off.SetActive(false);
                }
                break;
        }
    }
}
