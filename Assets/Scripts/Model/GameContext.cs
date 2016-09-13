using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class GameContext
    {
        public List<ObjectInitialData> dogs;
        public List<ObjectInitialData> cows;
        public List<CorralInitialData> corralData;
        public GameConditionData conditionData;
    }
}
