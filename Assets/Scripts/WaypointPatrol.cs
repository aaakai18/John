using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    //����NavMeshAgent�������������ȡ��ǰ��Ϸ����ĵ�������������
    NavMeshAgent navMeshAgent;

    //·��������
    public Transform[] waypoints;

    //��ǰ�����±�
    int m_CurrentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        //��ȡ���
        navMeshAgent = GetComponent<NavMeshAgent>();

        //���õ������  ����·������ʼ��λ
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    //ÿ��ˢ�¶�Ҫȥ���Ż�ȡ��һ��·����
    //���������������ָ����һ��·����
    //ͨ���㷨����·����ѭ��
    // Update is called once per frame
    void Update()
    {
        //��ǰ��ָ��·����ľ��� ���С��  ����ֹͣ����
        if(navMeshAgent.remainingDistance<navMeshAgent.stoppingDistance)
        {
            //��ȡ��һ��·�����������е������� 
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;

            //�����µ�λ�ã��ø÷��������ƶ�Ŀ�꣬������һ��Vector3ֵ      
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

            //����Ϸ�����ÿն�������ʾ������
        }
    }
}
