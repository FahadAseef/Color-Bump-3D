using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Material playermat;
    public Material enemymat;
    public Material planemat;
    public Color[] coloursarray;
    List<int>number=new List<int>();
    public GameObject winpopup;
    public static GameManager gm;
    public static int levelno;
    public GameObject[] levelarray;
    GameObject currentlevel;
    public Vector3 ballstartpos;
    public Transform ball;
    public static bool iswiner;
    public GameObject loadingpopup;

    void Start()
    {
        ballstartpos = ball.transform.position;
        levelno = 0;
        gm = this;
        setlevel();
        selectmaterial();
    }

    public void setlevel()
    {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
       // iswiner = false;
        Time.timeScale = 1;
        currentlevel = Instantiate(levelarray[levelno]);
        Invoke("iswinercall",.2f);
    }
    void iswinercall()
    {
         iswiner = false;
        loadingpopup.SetActive(false);
       // ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void selectmaterial()
    {
        for(int i = 0; i < coloursarray.Length; i++)
        {
            number.Add(i);
        }
        setmaterial(playermat,false);
        setmaterial(enemymat,false);
        setmaterial(planemat, false);
        setmaterial(planemat, true);

    }

    private void setmaterial(Material mat,bool iscam)
    {
        int index = number[UnityEngine.Random.Range(0, number.Count)];
        if(iscam)
            Camera.main.GetComponent<Camera>().backgroundColor=coloursarray[index];
        else
            mat.color=coloursarray[index];

        number.Remove(index);
    }

    public void win()
    {
        iswiner=true;
        Time.timeScale = 0.25f;
        winpopup.SetActive(true);
    }

    public void nextlevel()
    {
        Destroy(currentlevel);
        winpopup.SetActive(false);
        loadingpopup.SetActive(true);
        levelno++;
        if (levelno < levelarray.Length)
        {
            setlevel();
            ball.transform.position = ballstartpos;
        }
    }
}
