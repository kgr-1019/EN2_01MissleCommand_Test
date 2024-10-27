using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Meteor : MonoBehaviour
{
    private Vector3 groundMin;
    private Vector3 groundMax;
    private Vector3 target;// �s��
    private Vector3 direction;
    public float speed; // �ړ����x
    private GameManager gameManager; // GameManager�ւ̎Q��
    public GameObject explosion; // �����v���n�u�ւ̎Q��
    
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
            gameManager.StartCameraShake(0.5f,0.5f);

            // ���e�I��j�󂷂�
            Destroy(gameObject);
        }
        
        if (collider.gameObject.CompareTag("Explosion"))
        {
            // �����𐶐�
            Instantiate(explosion, transform.position, Quaternion.identity);

            gameManager.AddScore(100); // �X�R�A�����Z

            // ���e�I��j�󂷂�
            Destroy(gameObject);
        }
    }
}
