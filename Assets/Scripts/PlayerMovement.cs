using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //����һ��3dʸ������ʾ��ҽ�ɫ�ƶ�
    Vector3 m_Movement;

    //����������ȡ�û�����
    float horizontal;
    float vertical;
    

    Rigidbody m_Rigidbody;
    Animator m_Animator;

    //��Ԫ������m_Rotation����ʾ3d��Ϸ�е���ת
    //��ʼ����Ԫ�����󣬳�ʼ��Ϊ����ת
    Quaternion m_Rotation = Quaternion.identity;

    public float turnSpeed = 20.0f;

    AudioSource m_AudioSoutce;
    // Start is called before the first frame update
    void Start()
    {
        //��ȡ���
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
        //���û�������װ��3d�˶�������άʸ��
        m_Movement.Set(horizontal, 0.0f, vertical);
        m_Movement.Normalize();//�ƶ��������Ϊ1����Ȼ������ͬʱ��1ʱ����ɫ�ƶ��ٶȻῴ��������

        //���ж��Ƿ�����ƶ�
        bool hasHorizontal = !Mathf.Approximately(horizontal, 0.0f);
        bool hasVeretical = !Mathf.Approximately(vertical, 0.0f);
        //ֻҪ��һ���ƶ�������ҽ�ɫ�ʹ����ƶ�״̬
        bool isWalking = (hasHorizontal || hasVeretical);
        //���������ݸ�����������
        m_Animator.SetBool("IsWalking", isWalking);

        //����άʸ������ʾ��ת����ҽ�ɫ
        //RotateTowards
        //����1��Ҫ���������
        //����2��Ŀ������
        //����3���˴���ת��������Ƕ�
        //����4������ת��������ʸ�����ȱ仯
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,m_Movement,turnSpeed*Time.deltaTime,0f);
        //������Ԫ��
        m_Rotation = Quaternion.LookRotation(desiredForward);

        //����߶������ŽŲ�����Ч
        if(isWalking)
        {
            //��֤ÿ�β��Ų����ظ����ţ�Ҳ���Ǵӵ�һ�뿪ʼ����
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

    //����������������ƶ�ʱִ�иú���
    private void OnAnimatorMove()
    {
        //ʹ�ô��û������ȡ����ʸ����Ϊ�ƶ����򣬶�����ÿ��0.02s�ƶ�������Ϊ����
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);

    }
}
