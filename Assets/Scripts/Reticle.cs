using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public float rotationSpeed; // 1�b������̉�]�x��
    
    // Update is called once per frame
    void Update()
    {
        // �����ɉ�]
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
