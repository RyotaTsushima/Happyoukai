using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    AudioSource m_audio;
    AudioClip m_clip;
    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        if (m_audio != null)
        {
            m_clip = m_audio.clip;
        }
        else
        {
            Debug.Log("AudioSource‚ª‚ ‚è‚Ü‚¹‚ñ");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("{name} ‚Í”j‰ó‚³‚ê‚Ü‚µ‚½");
            Destroy(this.gameObject);

            if (m_clip != null)
            {
                AudioSource.PlayClipAtPoint(m_clip, transform.position);
            }
            else
            {
                Debug.Log("Clip‚ª‚ ‚è‚Ü‚¹‚ñ");
            }
        }
    }
}
