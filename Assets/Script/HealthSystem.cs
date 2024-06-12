using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_maxHealth = default;   //Health�͕b���B���b�ς����邩��\��
    [SerializeField] float m_scale = default;   //���a�̓X�P�[����1/2
    [SerializeField] float m_itemTime = default;
    public static float m_health;
    float m_per;
    Vector2 _scale1;
    int _itemnum = 0;
    int _itemnum2 = 0;
    bool m_gameover = false;
    GameObject m_gameoverText;
    GameObject m_healthText;
    void Start()
    {
        _scale1 = transform.localScale* m_scale;
        transform.localScale = _scale1;
        _itemnum = 0;
        m_gameoverText = GameObject.Find("GameoverText");
        m_healthText = GameObject.Find("Health");
        m_health = m_maxHealth;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Rain"&&_itemnum!=1)
        {
            Debug.Log("It is raining,");
            m_health -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Puddle"&&_itemnum!=1)
        {
            Debug.Log("I fall down to puddle.");
            m_health = 0f;
        }

        if(collision.tag == "Umbrella")
        {
            Debug.Log("�P����ɓ��ꂽ");
            _itemnum = 1;
            StartCoroutine(ItemReset());
        }

        if (collision.tag == "Boat")
        {
            Debug.Log("�D�ɏ����");
            _itemnum2 = 1;
            StartCoroutine(ItemReset());
        }
    }

    void Update()
    {
        //�̗͕\��
        m_healthText.GetComponent<TextMeshProUGUI>().text = "Rest Health :"+m_health.ToString("F0");

        //�̗͂ɉ����đ傫����ς���
        if (m_health > 0)
        {
            m_per = m_health / m_maxHealth;
            transform.localScale = _scale1 * m_per;
        }

        //gameover�̏���
        if (m_health <= 0f)
        {
            m_gameover = true;
        }

        if (m_gameover)
        {
            m_gameoverText.GetComponent<TextMeshProUGUI>().text = "You melted";
        }
    }

    IEnumerator ItemReset()
    {
        yield return new WaitForSeconds(m_itemTime);
        _itemnum = 0;
        _itemnum2=0;
        Debug.Log("�A�C�e������ꂽ");
    }
}
