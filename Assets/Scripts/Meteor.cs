using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Meteor : MonoBehaviour
{
    private Vector3 groundMin;
    private Vector3 groundMax;
    private Vector3 target;// 行先
    private Vector3 direction;
    public float speed; // 移動速度
    private GameManager gameManager; // GameManagerへの参照
    public GameObject explosion; // 爆発プレハブへの参照
    
    void Start()
    {
        // 行先
        target = new Vector3(Random.Range(groundMin.x, groundMax.x), groundMax.y, groundMax.z);
        groundMax.z = 0;
        direction = (target - transform.position);
        // 正規化する
        direction.Normalize();
    }

    void Update()
    {
        // 移動
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    public void SetUP(SpriteRenderer sp,GameManager gm)
    {
        // groundのBoundsを取得
        groundMin = sp.bounds.min;
        groundMax = sp.bounds.max;

        /*
         * GameManagerスクリプト型のgameManagerという中身が空の名前だけの変数に
         * 中身を入れてあげる
         */
        gameManager = gm;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            // カメラを揺らす
            gameManager.StartCameraShake(0.5f,0.5f);

            // メテオを破壊する
            Destroy(gameObject);
        }
        
        if (collider.gameObject.CompareTag("Explosion"))
        {
            // 爆発を生成
            Instantiate(explosion, transform.position, Quaternion.identity);

            gameManager.AddScore(100); // スコアを加算

            // メテオを破壊する
            Destroy(gameObject);
        }
    }
}
