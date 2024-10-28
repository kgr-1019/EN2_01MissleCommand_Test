using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    float groundMaxHp = 1f;// �ő�HP
    float groundHp;// ���݂�HP
    public Score score; // Score�N���X�̎Q��
    private bool isGameOver = false; // �Q�[���I�[�o�[�t���O
    public FadeManager fadeManager; // FadeManager�̎Q��
    [SerializeField] private TextMeshProUGUI gameOverText; // Game Over �e�L�X�g
    [SerializeField] private TextMeshProUGUI resultScoreText; // Result Score �e�L�X�g
    [SerializeField] private TextMeshProUGUI pushResetText; // Push Reset �e�L�X�g
    private bool finishFadeIn = false;
    [SerializeField] public GameObject explosion;// ����
    private bool isSpawnExplosions = false; // �����𐶐������ǂ����̃t���O

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���I�[�o�[�e�L�X�g���\���ɂ���
        gameOverText.gameObject.SetActive(false);
        resultScoreText.gameObject.SetActive(false);
        pushResetText.gameObject.SetActive(false);

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
        towers.Clear(); // �����̃^���[���X�g���N���A
        Tower[] allTowers = FindObjectsOfType<Tower>();
        towers.AddRange(allTowers);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Tower: "+towers.Count);

        spawnTimer -= Time.deltaTime;

        // ground��HP��0�ȏ�̎�
        if (spawnTimer <= 0&&!finishFadeIn)
        {
            // ���e�I�𐶐�����
            SpawnMeteor();

            // �^�C�}�[���Z�b�g
            // �����Q�[���I�[�o�[�Ȃ�0,5f�A�����łȂ��Ȃ�1f
            spawnTimer = isGameOver ? 0.5f : 1f;
            meteor.speed = isGameOver ? 8f : 5f;
        }

        // ���N���b�N�Ń~�T�C������
        if (Input.GetMouseButtonDown(0))
        {
            if (isGameOver && finishFadeIn)
            {
                ResetGame();
            }
            else
            {
                // �܂��^���[���c���Ă���ꍇ�̂݃~�T�C���𐶐�
                if (towers.Count > 0)
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

    private IEnumerator SpawnExplosions()
    {
        isSpawnExplosions = true;

        // ��`���������̐�
        int explosionCount = 10;

        for (int i = 0; i < explosionCount; i++)
        {
            // �r���[�|�[�g�̃����_���Ȉʒu���v�Z
            float randomX = Random.Range(0f, 1f); // 0����1�͈̔͂Ń����_����X�l
            float randomY = Random.Range(0f, 1f); // 0����1�͈̔͂Ń����_����Y�l

            // �����_���Ȓ[�̈ʒu������
            Vector3 randomViewportPoint = new Vector3(randomX, randomY, 0);
            Vector3 worldPosition = Camera.main.ViewportToWorldPoint(randomViewportPoint);
            
            // Z����0�ɂ���
            worldPosition.z = 0;

            // �����𐶐�
            Instantiate(explosion, worldPosition, Quaternion.identity);

            // �����ҋ@
            yield return new WaitForSeconds(0.1f); // 0.1�b���Ƃɐ���
        }

        isSpawnExplosions = false; // ������������
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

        //HP��Slider�ɔ��f
        slider.value = (float)groundHp;

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
        if (groundHp <= 0 && !isGameOver)
        {
            isGameOver = true;
            fadeManager.FadeIn(); // FadeManager��FadeIn�֐����Ăяo��

            // �Q�[���I�[�o�[���ɔ����𐶐�
            StartCoroutine(SpawnExplosions());

            // �t�F�[�h�C���I����Ƀe�L�X�g��\��
            StartCoroutine(ShowGameOverText());
        }
    }

    private IEnumerator ShowGameOverText()
    {
        // FadeIn���\�b�h����������̂�҂�
        while (fadeManager.IsFading)
        {
            yield return null; // ���̃t���[����҂�
        }

        // Game Over �e�L�X�g��\��
        gameOverText.gameObject.SetActive(true);

        // �X�R�A���擾���ĕ\��
        int finalScore = score.GetScore();
        resultScoreText.text = "Score : " + finalScore.ToString("d7");
        resultScoreText.gameObject.SetActive(true);

        // Push Reset �e�L�X�g��\��
        pushResetText.gameObject.SetActive(true);

        finishFadeIn = true;
    }

    

    private void ResetGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
