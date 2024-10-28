using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]public SpriteRenderer ground;//床
    [SerializeField] public Meteor meteor;// メテオ
    [SerializeField] private Shake cameraShake; // Shake
    [SerializeField] public GameObject reticle;// レティクル
    [SerializeField] public List<Tower> towers; // Towerオブジェクトを格納するリスト
    public List<Transform> meteorSpawnPoints;// 発生位置を格納するリスト
    private float spawnTimer;// メテオ生成時間間隔
    public Slider slider;// スライダー
    float groundMaxHp = 1f;// 最大HP
    float groundHp;// 現在のHP
    public Score score; // Scoreクラスの参照
    private bool isGameOver = false; // ゲームオーバーフラグ
    public FadeManager fadeManager; // FadeManagerの参照
    [SerializeField] private TextMeshProUGUI gameOverText; // Game Over テキスト
    [SerializeField] private TextMeshProUGUI resultScoreText; // Result Score テキスト
    [SerializeField] private TextMeshProUGUI pushResetText; // Push Reset テキスト
    private bool finishFadeIn = false;
    [SerializeField] public GameObject explosion;// 爆発
    private bool isSpawnExplosions = false; // 爆発を生成中かどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        // ゲームオーバーテキストを非表示にする
        gameOverText.gameObject.SetActive(false);
        resultScoreText.gameObject.SetActive(false);
        pushResetText.gameObject.SetActive(false);

        slider.value = 10;//Sliderを最大にする
        groundHp = groundMaxHp; // 初期HPを設定
        
        spawnTimer = 0;

        // メテオ発生位置をリストに追加
        meteorSpawnPoints = new List<Transform>();
        // "SpawnPoint"タグを持つ全てのオブジェクトを検索してリストに追加
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        // spawnPointの数だけループする
        foreach (GameObject point in spawnPoints)
        {
            meteorSpawnPoints.Add(point.transform);
        }


        // ゲーム開始時にすべてのタワーをリストに追加
        towers.Clear(); // 既存のタワーリストをクリア
        Tower[] allTowers = FindObjectsOfType<Tower>();
        towers.AddRange(allTowers);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Tower: "+towers.Count);

        spawnTimer -= Time.deltaTime;

        // groundのHPが0以上の時
        if (spawnTimer <= 0&&!finishFadeIn)
        {
            // メテオを生成する
            SpawnMeteor();

            // タイマーリセット
            // もしゲームオーバーなら0,5f、そうでないなら1f
            spawnTimer = isGameOver ? 0.5f : 1f;
            meteor.speed = isGameOver ? 8f : 5f;
        }

        // 左クリックでミサイル生成
        if (Input.GetMouseButtonDown(0))
        {
            if (isGameOver && finishFadeIn)
            {
                ResetGame();
            }
            else
            {
                // まだタワーが残っている場合のみミサイルを生成
                if (towers.Count > 0)
                {
                    // マウスの位置を取得
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // すべてのタワーにミサイルを生成させる
                    foreach (Tower tower in towers)
                    {
                        // タワーが撃てるかどうかを確認
                        if (tower.isShot) // ここでisShotのフラグをチェック
                        {
                            // レティクルをマウスポジションに生成
                            GameObject reticleObject = Instantiate(reticle, mousePosition, Quaternion.identity);

                            // タワーにマウスの位置を渡してミサイルを発射
                            tower.Shot(mousePosition, reticleObject);

                            // isShotを一回通っていたら break する
                            break;
                        }
                    }
                }
            }
        }

        // ゲームオーバー判定
        CheckGameOver();
    }


    void SpawnMeteor()
    {
        // ランダムに発生位置を選択
        int randomIndex = Random.Range(0, meteorSpawnPoints.Count);
        Transform spawnPoint = meteorSpawnPoints[randomIndex];

        // メテオを生成
        Meteor meteorObj = Instantiate(meteor, spawnPoint.position, spawnPoint.rotation);

        // SetUP()関数を呼ぶ
        /*
         GameManagerスクリプトの全ての情報を欲しいから this
        　SetUP関数の引数の gm は this(GameManagerの情報)のこと
        */
        meteorObj.SetUP(ground,this);
    }

    private IEnumerator SpawnExplosions()
    {
        isSpawnExplosions = true;

        // 定義した爆発の数
        int explosionCount = 10;

        for (int i = 0; i < explosionCount; i++)
        {
            // ビューポートのランダムな位置を計算
            float randomX = Random.Range(0f, 1f); // 0から1の範囲でランダムなX値
            float randomY = Random.Range(0f, 1f); // 0から1の範囲でランダムなY値

            // ランダムな端の位置を決定
            Vector3 randomViewportPoint = new Vector3(randomX, randomY, 0);
            Vector3 worldPosition = Camera.main.ViewportToWorldPoint(randomViewportPoint);
            
            // Z軸は0にする
            worldPosition.z = 0;

            // 爆発を生成
            Instantiate(explosion, worldPosition, Quaternion.identity);

            // 少し待機
            yield return new WaitForSeconds(0.1f); // 0.1秒ごとに生成
        }

        isSpawnExplosions = false; // 爆発生成完了
    }

    // タワーをリストから削除する関数
    public void RemoveTower(Tower towerToRemove)
    {
        if (towers.Contains(towerToRemove))
        {
            towers.Remove(towerToRemove);
        }
    }

    public void StartCameraShake(float duration, float magnitude)
    {
        // Shake クラスの StartShake メソッドを呼び出す
        cameraShake.StartShake(duration, magnitude);

        // HPを減少させる
        ReduceHP(0.1f); // 1HP減少させる
    }


    private void ReduceHP(float amount)
    {
        groundHp -= amount;

        //HPをSliderに反映
        slider.value = (float)groundHp;

        // HPが0未満にならないように制限
        if (groundHp < 0)
        {
            groundHp = 0;
        }
    }

    public void AddScore(int amount)
    {
        if (score != null)
        {
            // 既存のスコアを取得し加算
            int newScore = score.GetScore() + amount;
            score.SetScore(newScore); // スコアを設定
        }
    }

    private void CheckGameOver()
    {
        // HPが0未満になった場合
        if (groundHp <= 0 && !isGameOver)
        {
            isGameOver = true;
            fadeManager.FadeIn(); // FadeManagerのFadeIn関数を呼び出す

            // ゲームオーバー時に爆発を生成
            StartCoroutine(SpawnExplosions());

            // フェードイン終了後にテキストを表示
            StartCoroutine(ShowGameOverText());
        }
    }

    private IEnumerator ShowGameOverText()
    {
        // FadeInメソッドが完了するのを待つ
        while (fadeManager.IsFading)
        {
            yield return null; // 次のフレームを待つ
        }

        // Game Over テキストを表示
        gameOverText.gameObject.SetActive(true);

        // スコアを取得して表示
        int finalScore = score.GetScore();
        resultScoreText.text = "Score : " + finalScore.ToString("d7");
        resultScoreText.gameObject.SetActive(true);

        // Push Reset テキストを表示
        pushResetText.gameObject.SetActive(true);

        finishFadeIn = true;
    }

    

    private void ResetGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
