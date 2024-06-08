using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameObject m_player;
    GameObject m_health;
    GameObject m_time;
    HealthSystem HealthSystem;
    float m_nowHealth;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");
        HealthSystem = m_player.GetComponent<HealthSystem>();
        m_nowHealth = HealthSystem.m_health;
        m_health = GameObject.Find("Health");
        m_time = GameObject.Find("Time");
        
    }

    // Update is called once per frame
    void Update()
    {
        //�̗͕\��
        int _nowHealth = (int)m_nowHealth;
        m_health.GetComponent<TextMeshProUGUI>().text = "����"+_nowHealth+"�b";

        //�^�C���\��
        _timer = Time.time;
        int _minutes = (int)_timer / 60;
        int _secound = (int)_timer % 60;
        m_time.GetComponent<TextMeshProUGUI>().text = _minutes + ":" + _secound;
    }
}
