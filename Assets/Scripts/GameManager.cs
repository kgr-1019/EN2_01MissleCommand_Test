using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]public SpriteRenderer ground;//床
    [SerializeField] public Meteor meteor;// メテオ
    public List<Transform> meteorSpawnPoints;// 発生位置を格納するリスト
    private float spawnTimer;// メテオ生成時間間隔
    [SerializeField] private Shake cameraShake; // Shakeのインスタンス
    int maxHp = 100;// 最大HP
    int Hp;// 現在のHP
    public Slider slider;
    [SerializeField] public List<Tower> towers; // Towerオブジェクトを格納するリスト
    [SerializeField] public GameObject reticle;// レティクル
    private Vector2 mousePosition;// マウスの位置

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 100;//Sliderを最大にする
        Hp = maxHp; // 初期HPを設定
        
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

        // Towerのリストにコンポーネントをアサイン（Inspectorで設定されているはず）
        foreach (Tower tower in towers)
        {
            // 射撃可能か確認
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            // メテオを生成する
            SpawnMeteor();

            // タイマーリセット
            spawnTimer = 1;
        }

        // 左クリックで生成
        if (Input.GetMouseButtonDown(0))
        {
            // マウスの位置を取得
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // レティクルをマウスポジションに生成
            Instantiate(reticle, mousePosition, Quaternion.identity);
        }
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

    public void StartCameraShake()
    {
        // Shake クラスの StartShake メソッドを呼び出す
        cameraShake.StartShake(0.5f, 0.5f);

        // HPを減少させる
        ReduceHP(10); // 10HP減少させる
    }

    private void ReduceHP(int amount)
    {
        Hp -= amount;

        slider.value = (float)Hp;//HPをSliderに反映
        // HPが0未満にならないように制限
        if (Hp < 0)
        {
            Hp = 0;
        }

        Debug.Log("Current HP: " + Hp); // 現在のHPをログに出力
    }
}
