using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour
{

    private MechActor mech;
    private Transform cameraBoom;
    private Camera mainCamera;
    // Use this for initialization
    void Start()
    {
        mech = GetComponent<MechActor>();
        mainCamera = GetComponentInChildren<Camera>();
        cameraBoom = mainCamera.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

        mech.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        HandleCam();
        mech.AimTowards(FindAim());
    }

    private Vector3 FindAim()
    {
        Ray aimRay = new Ray(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward));
        RaycastHit aimHit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        if (Physics.Raycast(aimRay, out aimHit, 10000, layerMask))
        {
            Debug.DrawLine(mainCamera.transform.position, aimHit.point);
            return aimHit.point;
        }

        Vector3 noHit = mainCamera.transform.position + (10000 * mainCamera.transform.TransformDirection(Vector3.forward));
        Debug.DrawLine(mainCamera.transform.position, noHit);
        return noHit;

    }

    private void HandleCam()
    {
        float sensitivity = 2f;
        float verticalSpeed = Input.GetAxis("Mouse Y") * -1 * sensitivity;
        float horizontalSpeed = Input.GetAxis("Mouse X") * sensitivity;

        Vector3 camAngle = cameraBoom.rotation.eulerAngles;
        cameraBoom.rotation = Quaternion.Euler(camAngle.x + verticalSpeed, camAngle.y + horizontalSpeed, 0);
    }
}
