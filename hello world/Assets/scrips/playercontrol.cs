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
        //��ȡӢ�۽ṹ
         heroBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        fInput = Input.GetAxis("Horizontal");
        //����Ƿ񵽴����
        bGrounded = Physics2D.Linecast(transform.position, mGroundCheck.position, 
            1 << LayerMask.NameToLayer("Ground"));
        if (bGrounded)
        {
            //��ɫ��Ծ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                heroBody.AddForce(new Vector2(0, jumpForce));
            }
        }
        //��ɫת��
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
        //��ɫ�ƶ�����
        if (Mathf.Abs(heroBody.velocity.x) < maxSpeed)
            heroBody.AddForce(fInput * moveForce * Vector2.right);
        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed,
                heroBody.velocity.y);
    }
}
