using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float scaleSpeed = 0.1f; // 拡大する速度
    public float maxScale = 3.0f;    // 最大スケール
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScaleUp();
    }

    // スケールを徐々に拡大する関数
    protected virtual void ScaleUp()
    {
        Vector3 newScale = transform.localScale;

        // X軸とY軸を拡大する
        if (newScale.x < maxScale) newScale.x += scaleSpeed * Time.deltaTime;
        if (newScale.y < maxScale) newScale.y += scaleSpeed * Time.deltaTime;

        // 適用
        transform.localScale = newScale;

        // スケールが最大値を超えたら削除
        if (newScale.x >= maxScale && newScale.y >= maxScale)
        {
            Destroy(gameObject);
        }
    }
}
