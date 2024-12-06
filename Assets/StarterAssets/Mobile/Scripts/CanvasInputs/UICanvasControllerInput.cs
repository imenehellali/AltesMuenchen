using Unity.VisualScripting;
using UnityEngine;


    public class UICanvasControllerInput : MonoBehaviour
    {
   
    public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            MovementComponent.OnMoveInput.Invoke(virtualMoveDirection);
        }
        
        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
              LookComponent.OnLookInput.Invoke(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            JumpComponent.OnJumpTriggered.Invoke(virtualJumpState);
        }
        
        public void VirtualSprintInput(bool virtualSprintState)
        {
            RunComponent.OnRunTriggered.Invoke(virtualSprintState);
        }
        public void VirtualSimpleAttack(bool virtualSimpleAttackState)
    {
        SimpleAttackComponent.OnSimpleAttackTriggered.Invoke(virtualSimpleAttackState);
    }
        public void VirtualSpecialAttack(bool virtualSpecialAttackState) 
    { 
        SpecialAttackComponent.OnSpecialAttackTriggered.Invoke(virtualSpecialAttackState);  
    }
   
}


