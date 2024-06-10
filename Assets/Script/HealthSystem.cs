using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_maxHealth = default;   //Healthは秒数。何秒耐えられるかを表す
    [SerializeField] float m_scale = default;   //半径はスケールの1/2
    [SerializeField] float m_itemTime = default;
    public static float m_health;
    float m_per;
    Vector2 _scale1;
    int _itemnum = 0;
    int _itemnum2 = 0;
    void Start()
    {
        _scale1 = transform.localScale* m_scale;
        transform.localScale = _scale1;
        _itemnum = 0;
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
            Debug.Log("傘を手に入れた");
            _itemnum = 1;
            StartCoroutine(ItemReset());
        }

        if (collision.tag == "Boat")
        {
            Debug.Log("船に乗った");
            _itemnum2 = 1;
            StartCoroutine(ItemReset());
        }
    }

    void Update()
    {
        if (m_health > 0)
        {
            m_per=m_health/m_maxHealth;
            transform.localScale = _scale1 * m_per;
        }
    }

    IEnumerator ItemReset()
    {
        yield return new WaitForSeconds(m_itemTime);
        _itemnum = 0;
        _itemnum2=0;
        Debug.Log("アイテムが壊れた");
    }
}
