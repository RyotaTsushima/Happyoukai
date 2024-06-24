using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] string _targetName;
    [SerializeField] float _waitTime;
    [SerializeField] float _waitSpeed;
    [SerializeField] Image _panelImage;
    bool _sceneChange = false;
    Color _panelAlpha;
    AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return)) 
        {
            _audio.Play();
            StartCoroutine(ChamgeScene());
        }
    }
    IEnumerator ChamgeScene()
    {
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
       
        SceneManager.LoadScene(_targetName);
    }
}
