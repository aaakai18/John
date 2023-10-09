using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;

    //是否进入敌人视线区域
    bool m_IsplayerInRange;

    //声明游戏结束脚本组件类对象，用来调用游戏结束代码
    public GameEnding gameEnding;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //如果玩家在它的视线范围内，则发射射线，模拟眼睛，看是否能看到玩家
        if (m_IsplayerInRange)
        {

            Vector3 direction = player.position - transform.position + Vector3.up;
            //创建射线
            Ray ray = new Ray(transform.position, direction);

            //射线击中对象，包含射线碰撞信息
            RaycastHit raycastHit;

            //使用物理系统发射射线，如果碰撞到物体
            //则进入第一层if判断
            //out 代表第二个参数是输出参数，可以带出数据到参数中，除了该函数return出来的，还可以带出其他参数
            if(Physics.Raycast(ray,out raycastHit))
            {
                //如果碰到的是玩家
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
