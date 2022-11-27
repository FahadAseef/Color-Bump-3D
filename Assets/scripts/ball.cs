using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody rb;
    public float startforce;
    private Vector2 lastmousepos = Vector2.zero;
    public float walldistance;
    public float mincamdistance=5;

    void Start()
    {
        
    }


    void Update()
    {
        if (!GameManager.iswiner)
        {
            Vector2 deltapos = Vector2.zero;

            if (Input.GetMouseButton(0))
            {
                Vector2 currentpos = Input.mousePosition;
                if (lastmousepos == Vector2.zero)
                    lastmousepos = currentpos;
                deltapos = currentpos - lastmousepos;
                lastmousepos = currentpos;
                Vector3 force = new Vector3(deltapos.x, 0, deltapos.y) * startforce;
                rb.AddForce(force);
            }
            else
            {
                lastmousepos = Vector2.zero;
            }
            afterupdate();
        }
    }

    

    private void afterupdate()
    {
        Vector3 pos = this.transform.position;
        pos.x=Mathf.Clamp(pos.x,-walldistance,walldistance);
        float dist = Camera.main.transform.position.z + mincamdistance;
        if(transform.position.z < dist)
        {
            pos.z = dist;
        }
        //if (transform.position.x > walldistance)
        //{
        //    pos.x = walldistance;
        //}
        //else if(transform.position.x < -walldistance)
        //{
        //    pos.x = -walldistance;
        //}
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError(collision.gameObject);
        if (collision.gameObject.tag != this.tag)
        {
            Debug.Log("enemy");
            Time.timeScale = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "colorchanger")
        {
            if (this.tag == "player")
            {
                this.tag = "enemy";
                this.GetComponent<MeshRenderer>().sharedMaterial = GameManager.gm.enemymat;
            }
            else if (this.tag == "enemy")
            {
                this.tag = "player";
                this.GetComponent<MeshRenderer>().sharedMaterial = GameManager.gm.playermat;
            }
        }

        if (other.gameObject.tag == "win")
        {
            Debug.Log("winner winner chicken dinner");
            GameManager.gm.win();
        }
    }
}
