using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // フェード用のImage
    private float fadeDuration = 5f; // フェードにかかる時間
    private bool isFading = false; // フェード中かどうかのフラグ

    // フェードインの関数
    public void FadeIn()
    {
        if (!isFading)
        {
            StartCoroutine(FadeInCoroutine());
        }
    }

    private IEnumerator FadeInCoroutine()
    {
        isFading = true;

        // Imageを表示させて、最初は表示しない
        fadeImage.gameObject.SetActive(true);

        // 初期透明度を0にする
        Color color = fadeImage.color;
        color.a = 0;

        // 現在の色を取得し、アルファ値を変更して再設定
        fadeImage.color = color;

        float elapsedTime = 0;// フェード経過時間

        // フェードイン処理
        while (elapsedTime < fadeDuration)
        {
            // elapsedTimeに現在のフレームが経過した時間（Time.deltaTime）を加算
            elapsedTime += Time.deltaTime;

            // アルファ値更新
            // アルファ値が負の値や1を超えないようにする
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);

            // 更新されたアルファ値を設定する
            fadeImage.color = color;

            yield return null;// 次のフレームを待つ
        }

        // フェード処理が完了したらフラグをリセット
        isFading = false;
    }
}
