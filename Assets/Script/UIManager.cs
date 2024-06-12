using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    GameObject m_time;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        m_time = GameObject.Find("Time");  
    }

    // Update is called once per frame
    void Update()
    {
        //タイム表示
        _timer = Time.time;
        int _minutes = (int)_timer / 60;
        int _secound = (int)_timer % 60;
        m_time.GetComponent<TextMeshProUGUI>().text = _minutes + ":" + _secound;
    }
}
