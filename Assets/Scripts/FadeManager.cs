using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // �t�F�[�h�p��Image
    private float fadeDuration = 5f; // �t�F�[�h�ɂ����鎞��
    private bool isFading = false; // �t�F�[�h�����ǂ����̃t���O
    private float fadeElapsedTime = 0f; // �t�F�[�h�o�ߎ���

    public bool IsFading
    {
        get { return isFading; }
    }

    // �t�F�[�h�C���̊֐�
    public void FadeIn()
    {

        if (!isFading)
        {
            isFading = true;

            // Image��\�������āA�ŏ��͕\�����Ȃ�
            fadeImage.gameObject.SetActive(true);

            // ���������x��0�ɂ���
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;

            fadeElapsedTime = 0f; // �t�F�[�h���Ԃ����Z�b�g
        }
    }

    void Update()
    {
        if (isFading)
        {
            // �t�F�[�h�����̐i�s
            fadeElapsedTime += Time.deltaTime;

            // �A���t�@�l���X�V
            // �A���t�@�l�����̒l��1�𒴂��Ȃ��悤�ɂ���
            Color color = fadeImage.color;
            color.a = Mathf.Clamp01(fadeElapsedTime / fadeDuration);

            // �X�V���ꂽ�A���t�@�l��ݒ肷��
            fadeImage.color = color;

            // �t�F�[�h������������
            if (fadeElapsedTime >= fadeDuration)
            {
                // �t�F�[�h������ɃA���t�@��1�ɐݒ�
                color.a = 1;
                fadeImage.color = color;

                // �t�F�[�h����������������t���O�����Z�b�g
                isFading = false;
            }
        }
    }
}
