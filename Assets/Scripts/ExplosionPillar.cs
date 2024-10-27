using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPillar : Explosion
{
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        ScaleUp();
    }

    protected override void ScaleUp()
    {
        Vector3 newScale = transform.localScale;

        // X軸のみを拡大する
        if (newScale.x < maxScale) newScale.x += scaleSpeed * Time.deltaTime;

        // 適用
        transform.localScale = newScale;

        // スケールが最大値を超えたら削除
        if (newScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }
}
