using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]public SpriteRenderer ground;//��
    [SerializeField] public Meteor meteor;// ���e�I
    public List<Transform> meteorSpawnPoints;// �����ʒu���i�[���郊�X�g
    private float spawnTimer;// ���e�I�������ԊԊu
    [SerializeField] private Shake cameraShake; // Shake�̃C���X�^���X
    int maxHp = 100;// �ő�HP
    int Hp;// ���݂�HP
    public Slider slider;
    [SerializeField] public List<Tower> towers; // Tower�I�u�W�F�N�g���i�[���郊�X�g
    [SerializeField] public GameObject reticle;// ���e�B�N��
    private Vector2 mousePosition;// �}�E�X�̈ʒu

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 100;//Slider���ő�ɂ���
        Hp = maxHp; // ����HP��ݒ�
        
        spawnTimer = 0;

        // ���e�I�����ʒu�����X�g�ɒǉ�
        meteorSpawnPoints = new List<Transform>();

        // "SpawnPoint"�^�O�����S�ẴI�u�W�F�N�g���������ă��X�g�ɒǉ�
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        // spawnPoint�̐��������[�v����
        foreach (GameObject point in spawnPoints)
        {
            meteorSpawnPoints.Add(point.transform);
        }

        // Tower�̃��X�g�ɃR���|�[�l���g���A�T�C���iInspector�Őݒ肳��Ă���͂��j
        foreach (Tower tower in towers)
        {
            // �ˌ��\���m�F
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            // ���e�I�𐶐�����
            SpawnMeteor();

            // �^�C�}�[���Z�b�g
            spawnTimer = 1;
        }

        // ���N���b�N�Ő���
        if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X�̈ʒu���擾
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ���e�B�N�����}�E�X�|�W�V�����ɐ���
            Instantiate(reticle, mousePosition, Quaternion.identity);
        }
    }

    void SpawnMeteor()
    {
        // �����_���ɔ����ʒu��I��
        int randomIndex = Random.Range(0, meteorSpawnPoints.Count);
        Transform spawnPoint = meteorSpawnPoints[randomIndex];

        // ���e�I�𐶐�
        Meteor meteorObj = Instantiate(meteor, spawnPoint.position, spawnPoint.rotation);

        // SetUP()�֐����Ă�
        /*
         GameManager�X�N���v�g�̑S�Ă̏���~�������� this
        �@SetUP�֐��̈����� gm �� this(GameManager�̏��)�̂���
        */
        meteorObj.SetUP(ground,this);
    }

    public void StartCameraShake()
    {
        // Shake �N���X�� StartShake ���\�b�h���Ăяo��
        cameraShake.StartShake(0.5f, 0.5f);

        // HP������������
        ReduceHP(10); // 10HP����������
    }

    private void ReduceHP(int amount)
    {
        Hp -= amount;

        slider.value = (float)Hp;//HP��Slider�ɔ��f
        // HP��0�����ɂȂ�Ȃ��悤�ɐ���
        if (Hp < 0)
        {
            Hp = 0;
        }

        Debug.Log("Current HP: " + Hp); // ���݂�HP�����O�ɏo��
    }
}
