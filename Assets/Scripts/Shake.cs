using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private float shakeDuration; // �V�F�C�N�̎�������
    private float shakeMagnitude; // �V�F�C�N�̋��x
    private Vector3 originalPosition; // �I���W�i���̃J�����ʒu
    private float shakeTime; // �V�F�C�N�̌o�ߎ���
    private float decrementMagnitude;// �V�F�C�N�̌���

    // Start is called before the first frame update
    void Start()
    {
        // ���̃J�����̈ʒu��ۑ�
        originalPosition = transform.localPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        // �V�F�C�N���i�s���ł���΃V�F�C�N�����𑱂���
        if (shakeTime > 0)
        {
            // �V�F�C�N������������
            decrementMagnitude = shakeMagnitude * (shakeTime / shakeDuration);

            // �����_���ȃI�t�Z�b�g���v�Z
            Vector3 shakeOffset = Random.insideUnitSphere * decrementMagnitude;

            // �J�����̐V�����ʒu��ݒ�
            transform.localPosition = originalPosition + shakeOffset;

            // �o�ߎ��Ԃ�����
            shakeTime -= Time.deltaTime;
        }
        else
        {
            // �V�F�C�N���I������猳�̈ʒu�ɖ߂�
            transform.localPosition = originalPosition;
        }
    }

    public void StartShake(float duration, float magnitude)
    {
        shakeDuration = duration; // �V�F�C�N�̎������Ԃ�ݒ�
        shakeMagnitude = magnitude; // �V�F�C�N�̋��x��ݒ�
        shakeTime = shakeDuration; // �V�F�C�N�̌o�ߎ��Ԃ�������
    }
}
