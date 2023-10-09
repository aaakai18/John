using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;

    //�Ƿ���������������
    bool m_IsplayerInRange;

    //������Ϸ�����ű�������������������Ϸ��������
    public GameEnding gameEnding;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //���������������߷�Χ�ڣ��������ߣ�ģ���۾������Ƿ��ܿ������
        if (m_IsplayerInRange)
        {

            Vector3 direction = player.position - transform.position + Vector3.up;
            //��������
            Ray ray = new Ray(transform.position, direction);

            //���߻��ж��󣬰���������ײ��Ϣ
            RaycastHit raycastHit;

            //ʹ������ϵͳ�������ߣ������ײ������
            //������һ��if�ж�
            //out ����ڶ���������������������Դ������ݵ������У����˸ú���return�����ģ������Դ�����������
            if(Physics.Raycast(ray,out raycastHit))
            {
                //��������������
                if(raycastHit.collider.transform==player)
                {
                    
                    gameEnding.CaughtPlayer();
                }
            }
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            m_IsplayerInRange = true;
        }
    }

    
}
