//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameCntrl : MonoBehaviour {

    public GameObject pLost;

    public GameObject colBlock;
    public Vector3[] positions;
    private GameObject block;
    private GameObject[] blocks = new GameObject[4];

    private int rand, count;
    private float rCol, gCol, bCol;
    public Text score;
    private static Color aColor;

    private static int advCount = 0;
    private bool funcDone;

    [HideInInspector]
    public bool next, lose;

	void Start () {
        if (PlayerPrefs.GetString("NoADS") != "yes")
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize("1779937", false);
            }
            else
                Debug.Log("Platform is not supported");
        }

        count = 0;
        next = false;
        lose = false;
        rand = Random.Range(0, positions.Length);
        for (int i = 0; i < positions.Length; i++)
        {
            blocks[i] = Instantiate(colBlock, positions[i], Quaternion.identity) as GameObject;
            if (rand == i)
                block = blocks[i]; print(i);
        }
        block.GetComponent<RandCol>().right = true;
	}

	void Update () {
        if (lose && !funcDone)
            playerLose();
        if (next && !lose)
            nextColors();
	}

    void nextColors() {
        if (PlayerPrefs.GetString("Musik") != "no")
            GetComponent<AudioSource>().Play();
        count++;
        score.text = count.ToString();
        aColor = new Vector4(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), 1);
        GetComponent<Renderer>().material.color = aColor;
        next = false;

        //rCol = 1f;
        //gCol = 1f;
        //bCol = 1f;

        if (count < 3)
        {
            rCol = 0.9f;
            gCol = 0.9f;
            bCol = 0.9f;
        }
        else if (count >= 3 && count < 5)
        {
            rCol = 0.3f;
            gCol = 0.3f;
            bCol = 0.3f;
        }
        else if (count >= 5 && count < 8)
        {
            rCol = 0.2f;
            gCol = 0f;
            bCol = 0.2f;
        }
        else if (count >= 8)
        {
            rCol = 0f;
            gCol = 0f;
            bCol = 0.2f;
        }

        rand = Random.Range(0, positions.Length);
        for (int i = 0; i < positions.Length; i++) {
            if (i == rand)
            {
                blocks[i].GetComponent<Renderer>().material.color = aColor;
                print(i);
            }
            else
            {
                float temp = Random.Range(0.1f, rCol);
                float r = aColor.r + temp > 1f ? 1f : aColor.r + temp;
                temp = Random.Range(0.1f, gCol);
                float g = aColor.g + temp > 1f ? 1f : aColor.g + temp;
                temp = Random.Range(0.1f, bCol);
                float b = aColor.b + temp > 1f ? 1f : aColor.b + temp;
                blocks[i].GetComponent<Renderer>().material.color = new Vector4(r, g, b, aColor.a);
            }
        }
    }

    private void playerLose() {
        funcDone = true;
        if (PlayerPrefs.GetString("NoAds") != "yes") {
            advCount++;
            if (Advertisement.IsReady() && advCount % 5 == 0)
                Advertisement.Show();
        }

        print("Player lose");
        if (PlayerPrefs.GetInt("Score") < count)
            PlayerPrefs.SetInt("Score", count);
        pLost.SetActive(true);
        if (PlayerPrefs.GetString("Musik") == "no")
            pLost.GetComponent<AudioSource>().mute = true;
    }
}
