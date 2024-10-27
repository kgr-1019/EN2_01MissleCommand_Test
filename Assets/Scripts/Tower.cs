using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] public Missile missile;// ミサイル
    [SerializeField] public Renderer towerRenderer;// タワー
    [SerializeField] public GameObject explosionPillarPrefab; // 爆発プレハブへの参照
    [SerializeField] private GameManager gameManager; // GameManagerへの参照
    private float coolTime = 3.0f; // クールタイム
    private float lastShotTime; // 最後にミサイルを発射した時間
    public bool isShot = true;// 撃てるかどうかのフラグ
    private int maxHP = 10; // 最大HP
    private int currentHP; // 現在のHP
    private Color originalColor; // 元の色を保存するための変数

    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = -coolTime; // 初期設定で既に発射可能状態にする
        currentHP = maxHP; // 現在のHPを最大HPで初期化
        originalColor = towerRenderer.material.color; // 元の色を保存
    }

    // Update is called once per frame
    void Update()
    {
        // クールタイムが経過しているか確認してisShotを更新
        if (Time.time >= lastShotTime + coolTime)
        {
            isShot = true; // クールタイムが過ぎたら発射可能
            towerRenderer.material.color = originalColor; // 色を元に戻す
        }
        else
        {
            // クールタイム中の色の変化
            float t = (Time.time - lastShotTime) / coolTime; // 0?1の範囲に正規化
            towerRenderer.material.color = Color.Lerp(Color.red, originalColor, t); // 色を補間
        }
    }

    public void Shot(Vector2 targetPosition, GameObject reticle)
    {
        // クールタイムが経過しているか確認
        if (isShot) // isShotフラグを使って発射の可否を判定
        {
            if (missile != null)
            {
                isShot = false;

                // ミサイルをタワーの中心位置に生成
                Missile missileInstance = Instantiate(missile, transform.position, Quaternion.identity);

                // 生成したミサイルのターゲット位置を設定
                missileInstance.targetPosition = targetPosition; // 目標位置を設定

                // レティクルをミサイルに設定
                missileInstance.SetReticle(reticle);

                // 発射時刻を記録
                lastShotTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Meteor"))
        {
            // HPを減らす
            currentHP -= 4;

            // HPが0になったらタワーを削除
            if (currentHP <= 0)
            {
                // カメラを揺らす
                gameManager.StartCameraShake(1f, 1f);

                // 爆発を生成
                if (explosionPillarPrefab != null)
                {
                    Instantiate(explosionPillarPrefab, transform.position, Quaternion.identity);
                }

                Destroy(gameObject); // 自身を削除

                // タワーをGameManagerから削除
                gameManager.RemoveTower(this);
            }

            // メテオを破壊する
            Destroy(collider.gameObject);
        }
    }
}
