using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoblieFlowController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public MobileController controller;

	public bool isFlowController = false;

	public Vector3 defaultPosition = new Vector3();

	private void Start()
	{
		RectTransform rectTransform = this.GetComponent<RectTransform>();

		defaultPosition = controller.transform.position;
	}

	// ===============================
	//
	// ТОЛЬКО ДЛЯ ОТЛАДКИ, УДАЛИ ПОТОМ
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			SetFlowController();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			SetStaticController();
		}
	}   
	// 
	// ===============================

	public void SetFlowController()
	{
		isFlowController = true;

		this.GetComponent<Image>().raycastTarget = true;
		controller.joystickBackgroundImage.raycastTarget = false;

		controller.transform.position = defaultPosition;
		controller.DeactivateJoystick();
	}

	public void SetStaticController()
	{
		isFlowController = false;

		this.GetComponent<Image>().raycastTarget = false;
		controller.joystickBackgroundImage.raycastTarget = true;

		controller.transform.position = defaultPosition;
		controller.ActivateJoystick();
	}

	public void OnDrag(PointerEventData eventData)
	{
		controller.OnDrag(eventData);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		controller.ActivateJoystick();
		controller.transform.position = eventData.position;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		controller.OnPointerUp(eventData);
		controller.DeactivateJoystick();
	}
}
