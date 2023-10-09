using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{

    bool m_IsPlayerAtExit = false;
    public GameObject player;
    public float fadeDuration = 1.0f;
    //计时器
    float m_Timer;

    //正常UI结束显示时间
    public float displayImageDuration = 1.0f;

    //声明一个CavasGroup 用来获取Ui中图像透明度
    public CanvasGroup exitBackgroundImageCanvasGroup;

   

    bool m_IsPlayerCaught;

    //显示结束图片对象
    public Image image;

    //创建两个对象分别表示从硬盘中获取游戏素材
    Sprite spriteCaught;
    Sprite spriteWon;

    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    bool m_HasAudioPlayed = false;

    void Start()
    {
        //准备素材
        spriteCaught = Resources.Load<Sprite>("Caught");
        spriteWon = Resources.Load<Sprite>("Won");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsPlayerAtExit)
        {
            EndLevel(spriteWon,false,exitAudio);
        }
        else if(m_IsPlayerCaught)
        {
            EndLevel(spriteCaught,true,caughtAudio);
        }
    }

    //触发器事件
    private void OnTriggerEnter(Collider other)
    {
        //如果进入触发器的是玩家对象，则设为TRUE
        if(other.gameObject ==player)
        {
            m_IsPlayerAtExit = true;
        }

    }

    //结束当前关卡
    void EndLevel(Sprite sprite,bool doRestart,AudioSource audioSource)
    {
        //退出当前游戏,只有打包运行时才能退出
        //Application.Quit();
        if(!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;

        //设置透明度，使图像达到渐变效果
        //imageCanvasGroup.alpha = m_Timer / fadeDuration;

        image.sprite = sprite;

        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;
        //当计时器大于我们设置透明度和我们想要结束图片清晰呈现时间之和，退出游戏
        if(m_Timer>fadeDuration+displayImageDuration)
        {
            if (doRestart)
            {
                //重载场景
                SceneManager.LoadScene(0);
               
            }
            else
            {
                //退出当前游戏,只有打包运行时才能退出
                Application.Quit();
               // UnityEditor.EditorApplication.isPlaying = false;
            }

            
        }
        //编辑状态下的退出游戏



    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
}
