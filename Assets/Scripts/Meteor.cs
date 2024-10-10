using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Vector3 groundMin;
    private Vector3 groundMax;
    private Vector3 target;// �s��
    private Vector3 direction;
    public float speed = 5f; // �ړ����x
    private GameManager gameManager; // GameManager�ւ̎Q��

    void Start()
    {
        // �s��
        target = new Vector3(Random.Range(groundMin.x, groundMax.x), groundMax.y, groundMax.z);
        groundMax.z = 0;
        direction = (target - transform.position);
        // ���K������
        direction.Normalize();
    }

    void Update()
    {
        // �ړ�
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    public void SetUP(SpriteRenderer sp,GameManager gm)
    {
        // ground��Bounds���擾
        groundMin = sp.bounds.min;
        groundMax = sp.bounds.max;

        /*
         * GameManager�X�N���v�g�^��gameManager�Ƃ������g����̖��O�����̕ϐ���
         * ���g�����Ă�����
         */
        gameManager = gm;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            // �J������h�炷
            gameManager.StartCameraShake();

            // ���e�I��j�󂷂�
            Destroy(gameObject);
        }
    }
}
