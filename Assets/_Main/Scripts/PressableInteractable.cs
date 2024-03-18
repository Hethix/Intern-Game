using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

// TUTORIAL FROM https://www.youtube.com/watch?v=pmRwhE2hQ9g PART 1 AND 2! 

public class PressableInteractable : XRBaseInteractable
{
    [SerializeField] bool isActive;
    [SerializeField] bool stayDownOnPress = false;
    [SerializeField] [Range(0.0001f, 1)] float minHeightPushed = 0.5f;
    [SerializeField] UnityEvent onPress = new UnityEvent();
    [SerializeField] UnityEvent onCancelStayDown = new UnityEvent();
    [SerializeField] GameObject pressableInteractableIcon;

    bool stayDown = false;
    float yMin = 0.0f;
    float yMax = 0.0f;
    bool previousPress = false;
    float previousHandHeight = 0.0f;
    XRBaseInteractor hoverInteractor = null;

    Collider collider = null;

    public UnityEvent OnPress { get => onPress; }
    public bool StayDown { get => stayDown; set => stayDown = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    public UnityEvent OnCancelStayDown { get => onCancelStayDown; set => onCancelStayDown = value; }
    public float YMin { get => yMin; set => yMin = value; }
    public GameObject PressableInteractableIcon { get => pressableInteractableIcon; }

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<Collider>();
        if (!collider)
            Debug.LogError("PressableInteractable  " + name + " has no collider ", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }

    void StartPress(XRBaseInteractor interactor)
    {
        if (!stayDown && isActive)
        {
            hoverInteractor = interactor;
            previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
        }
    }

    void StartPress(BaseInteractionEventArgs hover)
    {
        gameObject.SetActive(false);
    }
    void EndPress(XRBaseInteractor interactor)
    {
        if (!stayDown && isActive)
        {
            hoverInteractor = null;
            previousHandHeight = 0.0f;

            previousPress = false;
            SetYPosition(yMax);
        }
    }

    void Start()
    {
        SetMinMax();
    }

    void SetMinMax()
    {
        yMin = transform.localPosition.y - (collider.bounds.size.y * minHeightPushed);
        yMax = transform.localPosition.y;
    }
    
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (hoverInteractor && !stayDown)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }
    
    float GetLocalYPosition(Vector3 position)
    {
        Vector3 localposition = transform.root.InverseTransformPoint(position);

        return localposition.y;
    }

    public void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        Vector3 dir = newPosition - transform.localPosition;
        dir = transform.localRotation * dir;
        transform.localPosition = transform.localPosition + dir;
    }

    void CheckPress()
    {
        bool inPosition = InPosition();

        if (inPosition && inPosition != previousPress)
        {
            onPress.Invoke();
            if (stayDownOnPress)
            {
                hoverInteractor = null;
                stayDown = true;
            }
        }

        previousPress = inPosition;
    }

    bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.001f);
        return transform.localPosition.y == inRange;
    }

    public void CancelStayDown()
    {
        onCancelStayDown.Invoke();
        StayDown = false;
        SetYPosition(yMax);
    }
}
