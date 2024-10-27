using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]public TMP_Text scoreLabel;
    private int scoreCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 0埋め。文字列を整形して常に7桁表示
        scoreLabel.text = "Score : " + scoreCount.ToString("d7");
    }

    // 新しいスコアを受け取ったら、画面に反映させるための関数
    public void UpdateScoreDisplay(int newScore)
    {
        scoreLabel.text = "Score : " + newScore.ToString("d7");
    }

    // スコアを取得。スコアを他のクラスから取得するための関数
    public int GetScore()
    {
        return scoreCount;
    }

    // スコアを設定する。スコアを変更するための関数
    public void SetScore(int score)
    {
        scoreCount = score;
        UpdateScoreDisplay(scoreCount);
    }
}
