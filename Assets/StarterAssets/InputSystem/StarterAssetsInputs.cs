using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool fire1,fire2;

		[Header("Dialogue Options")]
    public bool dialogue1, dialogue2, dialogue3;

    [Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

    public bool interactState = false;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnFire1(InputValue value){
      Fire1Input(value.isPressed);
    }

    public void OnFire2(InputValue value) {
      Fire2Input(value.isPressed);
    }

		public void OnInteractButton(InputValue value){
      InteractiveButtonInput(value.isPressed);
    }

    public void OnDialogue1(InputValue value) {
      Dialogue1Input(value.isPressed);
    }

    public void OnDialogue2(InputValue value) {
      Dialogue2Input(value.isPressed);
    }

    public void OnDialogue3(InputValue value) {
      Dialogue3Input(value.isPressed);
    }
		
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void Fire1Input(bool newFireState){
      fire1 = newFireState;
    }

    public void Fire2Input(bool newFireState) {
      fire2 = newFireState;

    }

    public void InteractiveButtonInput(bool newInteractState) {
      interactState = newInteractState;

    }

		public void Dialogue1Input(bool newDialogue1State){
      dialogue1 = newDialogue1State;
    }


    public void Dialogue2Input(bool newDialogue2State) {
      dialogue2 = newDialogue2State;
    }


    public void Dialogue3Input(bool newDialogue3State) {
      dialogue3 = newDialogue3State;
    }

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}