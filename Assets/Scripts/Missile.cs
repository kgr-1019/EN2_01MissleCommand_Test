using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Missile : MonoBehaviour
{
    public Vector2 targetPosition;// ミサイルが飛んでいく場所
    public float speed = 5f; // ミサイルの移動速度
    

    // Start is called before the first frame update
    void Start()
    {
        // マウスの位置を取得
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ミサイルの目標位置をマウス位置に設定
        targetPosition = mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        // ミサイル移動
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        // 現在の位置から目的地への方向を計算
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

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
            // ミサイルを消す
            Destroy(gameObject);
        }
    }

    
}
