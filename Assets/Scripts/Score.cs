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
        // 0���߁B������𐮌`���ď��7���\��
        scoreLabel.text = "Score : " + scoreCount.ToString("d7");
    }

    // �V�����X�R�A���󂯎������A��ʂɔ��f�����邽�߂̊֐�
    public void UpdateScoreDisplay(int newScore)
    {
        scoreLabel.text = "Score : " + newScore.ToString("d7");
    }

    // �X�R�A���擾�B�X�R�A�𑼂̃N���X����擾���邽�߂̊֐�
    public int GetScore()
    {
        return scoreCount;
    }

    // �X�R�A��ݒ肷��B�X�R�A��ύX���邽�߂̊֐�
    public void SetScore(int score)
    {
        scoreCount = score;
        UpdateScoreDisplay(scoreCount);
    }
}
