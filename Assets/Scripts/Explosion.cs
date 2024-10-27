using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float scaleSpeed = 0.1f; // �g�傷�鑬�x
    public float maxScale = 3.0f;    // �ő�X�P�[��
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScaleUp();
    }

    // �X�P�[�������X�Ɋg�傷��֐�
    protected virtual void ScaleUp()
    {
        Vector3 newScale = transform.localScale;

        // X����Y�����g�傷��
        if (newScale.x < maxScale) newScale.x += scaleSpeed * Time.deltaTime;
        if (newScale.y < maxScale) newScale.y += scaleSpeed * Time.deltaTime;

        // �K�p
        transform.localScale = newScale;

        // �X�P�[�����ő�l�𒴂�����폜
        if (newScale.x >= maxScale && newScale.y >= maxScale)
        {
            Destroy(gameObject);
        }
    }
}
