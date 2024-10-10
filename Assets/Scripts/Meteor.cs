using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Vector3 groundMin;
    private Vector3 groundMax;
    private Vector3 target;// 行先
    private Vector3 direction;
    public float speed = 5f; // 移動速度
    private GameManager gameManager; // GameManagerへの参照

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
            gameManager.StartCameraShake();

            // メテオを破壊する
            Destroy(gameObject);
        }
    }
}
