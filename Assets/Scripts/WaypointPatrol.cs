using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    //设置NavMeshAgent组件对象，用来获取当前游戏对象的导航网格代理组件
    NavMeshAgent navMeshAgent;

    //路径点数组
    public Transform[] waypoints;

    //当前数组下标
    int m_CurrentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        //获取组件
        navMeshAgent = GetComponent<NavMeshAgent>();

        //设置导航组件  导航路径的起始点位
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    //每次刷新都要去试着获取下一个路径点
    //如果满足条件，则指定下一个路径点
    //通过算法，让路径点循环
    // Update is called once per frame
    void Update()
    {
        //当前到指定路径点的距离 如果小于  最终停止距离
        if(navMeshAgent.remainingDistance<navMeshAgent.stoppingDistance)
        {
            //获取下一个路径点在数组中的索引数 
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;

            //设置新的位置，用该方法设置移动目标，参数是一个Vector3值      
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

            //在游戏中设置空对象来表示导航点
        }
    }
}
