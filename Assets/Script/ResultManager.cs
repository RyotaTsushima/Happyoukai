using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] float _waitTime;
    [SerializeField] TextMeshProUGUI _clearTime;
    [SerializeField] TextMeshProUGUI _result;
    [SerializeField] float _changeTime;
    [SerializeField] float _waitSpeed;
    [SerializeField] Image _panelImage;
    bool _gameover;
    bool _goal;
    float _timer;
    float _minutes;
    float _seconds;
    Color _panelAlpha;
    bool _sceneChange = false;
    bool _sc;
    AudioSource _audio;

    // Start is called before the first frame update
    private void Awake()
    {
        _panelAlpha.a = 1;
        _panelImage.color = _panelAlpha;
        StartCoroutine(ChangeScene());
    }
    void Start()
    {
        //HealthSystem._instance.m_gameover;
        _gameover = HealthSystem.m_gameover;
        _goal = HealthSystem._goal;
        _timer = HealthSystem.m_timer;
        _minutes = _timer / 60;
        _seconds = _timer % 60;
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //シーン遷移
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            _audio.Play();
            StartCoroutine(ChangeScene("Title"));
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            _audio.Play();
            StartCoroutine(ChangeScene("GameScene"));
        }

        //テキスト
        if (_gameover == true)
        {
            _result.text = "You Melted.";
            _clearTime.text = "";
        }
        else if(_goal == true)
        {
            _result.text = "You Cleared !";
            _clearTime.text = "Clear Time   "+_minutes.ToString("N0") + ": " + _seconds.ToString("N0");
        }
    }

    IEnumerator ChangeScene(string sceneName)
    {
        while (_sceneChange != true)
        {
            
            _panelAlpha.a += _waitSpeed;
            _panelImage.color = _panelAlpha;

            if (_panelAlpha.a >= 1)
            {
                _sceneChange = true;
            }
            yield return new WaitForSeconds(_changeTime);
        }
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ChangeScene()
    {
        
        while (_sc != true)
        {
            _panelAlpha.a -= _waitSpeed;
            _panelImage.color = _panelAlpha;

            if (_panelAlpha.a <= 0)
            {
                _sc = true;
            }
            yield return new WaitForSeconds(_changeTime);
        }
    }
}
