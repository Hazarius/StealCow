using Control;
using UnityEngine;

namespace Model
{
    public class BaseModel : MonoBehaviour, IManageable, IControllable
    {
        public bool IsEnabled { get; protected set; }        
		public bool IsControlled { get { return TargetProvider != null; } }
		public Vector3 Position { get { return transform.position; } }

        protected Vector3 _destination;
		private Vector3 velocity = Vector3.zero;
		private Vector3 _previousDirection;        
        private float _speed;

        public IControllableProvider TargetProvider { get; private set; }

        protected EDirection direction;

        private EMovementState _movementState;

        public EMovementState MovementState
        {
            get { return _movementState; }
            private set
            {
                if (value != _movementState)
                {
                    _movementState = value;
                    OnSetNewState(value);
                }
            }
        }
        
        public void Init(ObjectInitialData data)
        {
            _speed = data.speed;
            transform.position = data.initialPosition;
            SetTarget(data.initialPosition);
			gameObject.SetActive(true);
	        InternalInit();
			IsEnabled = true;
        }
        
        public void UpdateObject(float dt)
        {
            UpdateMovement(dt);
            UpdateObjectInternal(dt);
        }

		protected virtual void InternalInit() { }
		protected virtual void OnSetNewState(EMovementState newState) { }
        protected virtual void UpdateObjectInternal(float dt) { }

        private void UpdateMovement(float dt)
        {
            if (TargetProvider != null) // follow behavior
            {
                _destination = TargetProvider.Position;
            }
            var movementVector = _destination - transform.position;
            var normalizedMovementVector = movementVector.normalized;
            if (_speed > 0f)
            {
                if (movementVector.sqrMagnitude > Constants.MovementTollerance)
                {                                       
                    transform.position = Vector3.SmoothDamp(transform.position, _destination, ref velocity, dt, _speed);
                    if (movementVector.sqrMagnitude > Constants.MovementTollerance && Vector3.Dot(_previousDirection, normalizedMovementVector) < (1f - Constants.MovementTollerance))
                    {
                        direction = GetDirection(normalizedMovementVector);
                        _previousDirection = normalizedMovementVector;
                    }
                    MovementState = EMovementState.Move;
                }
                else
                    MovementState = EMovementState.Idle;
            }                                   
        }

        private EDirection GetDirection(Vector2 inputDirection)
        {
            var main = Vector2.Dot(inputDirection, Vector2.up);
            var cross = Vector2.Dot(inputDirection, Vector2.left);
            if (main > 0.5f)
            {
                return EDirection.Up;
            }
            if (main < -0.5f)
            {
                return EDirection.Down;
            }
            return cross > 0f ? EDirection.Left : EDirection.Right;
        }

        public void SetTarget(Vector3 targetPosition)
        {
            _destination = targetPosition;
        }

        public void SetTargetProvider(IControllableProvider provider)
        {
            TargetProvider = provider;
        }

        public void Destroy()
        {
            Retain();
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void ClearInternal() { }

        public void Retain()
        {
	        TargetProvider = null;
			IsEnabled = false;
            ClearInternal();
            gameObject.SetActive(false);
        }        
    }
}
