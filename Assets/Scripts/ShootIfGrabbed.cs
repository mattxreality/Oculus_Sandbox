using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootIfGrabbed : MonoBehaviour
{
    private SimpleShoot simpleShoot;
    private OVRGrabbable ovrGrabbable;
    public OVRInput.Button shootingButton;
    public int maxNumbeOfBullets = 10;
    public Text bulletText;
    public AudioClip shootingAudio;

    // Start is called before the first frame update
    void Start()
    {
        simpleShoot = GetComponent<SimpleShoot>();
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if object is grabbed and if touch controller button is pressed
        if(ovrGrabbable.isGrabbed && OVRInput.GetDown(shootingButton, ovrGrabbable.grabbedBy.GetController()))
        {
            // make controller vibrate using an audio file
            //VibrationManager.singleton.TriggerVibration(shootingAudio,ovrGrabbable.grabbedBy.GetController());

            // make controller vibrate using manual frequency parameters
            VibrationManager.singleton.TriggerVibrationManual(40,2,255, ovrGrabbable.grabbedBy.GetController());

            // play audio file when shooting
            GetComponent<AudioSource>().PlayOneShot(shootingAudio);

            simpleShoot.TriggerShoot();
            maxNumbeOfBullets--;
            bulletText.text = maxNumbeOfBullets.ToString();
        }
    }
}
