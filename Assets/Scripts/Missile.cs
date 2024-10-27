using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Missile : MonoBehaviour
{
    public Vector2 targetPosition;// ミサイルが飛んでいく場所
    public float speed = 5f; // ミサイルの移動速度
    private Vector3 direction; // ミサイルの移動方向
    private GameObject reticle; // レティクルオブジェクトへの参照
    public GameObject explosionPrefab; // 爆発プレハブへの参照


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 現在位置から目的地への方向を取得
        direction = GetDirection(transform.position, targetPosition);

        // 移動
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // ミサイルのY軸回転を設定
        // Mathf.Atan2関数は、与えられたy（縦）とx（横）から、原点からの角度をラジアンで計算
        // Unityの回転は度（Degrees）で管理されるため、Mathf.Rad2Deg（ラジアンを度に変換する定数）を掛ける
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 回転
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // 目的地に達したら
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (explosionPrefab != null)
            {
                // 爆発を生成
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            if (reticle != null)
            {
                // レティクルを消す
                Destroy(reticle);
            }

            // ミサイルを消す
            Destroy(gameObject);
        }
    }

    // ミサイルの目的地への方向を計算する関数
    private Vector3 GetDirection(Vector3 currentPosition, Vector2 targetPosition)
    {
        // 現在の位置から目的地への方向を計算
        Vector3 direction = (targetPosition - (Vector2)currentPosition).normalized;
        return direction;
    }

    // レティクルオブジェクトを設定するための関数
    public void SetReticle(GameObject reticleObject)
    {
        this.reticle = reticleObject;
    }
}
