using System.Collections.Generic;

namespace Model
{
    public class ModelObjectManager
    {
        private readonly List<IManageable> _models;

        public ModelObjectManager()
        {
            _models = new List<IManageable>();
        }

        public void AddModel(IManageable model)
        {
            if (!_models.Contains(model))
            {
                _models.Add(model);
            }
        }

        public void RemoveModel(IManageable model)
        {
            if (_models.Contains(model))
            {
                _models.Remove(model);
            }
        }

        public void Update(float dt)
        {
            for (int i = 0, iMax = _models.Count; i < iMax; i++)
            {
                var model = _models[i];
                if (model!=null && model.IsEnabled)
                    model.UpdateObject(dt);
            }
        }
		
        public void Clear()
        {
			_models.Clear();			
        }
    }
}
