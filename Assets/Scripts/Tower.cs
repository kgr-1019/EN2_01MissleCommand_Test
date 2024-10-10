using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] public SpriteRenderer missile;// ミサイル
    [SerializeField] public Renderer towerRenderer;// タワー

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (towerRenderer != null)
        {
            // ミサイルをタワーの中心位置に生成
            Instantiate(missile, transform.position, Quaternion.identity);
        }
    }

    public void Shot()
    {
        
    }
}
