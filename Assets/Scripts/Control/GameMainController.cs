using System.Collections.Generic;
using Control;
using UnityEngine;
using Model;

public class GameMainController : MonoBehaviour
{
    public static GameMainController Instance { get; private set; }        
	public DogObjectPool dogObjectPool;
	public CowObjectPool cowObjectPool;
	public CorralObjectPool corralObjectPool;
	public BonusObjectPool bonusObjectPool;
	public float timeScale = 1f;    
    public int CurrentLevel { get; private set; }
    public List<GameContext> levels;

    
    private ModelObjectManager _objectManager;
    public ControlPool ControllablePool { get; private set; }

    public EGameState GameState { get; private set; }

	public void Awake ()
	{
	    Instance = this;
        _objectManager = new ModelObjectManager();
        ControllablePool = new ControlPool();
        GameState = EGameState.Prepare;
    }
    
    public void StartGame(int level)
    {
        GameState = EGameState.Prepare;        
        ControllablePool.Clear();
        GameLogicController.Instance.Reset();
        PregareGame(level);
        GameState = EGameState.Play;
    }

    private void PregareGame(int level)
    {
	    GameInputController.Instance.Reset();
		if (levels.Count > 0 && levels.Count > level)
		{			
			CurrentLevel = level;
            var gameContext = levels[CurrentLevel];
            for (var i = 0; i < gameContext.dogs.Count; i++)
            {
                var dogData = gameContext.dogs[i];
	            var dog = dogObjectPool.GetModel();
                dog.Init(dogData);
                ControllablePool.AddControllable(dog);
                _objectManager.AddModel(dog);                
            }

            for (var i = 0; i < gameContext.cows.Count; i++)
            {
                var cowData = gameContext.cows[i];
	            var cow = cowObjectPool.GetModel();
                cow.Init(cowData);
                _objectManager.AddModel(cow);
            }

			for (var i = 0; i < gameContext.corralData.Count; i++)
			{
				var corralData = gameContext.corralData[i];				
				var corral = corralObjectPool.GetModel();
				corral.Init(corralData);
				_objectManager.AddModel(corral);
			}
			
			GameLogicController.Instance.Init(gameContext.conditionData);
        }       
    }

	public void GenerateBonusObject()
	{
		var bonusObject = bonusObjectPool.GetModel();
		var coord = new Vector2(Random.Range(1f,17f),Random.Range(1f,9f));
		var timeBonus = Constants.BonusObjectBonusTime;

		bonusObject.Init(coord,timeBonus);
		_objectManager.AddModel(bonusObject);
	}

	// main game loop
	public void Update ()
    {
	    if (GameState == EGameState.Play)
	    {
            var dt = Time.deltaTime * timeScale;
            if (_objectManager != null)
            {
                _objectManager.Update(dt);
            }
			GameLogicController.Instance.Update(dt);
			GameState = GameLogicController.Instance.GetCurrentState();
		    if (GameState == EGameState.Win || GameState == EGameState.Lose)
		    {
			    OnLevelComplete();
		    }
        }	          
    }

	private void OnLevelComplete()
	{
		dogObjectPool.Clear();
		cowObjectPool.Clear();
		corralObjectPool.Clear();
		bonusObjectPool.Clear();
	}

	public void Reset()
	{
		ControllablePool.Clear();
		GameLogicController.Instance.Reset();
		OnLevelComplete();
		GameState = EGameState.Prepare;
	}
	
	public void OnDestroy()
    {
        Instance = null;
    }
}
