using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] public Missile missile;// �~�T�C��
    [SerializeField] public Renderer towerRenderer;// �^���[
    [SerializeField] public GameObject explosionPillarPrefab; // �����v���n�u�ւ̎Q��
    [SerializeField] private GameManager gameManager; // GameManager�ւ̎Q��
    private float coolTime = 3.0f; // �N�[���^�C��
    private float lastShotTime; // �Ō�Ƀ~�T�C���𔭎˂�������
    public bool isShot = true;// ���Ă邩�ǂ����̃t���O
    private int maxHP = 10; // �ő�HP
    private int currentHP; // ���݂�HP
    private Color originalColor; // ���̐F��ۑ����邽�߂̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = -coolTime; // �����ݒ�Ŋ��ɔ��ˉ\��Ԃɂ���
        currentHP = maxHP; // ���݂�HP���ő�HP�ŏ�����
        originalColor = towerRenderer.material.color; // ���̐F��ۑ�
    }

    // Update is called once per frame
    void Update()
    {
        // �N�[���^�C�����o�߂��Ă��邩�m�F����isShot���X�V
        if (Time.time >= lastShotTime + coolTime)
        {
            isShot = true; // �N�[���^�C�����߂����甭�ˉ\
            towerRenderer.material.color = originalColor; // �F�����ɖ߂�
        }
        else
        {
            // �N�[���^�C�����̐F�̕ω�
            float t = (Time.time - lastShotTime) / coolTime; // 0?1�͈̔͂ɐ��K��
            towerRenderer.material.color = Color.Lerp(Color.red, originalColor, t); // �F����
        }
    }

    public void Shot(Vector2 targetPosition, GameObject reticle)
    {
        // �N�[���^�C�����o�߂��Ă��邩�m�F
        if (isShot) // isShot�t���O���g���Ĕ��˂̉ۂ𔻒�
        {
            if (missile != null)
            {
                isShot = false;

                // �~�T�C�����^���[�̒��S�ʒu�ɐ���
                Missile missileInstance = Instantiate(missile, transform.position, Quaternion.identity);

                // ���������~�T�C���̃^�[�Q�b�g�ʒu��ݒ�
                missileInstance.targetPosition = targetPosition; // �ڕW�ʒu��ݒ�

                // ���e�B�N�����~�T�C���ɐݒ�
                missileInstance.SetReticle(reticle);

                // ���ˎ������L�^
                lastShotTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Meteor"))
        {
            // HP�����炷
            currentHP -= 4;

            // HP��0�ɂȂ�����^���[���폜
            if (currentHP <= 0)
            {
                // �J������h�炷
                gameManager.StartCameraShake(1f, 1f);

                // �����𐶐�
                if (explosionPillarPrefab != null)
                {
                    Instantiate(explosionPillarPrefab, transform.position, Quaternion.identity);
                }

                Destroy(gameObject); // ���g���폜

                // �^���[��GameManager����폜
                gameManager.RemoveTower(this);
            }

            // ���e�I��j�󂷂�
            Destroy(collider.gameObject);
        }
    }
}
