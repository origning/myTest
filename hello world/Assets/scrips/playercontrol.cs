using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D  heroBody ;
    public float moveForce = 100;
    private float fInput = 0.0f;
    public float maxSpeed = 5;
    private bool bFaceRight= false ;
    private float jumpForce= 400f;
    private bool bGrounded=false ;
    Transform mGroundCheck;
    void Awake()
    {
        mGroundCheck = transform.Find("GroundCheck");
    }
    void Start()
    {
        //获取英雄结构
         heroBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        fInput = Input.GetAxis("Horizontal");
        //检测是否到达地面
        bGrounded = Physics2D.Linecast(transform.position, mGroundCheck.position, 
            1 << LayerMask.NameToLayer("Ground"));
        if (bGrounded)
        {
            //角色跳跃
            if (Input.GetKeyDown(KeyCode.Space))
            {
                heroBody.AddForce(new Vector2(0, jumpForce));
            }
        }
        //角色转身
        if (fInput < 0 && !bFaceRight)
        {
            flip();
        }
        if (fInput > 0 && bFaceRight)
        {
            flip();
        }
        void flip()
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            bFaceRight = !bFaceRight;
        }
    }
    void FixedUpdate()
    {
        //角色移动控制
        if (Mathf.Abs(heroBody.velocity.x) < maxSpeed)
            heroBody.AddForce(fInput * moveForce * Vector2.right);
        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed,
                heroBody.velocity.y);
    }
}
