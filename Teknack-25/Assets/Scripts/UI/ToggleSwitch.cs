// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;

//     public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
//     {
//         [Header("Slider setup")] 
//         [SerializeField, Range(0, 1f)]
//         protected float sliderValue;
//         public bool CurrentValue { get; private set; }
        
//         private bool _previousValue;
//         private Slider _slider;

//         [Header("Animation")] 
//         [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
//         [SerializeField] private AnimationCurve slideEase =
//             AnimationCurve.EaseInOut(0, 0, 1, 1);

//         private Coroutine _animateSliderCoroutine;

//         [Header("Events")] 
//         [SerializeField] private UnityEvent onToggleOn;
//         [SerializeField] private UnityEvent onToggleOff;

//         private ToggleSwitchGroupManager _toggleSwitchGroupManager;
        
//         protected Action transitionEffect;
        
//         protected virtual void OnValidate()
//         {
//             SetupToggleComponents();

//             _slider.value = sliderValue;
//         }

//         private void SetupToggleComponents()
//         {
//             if (_slider != null)
//                 return;

//             SetupSliderComponent();
//         }

//         private void SetupSliderComponent()
//         {
//             _slider = GetComponent<Slider>();

//             if (_slider == null)
//             {
//                 Debug.Log("No slider found!", this);
//                 return;
//             }

//             _slider.interactable = false;
//             var sliderColors = _slider.colors;
//             sliderColors.disabledColor = Color.white;
//             _slider.colors = sliderColors;
//             _slider.transition = Selectable.Transition.None;
//         }
        
//         public void SetupForManager(ToggleSwitchGroupManager manager)
//         {
//             _toggleSwitchGroupManager = manager;
//         }


//         protected virtual void Awake()
//         {
//             SetupSliderComponent();
//         }

//         public void OnPointerClick(PointerEventData eventData)
//         {
//             Toggle();
//         }

        
//         private void Toggle()
//         {
//             if (_toggleSwitchGroupManager != null)
//                 _toggleSwitchGroupManager.ToggleGroup(this);
//             else
//                 SetStateAndStartAnimation(!CurrentValue);
//         }

//         public void ToggleByGroupManager(bool valueToSetTo)
//         {
//             SetStateAndStartAnimation(valueToSetTo);
//         }
        
        
//        private void SetStateAndStartAnimation(bool state)
// {
//     if (CurrentValue == state) return; // Prevent duplicate toggles

//     _previousValue = CurrentValue;  // Store old state
//     CurrentValue = state;           // Change state

//     Debug.Log($"🟢 Toggle State Changed: {CurrentValue}"); // Debug log

//     if (CurrentValue)
//         onToggleOn?.Invoke();
//     else
//         onToggleOff?.Invoke();

//     if (_animateSliderCoroutine != null)
//         StopCoroutine(_animateSliderCoroutine);

//     _animateSliderCoroutine = StartCoroutine(AnimateSlider());
// }




//        private IEnumerator AnimateSlider()
// {
//     float startValue = _slider.value;
//     float endValue = CurrentValue ? 1 : 0;
//     float time = 0;

//     if (animationDuration > 0)
//     {
//         while (time < animationDuration)
//         {
//             time += Time.deltaTime;

//             float lerpFactor = slideEase.Evaluate(time / animationDuration);
//             _slider.value = Mathf.Lerp(startValue, endValue, lerpFactor);

//             transitionEffect?.Invoke();

//             yield return null;
//         }
//     }

//     _slider.value = endValue;

//     // ✅ Force UI Refresh
//     _slider.gameObject.SetActive(false);
//     _slider.gameObject.SetActive(true);
// }


//     }
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [Header("Slider setup")] 
    [SerializeField, Range(0, 1f)]
    protected float sliderValue;
    public bool CurrentValue { get; private set; }
    
    private bool _previousValue;
    private Slider _slider;

    [Header("Animation")] 
    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField] private AnimationCurve slideEase =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine _animateSliderCoroutine;

    [Header("Events")] 
    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;

    private ToggleSwitchGroupManager _toggleSwitchGroupManager;
    
    protected Action transitionEffect;
    
    protected virtual void OnValidate()
    {
        SetupToggleComponents();
        _slider.value = sliderValue;
    }

    private void SetupToggleComponents()
    {
        if (_slider != null)
            return;

        SetupSliderComponent();
    }

    private void SetupSliderComponent()
    {
        _slider = GetComponent<Slider>();

        if (_slider == null)
        {
            Debug.Log("No slider found!", this);
            return;
        }

        _slider.interactable = false;
        var sliderColors = _slider.colors;
        sliderColors.disabledColor = Color.white;
        _slider.colors = sliderColors;
        _slider.transition = Selectable.Transition.None;
    }
    
    public void SetupForManager(ToggleSwitchGroupManager manager)
    {
        _toggleSwitchGroupManager = manager;
    }

    protected virtual void Awake()
    {
        SetupSliderComponent();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void Toggle()
    {
        if (_toggleSwitchGroupManager != null)
            _toggleSwitchGroupManager.ToggleGroup(this);
        else
            SetStateAndStartAnimation(!CurrentValue);
    }

    public void ToggleByGroupManager(bool valueToSetTo)
    {
        SetStateAndStartAnimation(valueToSetTo);
    }

    private void SetStateAndStartAnimation(bool state)
    {
        if (CurrentValue == state) return; // Prevent duplicate toggles

        _previousValue = CurrentValue;
        CurrentValue = state;

        Debug.Log($"🟢 Toggle State Changed: {CurrentValue}"); 

        if (CurrentValue)
            onToggleOn?.Invoke();
        else
            onToggleOff?.Invoke();

        // ✅ Ensure slider is active before coroutine starts
        if (!_slider.gameObject.activeInHierarchy)
        {
            _slider.gameObject.SetActive(true);
            StartCoroutine(WaitForActivationAndAnimate());
        }
        else
        {
            StartAnimation();
        }
    }

    private void StartAnimation()
    {
        if (_animateSliderCoroutine != null)
            StopCoroutine(_animateSliderCoroutine);
        _animateSliderCoroutine = StartCoroutine(AnimateSlider());
    }

    private IEnumerator WaitForActivationAndAnimate()
    {
        yield return new WaitForEndOfFrame(); // ✅ Wait for Unity to process activation
        StartAnimation();
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = _slider.value;
        float endValue = CurrentValue ? 1 : 0;
        float time = 0;

        if (animationDuration > 0)
        {
            while (time < animationDuration)
            {
                time += Time.deltaTime;
                float lerpFactor = slideEase.Evaluate(time / animationDuration);
                _slider.value = Mathf.Lerp(startValue, endValue, lerpFactor);
                transitionEffect?.Invoke();
                yield return null;
            }
        }

        _slider.value = endValue;

        // ✅ Force UI Refresh
        _slider.gameObject.SetActive(false);
        _slider.gameObject.SetActive(true);
    }
}
