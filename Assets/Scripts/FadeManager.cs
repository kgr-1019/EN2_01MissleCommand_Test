using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // �t�F�[�h�p��Image
    private float fadeDuration = 5f; // �t�F�[�h�ɂ����鎞��
    private bool isFading = false; // �t�F�[�h�����ǂ����̃t���O

    // �t�F�[�h�C���̊֐�
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

        // Image��\�������āA�ŏ��͕\�����Ȃ�
        fadeImage.gameObject.SetActive(true);

        // ���������x��0�ɂ���
        Color color = fadeImage.color;
        color.a = 0;

        // ���݂̐F���擾���A�A���t�@�l��ύX���čĐݒ�
        fadeImage.color = color;

        // �t�F�[�h�o�ߎ���
        float elapsedTime = 0;

        // �t�F�[�h�C������
        while (elapsedTime < fadeDuration)
        {
            // elapsedTime�Ɍ��݂̃t���[�����o�߂������ԁiTime.deltaTime�j�����Z
            elapsedTime += Time.deltaTime;

            // �A���t�@�l�X�V
            //        // �A���t�@�l�����̒l��1�𒴂��Ȃ��悤�ɂ���
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);

            // �X�V���ꂽ�A���t�@�l��ݒ肷��
            fadeImage.color = color;

            // ���̃t���[����҂�
            yield return null;
        }

        // �t�F�[�h����������������t���O�����Z�b�g
        isFading = false;
    }
}
