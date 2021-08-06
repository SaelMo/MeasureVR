using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
 

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 1;
    public XRNode inputSource;
    private Vector2 inputAxis;
    public float additionalHeight = 0.2f;
    private XRRig rig;
    private CharacterController character;
    public bool fly;
    public float gravity = -9.81f;
    private float fallingSpeed;
    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        
        

    }


    public void ToggleFly()
    {
        if (fly)
        {
            fly = false;
        }
        else
        {
            fly = true;
        }
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        if (fly==false)
        {
            Quaternion rigYaw = Quaternion.Euler(0, rig.transform.eulerAngles.y, 0);
            UnityEngine.XR.InputDevice handR = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            handR.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rotL);
            Vector3 direction = (rigYaw * rotL) * new Vector3(inputAxis.x, 0, inputAxis.y);
            character.Move(direction * Time.fixedDeltaTime * speed);
        }
        else
        {
            Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw *new Vector3(inputAxis.x, 0, inputAxis.y);
            character.Move(direction * Time.fixedDeltaTime * speed);

            bool isGrounded = CheckIfGrounded();
            if (isGrounded)
                fallingSpeed = 0;
            else
                fallingSpeed += gravity*Time.fixedDeltaTime;
            character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }
        


    }

    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
        
    }


}
