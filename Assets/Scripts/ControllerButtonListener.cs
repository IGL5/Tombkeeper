using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControllerButtonListener : MonoBehaviour
{
	[SerializeField] private InputActionReference _actionReference;

	// Eventos p�blicos de tipo UnityEvent
	[Space]
	public UnityEvent _onActionPerformed;
	public UnityEvent _onActionCancelled;

	// Se registran dos manejadores de eventos para la acci�n de entrada definida en _actionReference
	private void OnEnable()
	{
		_actionReference.action.performed += HandleOnActionPerformed;
		_actionReference.action.canceled += HandleOnActionCancelled;
	}

	// Se eliminan los manejadores de eventos registrados en OnEnable para evitar problemas de memoria
	// y fugas de eventos cuando el componente est� desactivado.
	private void OnDisable()
	{
		_actionReference.action.performed -= HandleOnActionPerformed;
		_actionReference.action.canceled -= HandleOnActionCancelled;
	}

	// Invocado cuando se realizan acciones de entrada
	private void HandleOnActionPerformed(InputAction.CallbackContext obj)
	{
		// Se ejecutan todas las acciones (o m�todos) que hayan sido suscritos a este evento
		// (tras la accion)
		_onActionPerformed.Invoke();
	}

	// Invocado cuando se cancelan las acciones de entrada
	private void HandleOnActionCancelled(InputAction.CallbackContext obj)
	{
		// Se ejecutan todas las acciones (o m�todos) que hayan sido suscritos a este evento
		// (tras la cancelacion de la accion)
		_onActionCancelled.Invoke();
	}
}
