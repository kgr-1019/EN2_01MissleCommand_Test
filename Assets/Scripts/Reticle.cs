using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public float rotationSpeed; // 1秒当たりの回転度数
    
    // Update is called once per frame
    void Update()
    {
        // 左回りに回転
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
