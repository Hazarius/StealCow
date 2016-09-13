using System;
using UnityEngine;

namespace Model
{
    public class Cow : BaseModel
    {       
        public Animator animator;
        private readonly int _hashedMoveState = Animator.StringToHash("Move");
        private readonly int _hashedDirection = Animator.StringToHash("Direction");
        private readonly int DirectionSides = Enum.GetNames(typeof (EDirection)).Length;

		void OnEnable()
        {
            SetAnimatinState(EMovementState.Idle);
        }

        protected override void UpdateObjectInternal(float dt)
        {
            if (animator != null)
            {
                var value = (float)direction/(DirectionSides-1);
                animator.SetFloat(_hashedDirection, value);
            }
        }

        protected override void OnSetNewState(EMovementState newState)
        {
            SetAnimatinState(newState);            
        }

        private void SetAnimatinState(EMovementState newState)
        {
            if (animator != null)
            {
                switch (newState)
                {
                    case EMovementState.Idle:
                        animator.SetBool(_hashedMoveState, false);
                        break;
                    case EMovementState.Move:
                        animator.SetBool(_hashedMoveState, true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("newState", newState, null);
                }
            }            
        }        
    }
}
