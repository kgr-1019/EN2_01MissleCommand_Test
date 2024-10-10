using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Missile : MonoBehaviour
{
    public Vector2 targetPosition;// �~�T�C�������ł����ꏊ
    public float speed = 5f; // �~�T�C���̈ړ����x
    

    // Start is called before the first frame update
    void Start()
    {
        // �}�E�X�̈ʒu���擾
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // �~�T�C���̖ڕW�ʒu���}�E�X�ʒu�ɐݒ�
        targetPosition = mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        // �~�T�C���ړ�
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        // ���݂̈ʒu����ړI�n�ւ̕������v�Z
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

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
            // �~�T�C��������
            Destroy(gameObject);
        }
    }

    
}