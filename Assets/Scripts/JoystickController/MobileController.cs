
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image joystickBackgroundImage;
    [SerializeField]
    public Image joystickImage;
    private Vector2 inputVector;

    public void ActivateJoystick()
	{
        this.gameObject.SetActive(true);
	}

    public void DeactivateJoystick()
	{
        this.gameObject.SetActive(false);
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystickImage.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackgroundImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBackgroundImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBackgroundImage.rectTransform.sizeDelta.x);

            inputVector = new Vector2(pos.x, pos.y);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImage.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBackgroundImage.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBackgroundImage.rectTransform.sizeDelta.x / 2));
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }
}
