using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _player;
    GameObject m_time;
    HealthSystem m_healthSystem;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        m_time = GameObject.Find("Time");
        m_healthSystem = _player.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //ƒ^ƒCƒ€•\Ž¦
        //if()
        _timer = Time.time;
        int _minutes = (int)_timer / 60;
        int _secound = (int)_timer % 60;
        m_time.GetComponent<TextMeshProUGUI>().text = _minutes + ":" + _secound;
    }
}
