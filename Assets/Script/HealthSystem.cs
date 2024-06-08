using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_maxHealth = default;   //Health‚Í•b”B‰½•b‘Ï‚¦‚ç‚ê‚é‚©‚ð•\‚·
    [SerializeField] float m_scale = default;   //”¼Œa‚ÍƒXƒP[ƒ‹‚Ì1/2
    float m_health;
    float m_per;
    Vector2 _scale1;
    void Start()
    {
        _scale1 = transform.localScale* m_scale;
        transform.localScale = _scale1;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Rain")
        {
            Debug.Log("It is raining,");
            m_health -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Puddle")
        {
            Debug.Log("I fall down to puddle.");
            m_health = 0f;
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

    
}
