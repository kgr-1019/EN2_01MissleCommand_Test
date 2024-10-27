using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPillar : Explosion
{
    // Update is called once per frame
    void Update()
    {
        ScaleUp();
    }

    protected override void ScaleUp()
    {
        Vector3 newScale = transform.localScale;

        // X���݂̂��g�傷��
        if (newScale.x < maxScale) newScale.x += scaleSpeed * Time.deltaTime;

        // �K�p
        transform.localScale = newScale;

        // �X�P�[�����ő�l�𒴂�����폜
        if (newScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }
}
