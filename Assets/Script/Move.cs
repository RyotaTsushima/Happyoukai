using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float m_speed = default;
    [SerializeField] float m_jumpPower=default;
    Rigidbody2D m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //¶‰EˆÚ“®
        float h = Input.GetAxisRaw("Horizontal");
        m_rb.AddForce(Vector2.right*h*m_speed);

        //ƒWƒƒƒ“ƒvˆ—
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump!");
            m_rb.AddForce(Vector2.up * m_jumpPower*10, ForceMode2D.Impulse);
        }
    }
}
