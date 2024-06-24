using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _maxHealth = default;   //Healthは秒数。何秒耐えられるかを表す
    [SerializeField] float m_scale = default;   //半径はスケールの1/2
    [SerializeField] float m_itemTime = default;
    [SerializeField] float _changeSceneTime;
    [SerializeField] Image m_umbrellaImage;
    [SerializeField] Image m_boatImage;
    [SerializeField] TextMeshProUGUI _time = default;
    [SerializeField] AudioClip _gameoverClip;
    [SerializeField] AudioClip _goalClip;
    [SerializeField] float _waitTime;
    [SerializeField] float _waitSpeed;
    [SerializeField] Image _panelImage;
    bool _sc = false;
    bool _sceneChange = false;
    Color _panelAlpha;
    public static float m_health;
    float m_maxHealth;
    float m_per;
    Vector2 _scale1;
    int _itemnum = 0;
    int _itemnum2 = 0;
    public static bool m_gameover = false;
    GameObject m_gameoverText;
    GameObject m_healthText;
    bool _raining;
    public static float m_timer;
    Move _move;
    public static bool _goal;


    private void Awake()  //値の引継ぎ用
    {
        _panelAlpha.a = 1;
        _panelImage.color = _panelAlpha;
        StartCoroutine(ChangedScene());
    }
    void Start()
    {
        _move = gameObject.GetComponent<Move>();
        //初期化
        m_maxHealth = _maxHealth;
        m_gameover = false;
        _goal = false;
        m_timer = 0;
        transform.position = new Vector2(-6.8f, -2f);
        _move.enabled = true;


        _scale1 = transform.localScale* m_scale;
        transform.localScale = _scale1;
        _itemnum = 0;
        m_gameoverText = GameObject.Find("GameoverText");
        m_healthText = GameObject.Find("Health");
        m_health = m_maxHealth;
        


    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Rain"&&_itemnum!=1)
    //    {
    //        Debug.Log("It is raining,");
    //        m_health -= Time.deltaTime;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Puddle"&&_itemnum2!=1)
        {
            Debug.Log("I fall down to puddle.");
            m_health = 0f;
        }

        if(collision.tag == "Umbrella")
        {
            Debug.Log("傘を手に入れた");
            _itemnum = 1;
            m_umbrellaImage.color = new Color(255,255,255,255);
            StartCoroutine(UmbrellamReset());
        }

        if (collision.tag == "Boat")
        {
            Debug.Log("船に乗った");
            _itemnum2 = 1;
            m_boatImage.color = new Color(255, 255, 255, 255);
            StartCoroutine(BoatReset());
            
        }

        if (collision.tag == "Rain")
        {
            _raining = true;
        }

        if ((collision.tag == "Goal"))
        {
            _goal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Rain")
        {
            _raining = false;
        }
    }

    void Update()
    {
        //体力表示
        if(m_healthText != null)
        {
            m_healthText.GetComponent<TextMeshProUGUI>().text = "Rest Health :" + m_health.ToString("F0");
        }
        

        //体力に応じて大きさを変える
        if (m_health > 0)
        {
            m_per = m_health / m_maxHealth;
            transform.localScale = _scale1 * m_per;
        }

        //体力処理
        if (_raining && _itemnum!=1 && m_health>0) 
        {
            m_health -= Time.deltaTime;
        }

        //タイム表示
        if (m_gameover == false)
        {
            m_timer += Time.deltaTime;
        }
        int _minutes = (int)m_timer / 60;
        int _secound = (int)m_timer % 60;
        if (_time != null)
        {
            _time.text = _minutes + ":" + _secound;
        }

        //gameoverの処理
        if (m_health <= 0f)
        {
            m_gameover = true;
        }

        if (m_gameover)
        {
            if (m_gameoverText != null)
            {
                m_gameoverText.GetComponent<TextMeshProUGUI>().text = "You melted";
            }
            if (_move.enabled == true)
            {
                _move.enabled = false;
            }
            AudioSource.PlayClipAtPoint(_gameoverClip,transform.position);
            _panelAlpha.a = 0;
            StartCoroutine(FadeIn());
            StartCoroutine(ChangeScene());
        }

        //ゴール処理
        if (_goal)
        {
            m_gameoverText.GetComponent<TextMeshProUGUI>().text = "Clear!";
            _move.enabled = false;
            //HealthSystem._instance._goal = true;
            //HealthSystem._instance.m_timer = m_timer;
            AudioSource.PlayClipAtPoint(_goalClip, transform.position);
            _panelAlpha.a = 0;
            StartCoroutine(FadeIn());
            StartCoroutine(ChangeScene());
        }
    }
    IEnumerator UmbrellamReset()
    {
        yield return new WaitForSeconds(m_itemTime);
        Debug.Log("アイテムが壊れた");
        _itemnum = 0;
        m_umbrellaImage.color = new Color(255, 255, 255, 0);
    }

    IEnumerator BoatReset()
    {
        yield return new WaitForSeconds(m_itemTime);
        Debug.Log("アイテムが壊れた");
        _itemnum2 = 0;
        m_boatImage.color = new Color(255, 255, 255, 0);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(_changeSceneTime);
        SceneManager.LoadScene("Result");
    }

    IEnumerator ChangedScene()
    {
        while (_sc != true)
        {
            _panelAlpha.a -= _waitSpeed;
            _panelImage.color = _panelAlpha;

            if (_panelAlpha.a <= 0)
            {
                _sc = true;
            }
            yield return new WaitForSeconds(_waitTime);
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(_changeSceneTime);
        while (_sceneChange != true)
        {
            _panelAlpha.a += _waitSpeed;
            _panelImage.color = _panelAlpha;

            if (_panelAlpha.a >= 1)
            {
                _sceneChange = true;
            }
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
