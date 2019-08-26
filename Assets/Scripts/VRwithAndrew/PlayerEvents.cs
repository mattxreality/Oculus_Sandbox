using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* The purpose of this script is to monitor player inputs and send out
 * events when player input is detected.
 * 
 * Also tracks when input is changing between controller L/R or headset.
 * 
 * 
 * 
 * 
 */

public class PlayerEvents : MonoBehaviour
{

    #region Events
    public static UnityAction OnTouchPadUp = null;
    public static UnityAction OnTouchPadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null; // GameObject is where our line renderer is coming from. L,R, or Headset.
    #endregion

    #region Anchors
    public GameObject m_LeftHandAnchor;
    public GameObject m_RightHandAnchor;
    public GameObject m_HeadAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None; // which controller was used last
    private OVRInput.Controller m_Controller = OVRInput.Controller.None; // checks if controller L/R are connected
    private bool m_InputActive = true; // is input active or not
    #endregion

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound; // true when player is wearing headset
        OVRManager.HMDUnmounted += PlayerLost; // true when player NOT wearing headset

        m_ControllerSets = CreateControllerSets(); // A dictionary with a list of all active controllers
    }

    // OnDestroy should be part of every script with Events
    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    private void Update()
    {
        // Check for active input
        if (!m_InputActive)
            return;

        // Check if a controller exists
        CheckForController();

        // Check for input source
        CheckInputSource();

        // Check for actual input
        Input();

    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;

        // Left remote
        if(OVRInput.IsControllerConnected(OVRInput.Controller.LTouch))
        { controllerCheck = OVRInput.Controller.LTouch; }

        // Right remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
        { controllerCheck = OVRInput.Controller.RTouch; }

        // If no controllers, headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.LTouch)&& 
            !OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
        { controllerCheck = OVRInput.Controller.Touchpad; }

        // Update
        m_Controller = UpdateSource(controllerCheck, m_Controller);
    }

    private void CheckInputSource()
    {
        // Get active controller, compare with previously known active controller. 
        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);


        //OLD CODE. Tutorial "VR with Andrew" replaced this with the above single line of code.
        #region OLD CODE
        //// Left remote, Oculus Touch
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTouch))
        //{
        //    Debug.Log("Left Input");
        //    Debug.Log("Controller Check " + m_Controller);
        //}

        //// Right remote
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTouch))
        //{
        //    Debug.Log("Right Input");
        //    Debug.Log("Controller Check " + m_Controller);
        //}
        //// Headset
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Touchpad))
        //{
        //    Debug.Log("Headset Input");
        //    Debug.Log("Controller Check " + m_Controller);
        //}
        #endregion


    }

    private void Input()
    {
        // Touchpad down
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if(OnTouchPadDown != null)
            { OnTouchPadDown(); } // send event
        }

        // Touchpad up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if(OnTouchPadUp != null)
            { OnTouchPadUp(); } // send event
        }
    }

    // Pass in current controller and keeps track of previous controller. 
    // If current/previous are different, send out event to update controller source.
    // This regulates updates on controller changes. 
    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        // If values are the same, return
        if (check == previous)
        { return previous; }

        // Get controller object
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);

        // If no controller object, set to the head
        if (controllerObject == null)
        { controllerObject = m_HeadAnchor; }

        // Send out event
        if(OnControllerSource != null)
        { OnControllerSource(check, controllerObject); }

        // Return new value
        return check;
    }

    private void PlayerFound()
    {
        m_InputActive = true;
    }

    private void PlayerLost()
    {
        m_InputActive = false;
    }

    // Creates the dictionary list of all active controllers
    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTouch, m_LeftHandAnchor },
            {OVRInput.Controller.RTouch, m_RightHandAnchor },
            {OVRInput.Controller.Touchpad, m_HeadAnchor }
        };

        return newSets;
    }
}
