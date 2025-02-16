using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle; // Corrected from SwitchToggle to Toggle
    Vector2 handlePosition;

    void Awake() {
        toggle = GetComponent<Toggle>();
        if (toggle == null) {
            Debug.LogError("Toggle component not found!", this);
            return;
        }

        handlePosition = uiHandleRectTransform != null ? uiHandleRectTransform.anchoredPosition : Vector2.zero;
        
        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    void OnSwitch(bool on) {
        if (uiHandleRectTransform != null)
            uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
    
        backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor;
        handleImage.color = on ? handleActiveColor : handleDefaultColor;

    }

    void OnDestroy() {
        if (toggle != null)
            toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
