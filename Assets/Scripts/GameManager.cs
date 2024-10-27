using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]public SpriteRenderer ground;//��
    [SerializeField] public Meteor meteor;// ���e�I
    [SerializeField] private Shake cameraShake; // Shake
    [SerializeField] public GameObject reticle;// ���e�B�N��
    [SerializeField] public List<Tower> towers; // Tower�I�u�W�F�N�g���i�[���郊�X�g
    public List<Transform> meteorSpawnPoints;// �����ʒu���i�[���郊�X�g
    private float spawnTimer;// ���e�I�������ԊԊu
    public Slider slider;// �X���C�_�[
    float groundMaxHp = 1;// �ő�HP
    float groundHp;// ���݂�HP
    public Score score; // Score�N���X�̎Q��
    private bool isGameOver = false; // �Q�[���I�[�o�[�t���O
    //[SerializeField] private Image fadeImage; // �t�F�[�h�p��Image
    //private float fadeDuration = 5f; // �t�F�[�h�ɂ����鎞��
    //private bool isFading = false; // �t�F�[�h�����ǂ����̃t���O
    public FadeManager fadeManager; // FadeManager�̎Q��

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 10;//Slider���ő�ɂ���
        groundHp = groundMaxHp; // ����HP��ݒ�
        
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

        // �Q�[���J�n���ɂ��ׂẴ^���[�����X�g�ɒǉ�
        Tower[] allTowers = FindObjectsOfType<Tower>();
        towers.AddRange(allTowers);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        // ground��HP��0�ȏ�̎�
        if (spawnTimer <= 0)
        {
            // ���e�I�𐶐�����
            SpawnMeteor();

            // �^�C�}�[���Z�b�g
            // �����Q�[���I�[�o�[�Ȃ�0,5f�A�����łȂ��Ȃ�1f
            spawnTimer = isGameOver ? 0.2f : 1f;
            meteor.speed = isGameOver ? 8f : 5f;
        }

        // ���N���b�N�Ń~�T�C������
        if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X�̈ʒu���擾
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���ׂẴ^���[�Ƀ~�T�C���𐶐�������
            foreach (Tower tower in towers)
            {
                // �^���[�����Ă邩�ǂ������m�F
                if (tower.isShot) // ������isShot�̃t���O���`�F�b�N
                {
                    // ���e�B�N�����}�E�X�|�W�V�����ɐ���
                    GameObject reticleObject = Instantiate(reticle, mousePosition, Quaternion.identity);

                    // �^���[�Ƀ}�E�X�̈ʒu��n���ă~�T�C���𔭎�
                    tower.Shot(mousePosition, reticleObject);

                    // isShot�����ʂ��Ă����� break ����
                    break;
                }
            }
        }

        // �Q�[���I�[�o�[����
        CheckGameOver();
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

    // �^���[�����X�g����폜����֐�
    public void RemoveTower(Tower towerToRemove)
    {
        if (towers.Contains(towerToRemove))
        {
            towers.Remove(towerToRemove);
        }
    }

    public void StartCameraShake(float duration, float magnitude)
    {
        // Shake �N���X�� StartShake ���\�b�h���Ăяo��
        cameraShake.StartShake(duration, magnitude);

        // HP������������
        ReduceHP(0.1f); // 1HP����������
    }


    private void ReduceHP(float amount)
    {
        groundHp -= amount;

        slider.value = (float)groundHp;//HP��Slider�ɔ��f
        // HP��0�����ɂȂ�Ȃ��悤�ɐ���
        if (groundHp < 0)
        {
            groundHp = 0;
        }
    }

    public void AddScore(int amount)
    {
        if (score != null)
        {
            // �����̃X�R�A���擾�����Z
            int newScore = score.GetScore() + amount;
            score.SetScore(newScore); // �X�R�A��ݒ�
        }
    }

    private void CheckGameOver()
    {
        // HP��0�����ɂȂ����ꍇ
        if (groundHp <= 0)
        {
            isGameOver = true;
            fadeManager.FadeIn(); // FadeManager��FadeIn���\�b�h���Ăяo��
            //StartCoroutine(FadeIn());
        }
    }

    //private IEnumerator FadeIn()
    //{
    //    isFading = true;

    //    // Image��\�������āA�ŏ��͕\�����Ȃ�
    //    fadeImage.gameObject.SetActive(true);

    //    // ���������x��0�ɂ���
    //    Color color = fadeImage.color;
    //    color.a = 0;

    //    // ���݂̐F���擾���A�A���t�@�l��ύX���čĐݒ�
    //    fadeImage.color = color;

    //    float elapsedTime = 0;
    //    // �t�F�[�h�C������
    //    while (elapsedTime < fadeDuration)
    //    {
    //        // elapsedTime�Ɍ��݂̃t���[�����o�߂������ԁiTime.deltaTime�j�����Z
    //        elapsedTime += Time.deltaTime;

    //        // �A���t�@�l�X�V
    //        // �A���t�@�l�����̒l��1�𒴂��Ȃ��悤�ɂ���
    //        color.a = Mathf.Clamp01(elapsedTime / fadeDuration);

    //        // �X�V���ꂽ�A���t�@�l��ݒ肷��
    //        fadeImage.color = color;

    //        // ���̃t���[����҂�
    //        yield return null;
    //    }
    //}
}
