using System;
using UnityEngine;
using UnityEngine.Playables;


public enum GameState
{
    Start,
    FindPipe,
    FightFish,
    Win,
    GetTool,
    FinishRepair,

}

public class GameController : MonoBehaviour
{
    public GameState[] gameState;
    public static GameController Instance {get; private set;}

    [SerializeField] private Pipe[] pipe;
    [SerializeField] private Fish[] fish;


    public event Action<int> FindPipe;
    public event Action FindFish;
    public event Action win;
    public event Action BackToPipe;
    public event Action problemSolved;
    public event Action die;
    //public event Action gameOver;


    public float maxOxygen = 100f;
    public float oxygenLevel;
    public float lowOxygen = 10f;
    public PlayableDirector oxygenTimeline;
    [SerializeField] private float LoseO2;
    [SerializeField] private float AddO2;
    [SerializeField] private int initialO2;
    [SerializeField] private GameObject _endUI;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameState[0] = GameState.Start;
        gameState[1] = GameState.Start;
        oxygenLevel = initialO2;
    }

    public bool oneOK = false;

    void Update()
    {
        DecreaseO2();
        OxygenLevel();

        for (int i = 0; i < pipe.Length; i++)
        {
            if(pipe[i].found == true && gameState[i] == GameState.Start)
            {
                FindPipe?.Invoke(pipe[i].pipeID);
                gameState[i] = GameState.FindPipe;
            }

            if(fish[i].found == true && gameState[i] == GameState.FindPipe)
            {
                FindFish?.Invoke();
                gameState[i] = GameState.FightFish;
            }

            if(fish[i].die == true && gameState[i] == GameState.FightFish)
            {
                gameState[i] = GameState.Win;
                win?.Invoke();
            }

            if(pipe[i].found == true && gameState[i] == GameState.Win)
            {
                BackToPipe?.Invoke();
                Debug.Log("back to pipe");
                gameState[i] = GameState.GetTool;
            }
            
            if(i == 0 && gameState[i] == GameState.GetTool)
            {
                if (pipe[i].correctTool1 == true)
                {
                    problemSolved?.Invoke();
                    gameState[i] = GameState.FinishRepair;
                }
            }
            else if (i == 1 && gameState[i] == GameState.GetTool)
            {
                if (pipe[i].correctTool2 == true)
                {
                    problemSolved?.Invoke();
                    gameState[i] = GameState.FinishRepair;
                }
            }

        }

        if(gameState[0] == GameState.FinishRepair && gameState[1] != GameState.FinishRepair)
        {
            oneOK = true;
        }
        else if(gameState[0] != GameState.FinishRepair && gameState[1] == GameState.FinishRepair)
        {
            oneOK = true;
        }

        if(gameState[0] == GameState.FinishRepair && gameState[1] == GameState.FinishRepair)
        {
            _endUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OxygenLevel()
    {
        if (oxygenLevel <= lowOxygen)
        {
            oxygenTimeline.Play();
        }
        if (oxygenLevel >= lowOxygen)
        {
            oxygenTimeline.Stop();
        }
    }

    public void DecreaseO2()
    {
        oxygenLevel -= LoseO2;
        if (oxygenLevel <= 0f)
        {
            oxygenLevel = 0f;
            die?.Invoke();
        }
    }

    public void KillNeedO2(float value)
    {
        oxygenLevel -= value;
    }

    public void IncreaseO2()
    {
        oxygenLevel += AddO2;
        if (oxygenLevel >= maxOxygen)
        {
            oxygenLevel = 100f;
        }
    }
}

//start的时候，鱼没有反应，pipe靠近了出发event findpipe
//靠近pipe弹窗之后对话结束变成findpipe，鱼会有反应聊，quest实现寻找fish
//靠近fish的时候fish会原地转圈，玩家可以通过点击E攻击，当鱼的生命值开始减少的时候进入FightFish
//鱼暂时不会回击，等鱼等生命值归0，进入win，这个时候玩家可以和鱼对话
//对话完之后变成GetTool，quest就变成找到tool然后可以去pipe那边修
//pipe靠近会有弹窗，提交tool之后就变成finishrepair。游戏结束




