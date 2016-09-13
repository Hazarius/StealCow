using Model;

namespace Control
{
    public class GameLogicController
    {
        private static GameLogicController _instance;
        public static GameLogicController Instance
        {
            get { return _instance ?? (_instance = new GameLogicController()); }
        }
        
        public float MaxTime { get; private set; }
        public float CurrentTime { get; private set; }

        public int ExpectedCows { get; private set; }
        public int CurrentCows { get; private set; }
        
        public void Init(GameConditionData data)
        {
            ExpectedCows = data.requiredCow;
            MaxTime = data.maxTime;
        }

        public void Update(float dt)
        {
			CurrentTime += dt;
		}

        public void OnCowCorraled()
        {
            CurrentCows += 1;
        }

	    public void ApplyBonus(BonusObject bonusObject)
	    {
		    if (MaxTime > 0f)
		    {
				MaxTime += bonusObject.TimeBonus;
			}
	    }


		public EGameState GetCurrentState()
        {
            if (CurrentCows >= ExpectedCows)
            {
                return EGameState.Win;
            }
            if (MaxTime < 0f) return EGameState.Play;
            return CurrentTime < MaxTime ? EGameState.Play : EGameState.Lose;
        }

        public void Reset()
        {
            CurrentCows = 0;
            CurrentTime = 0f;
        }
    }
}
