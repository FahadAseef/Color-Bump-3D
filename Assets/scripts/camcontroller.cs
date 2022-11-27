using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camcontroller : MonoBehaviour
{
    public Transform ball;
    float offsetz;

    void Start()
    {
        offsetz = this.transform.position.z - ball.position.z;
    }

    private void  LateUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, ball.position.z + offsetz);
    }

}
