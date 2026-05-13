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
    FinishRepair

}

public class GameController : MonoBehaviour
{
    public GameState gameState;
    public static GameController Instance {get; private set;}

    [SerializeField] private Pipe pipe;
    [SerializeField] private Fish fish;


    public event Action<int> FindPipe;
    public event Action FindFish;
    public event Action win;
    public event Action BackToPipe;
    public event Action problemSolved;


    public float maxOxygen = 100f;
    public float oxygenLevel;
    public float lowOxygen = 10f;
    public PlayableDirector oxygenTimeline;
    [SerializeField] private float LoseO2;
    [SerializeField] private float AddO2;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameState = GameState.Start;
        oxygenLevel = maxOxygen;
    }

    void Update()
    {
        DecreaseO2();
        OxygenLevel();
        Debug.Log($"O2:{oxygenLevel}");

        if(pipe.found == true && gameState == GameState.Start)
        {
            FindPipe?.Invoke(pipe.pipeID);
            gameState = GameState.FindPipe;
        }

        if(fish.found == true && gameState == GameState.FindPipe)
        {
            FindFish?.Invoke();
            gameState = GameState.FightFish;
        }
        
        if(fish.die == true && gameState == GameState.FightFish)
        {
            gameState = GameState.Win;
            win?.Invoke();
        }

        if(pipe.found == true && gameState == GameState.Win)
        {
            BackToPipe?.Invoke();
            gameState = GameState.GetTool;
        }

        if(pipe.correctTool == true && gameState == GameState.GetTool)
        {
            gameState = GameState.FinishRepair;
        }

        if(gameState == GameState.FinishRepair)
        {
            problemSolved?.Invoke();
            Debug.Log("End");
        }
    }

    public void OxygenLevel()
    {
        if (oxygenLevel <= lowOxygen)
        {
            oxygenTimeline.Play();
        }
    }

    public void DecreaseO2()
    {
        oxygenLevel -= LoseO2;
        if (oxygenLevel <= 0f)
        {
            oxygenLevel = 0f;
        }
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




