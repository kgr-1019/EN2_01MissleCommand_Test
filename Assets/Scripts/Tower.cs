using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] public SpriteRenderer missile;// �~�T�C��
    [SerializeField] public Renderer towerRenderer;// �^���[

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (towerRenderer != null)
        {
            // �~�T�C�����^���[�̒��S�ʒu�ɐ���
            Instantiate(missile, transform.position, Quaternion.identity);
        }
    }

    public void Shot()
    {
        
    }
}
