using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Missile : MonoBehaviour
{
    public Vector2 targetPosition;// �~�T�C�������ł����ꏊ
    public float speed = 5f; // �~�T�C���̈ړ����x
    private Vector3 direction; // �~�T�C���̈ړ�����
    private GameObject reticle; // ���e�B�N���I�u�W�F�N�g�ւ̎Q��
    public GameObject explosionPrefab; // �����v���n�u�ւ̎Q��


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���݈ʒu����ړI�n�ւ̕������擾
        direction = GetDirection(transform.position, targetPosition);

        // �ړ�
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // �~�T�C����Y����]��ݒ�
        // Mathf.Atan2�֐��́A�^����ꂽy�i�c�j��x�i���j����A���_����̊p�x�����W�A���Ōv�Z
        // Unity�̉�]�͓x�iDegrees�j�ŊǗ�����邽�߁AMathf.Rad2Deg�i���W�A����x�ɕϊ�����萔�j���|����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ��]
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // �ړI�n�ɒB������
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (explosionPrefab != null)
            {
                // �����𐶐�
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            if (reticle != null)
            {
                // ���e�B�N��������
                Destroy(reticle);
            }

            // �~�T�C��������
            Destroy(gameObject);
        }
    }

    // �~�T�C���̖ړI�n�ւ̕������v�Z����֐�
    private Vector3 GetDirection(Vector3 currentPosition, Vector2 targetPosition)
    {
        // ���݂̈ʒu����ړI�n�ւ̕������v�Z
        Vector3 direction = (targetPosition - (Vector2)currentPosition).normalized;
        return direction;
    }

    // ���e�B�N���I�u�W�F�N�g��ݒ肷�邽�߂̊֐�
    public void SetReticle(GameObject reticleObject)
    {
        this.reticle = reticleObject;
    }
}
