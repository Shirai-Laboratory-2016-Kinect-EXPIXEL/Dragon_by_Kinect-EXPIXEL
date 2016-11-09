using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(DragonController))]
public class DragoInput : MonoBehaviour
{
    private DragonController mydrago;
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    private Transform m_Cam;

    void Awake()
    {
        mydrago = GetComponent<DragonController>();
    }

    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }
    }


    void Update()
    {
        GetInput();
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 1, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }

        mydrago.CameraMove(m_Move);

    }

    //----------------------Link the buttons pressed with correspond variables- here you can change the type of input------------------------------------------------------------------------------

    void GetInput()
    {

        mydrago.Horizontal = CrossPlatformInputManager.GetAxis("Horizontal");   //Get the Horizontal Axis
        mydrago.Vertical = CrossPlatformInputManager.GetAxis("Vertical");       //Get the Vertical Axis
        mydrago.Attack1 = CrossPlatformInputManager.GetButton("Fire1");         //Get the Attack1 button
        mydrago.Attack2 = CrossPlatformInputManager.GetButtonDown("Fire2");         //Get the Attack1 button

        if (Input.GetKeyDown(KeyCode.Q)) mydrago.Fly = !mydrago.Fly;         //Toogle the Fly button

        mydrago.Jump = CrossPlatformInputManager.GetButton("Jump");     //Get the Jump button
        mydrago.Shift = Input.GetKey(KeyCode.LeftShift);                //Get the Shift button  
        mydrago.Down = Input.GetKey(KeyCode.C);                         //Get the Down button
        mydrago.Dodge = Input.GetKey(KeyCode.E);                        //Get the Dodge button      

        mydrago.Damage = Input.GetKeyDown(KeyCode.H);                   //Get the Damage button change the variable entry to manipulate how the damage works
        mydrago.Stun = Input.GetMouseButton(2);                         //Get the Stun button change the variable entry to manipulate how the stun works
        mydrago.Death = Input.GetKeyDown(KeyCode.K);                    //Get the Death button change the variable entry to manipulate how the death works

        mydrago.Speed1 = Input.GetKeyDown(KeyCode.Alpha1);              //Walk
        mydrago.Speed2 = Input.GetKeyDown(KeyCode.Alpha2);              //Trot
        mydrago.Speed3 = Input.GetKeyDown(KeyCode.Alpha3);              //Run

    }

  
}
