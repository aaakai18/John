using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //创建一个3d矢量来表示玩家角色移动
    Vector3 m_Movement;

    //创建变量获取用户输入
    float horizontal;
    float vertical;
    

    Rigidbody m_Rigidbody;
    Animator m_Animator;

    //四元数对象m_Rotation来表示3d游戏中的旋转
    //初始化四元数对象，初始化为不旋转
    Quaternion m_Rotation = Quaternion.identity;

    public float turnSpeed = 20.0f;

    AudioSource m_AudioSoutce;
    // Start is called before the first frame update
    void Start()
    {
        //获取组件
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSoutce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

       
    }


    private void FixedUpdate()
    {
        //将用户输入组装成3d运动所需三维矢量
        m_Movement.Set(horizontal, 0.0f, vertical);
        m_Movement.Normalize();//移动距离最大为1，不然当横纵同时加1时，角色移动速度会看起来更快

        //先判断是否横向移动
        bool hasHorizontal = !Mathf.Approximately(horizontal, 0.0f);
        bool hasVeretical = !Mathf.Approximately(vertical, 0.0f);
        //只要有一个移动，则玩家角色就处于移动状态
        bool isWalking = (hasHorizontal || hasVeretical);
        //将变量传递给动画管理器
        m_Animator.SetBool("IsWalking", isWalking);

        //用三维矢量来表示旋转后玩家角色
        //RotateTowards
        //参数1：要处理的向量
        //参数2：目标向量
        //参数3：此次旋转允许的最大角度
        //参数4：此旋转允许的最大矢量幅度变化
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,m_Movement,turnSpeed*Time.deltaTime,0f);
        //设置四元数
        m_Rotation = Quaternion.LookRotation(desiredForward);

        //如果走动，播放脚步声音效
        if(isWalking)
        {
            //保证每次播放不是重复播放，也就是从第一秒开始播放
            if(!m_AudioSoutce.isPlaying)
            {
                m_AudioSoutce.Play();
            }
            
        }
        else
        {
            m_AudioSoutce.Stop();
        }

    }

    //当动画播放引起根移动时执行该函数
    private void OnAnimatorMove()
    {
        //使用从用户输入获取到的矢量作为移动方向，动画中每次0.02s移动距离作为距离
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);

    }
}
