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
    //��ʱ��
    float m_Timer;

    //����UI������ʾʱ��
    public float displayImageDuration = 1.0f;

    //����һ��CavasGroup ������ȡUi��ͼ��͸����
    public CanvasGroup exitBackgroundImageCanvasGroup;

   

    bool m_IsPlayerCaught;

    //��ʾ����ͼƬ����
    public Image image;

    //������������ֱ��ʾ��Ӳ���л�ȡ��Ϸ�ز�
    Sprite spriteCaught;
    Sprite spriteWon;

    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    bool m_HasAudioPlayed = false;

    void Start()
    {
        //׼���ز�
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

    //�������¼�
    private void OnTriggerEnter(Collider other)
    {
        //������봥����������Ҷ�������ΪTRUE
        if(other.gameObject ==player)
        {
            m_IsPlayerAtExit = true;
        }

    }

    //������ǰ�ؿ�
    void EndLevel(Sprite sprite,bool doRestart,AudioSource audioSource)
    {
        //�˳���ǰ��Ϸ,ֻ�д������ʱ�����˳�
        //Application.Quit();
        if(!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;

        //����͸���ȣ�ʹͼ��ﵽ����Ч��
        //imageCanvasGroup.alpha = m_Timer / fadeDuration;

        image.sprite = sprite;

        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;
        //����ʱ��������������͸���Ⱥ�������Ҫ����ͼƬ��������ʱ��֮�ͣ��˳���Ϸ
        if(m_Timer>fadeDuration+displayImageDuration)
        {
            if (doRestart)
            {
                //���س���
                SceneManager.LoadScene(0);
               
            }
            else
            {
                //�˳���ǰ��Ϸ,ֻ�д������ʱ�����˳�
                Application.Quit();
               // UnityEditor.EditorApplication.isPlaying = false;
            }

            
        }
        //�༭״̬�µ��˳���Ϸ



    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
}
