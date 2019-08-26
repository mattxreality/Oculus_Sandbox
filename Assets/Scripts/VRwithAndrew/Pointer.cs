using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    public float m_Distance = 10.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0; // Layer mask to test against interactible things
    public LayerMask m_InteractableMask = 0; // Layer mask to test against everything. Create "interactable" layer.
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null; // event that Reticule will subscribe to


    private Transform m_CurrentOrigin = null; // pulled from PlayerEvents.cs event
    private GameObject m_CurrentObject = null; // Object being pointed at.

    private void Awake()
    {
        PlayerEvents.OnControllerSource += UpdateOrigin;
        PlayerEvents.OnTouchPadDown += ProcessTouchpadDown;
    }

    private void Start()
    {
        SetLineColor();
    }

    private void OnDestroy()
    {
        PlayerEvents.OnControllerSource -= UpdateOrigin;
        PlayerEvents.OnTouchPadDown -= ProcessTouchpadDown;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine(); //updates Vector3 location of endpoint of the line.

        m_CurrentObject = UpdatePointerStatus(); // returns GO of whatever we're pointing at.

        if (OnPointerUpdate != null)
        {
            OnPointerUpdate(hitPoint, m_CurrentObject);
        }
    }

    private Vector3 UpdateLine()
    {
        // Create ray
        RaycastHit hit = CreateRaycast(m_EverythingMask); // tests for any colliders in the scene

        // Set default end of the ray. So line doesn't go on forever.
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);

        // Check hit
        if(hit.collider != null)
        {
            endPosition = hit.point;
        }

        // Set position
        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    private void UpdateOrigin(OVRInput.Controller Controller, GameObject controllerObject)
    {
        // Set origin of pointer
        m_CurrentOrigin = controllerObject.transform;

        // Is the laser visible?
        if(Controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else
        {
            m_LineRenderer.enabled = true;
        }

    }

    private GameObject UpdatePointerStatus()
    {
        // Create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);

        // Check hit
        if(hit.collider)
        { return hit.collider.gameObject; }

        // Return
        return null;
    }


    // Pass in whatever layer we want to use, gathers distance info
    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit;
    }

    private void SetLineColor()
    {
        if (!m_LineRenderer) { return; }

        Color endColor = Color.white;
        endColor.a = 0.0f; // sets alpha of color to zero

        m_LineRenderer.endColor = endColor;
    }

    private void ProcessTouchpadDown()
    {
        if(!m_CurrentObject)
        { return; }

        Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
        interactable.Pressed();

    }
}
