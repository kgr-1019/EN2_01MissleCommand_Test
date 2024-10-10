using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private float shakeDuration; // シェイクの持続時間
    private float shakeMagnitude; // シェイクの強度
    private Vector3 originalPosition; // オリジナルのカメラ位置
    private float shakeTime; // シェイクの経過時間
    private float decrementMagnitude;// シェイクの減衰

    // Start is called before the first frame update
    void Start()
    {
        // 元のカメラの位置を保存
        originalPosition = transform.localPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        // シェイクが進行中であればシェイク処理を続ける
        if (shakeTime > 0)
        {
            // シェイクを減衰させる
            decrementMagnitude = shakeMagnitude * (shakeTime / shakeDuration);

            // ランダムなオフセットを計算
            Vector3 shakeOffset = Random.insideUnitSphere * decrementMagnitude;

            // カメラの新しい位置を設定
            transform.localPosition = originalPosition + shakeOffset;

            // 経過時間を減少
            shakeTime -= Time.deltaTime;
        }
        else
        {
            // シェイクが終わったら元の位置に戻す
            transform.localPosition = originalPosition;
        }
    }

    public void StartShake(float duration, float magnitude)
    {
        shakeDuration = duration; // シェイクの持続時間を設定
        shakeMagnitude = magnitude; // シェイクの強度を設定
        shakeTime = shakeDuration; // シェイクの経過時間を初期化
    }
}
