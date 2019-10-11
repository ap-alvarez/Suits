using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour
{
    private MechActor mech;
    private HUDController HUD;
    private Transform cameraBoom;
    private Camera mainCamera;

    private float zoom = 1f;
    private float zoomMin = 1f;
    private float zoomMax = 1.6f;
    private float zoomSpeed = 2f;

    private Vector3 currentAim;

    // Use this for initialization
    void Start()
    {
        mech = GetComponent<MechActor>();
        mainCamera = GetComponentInChildren<Camera>();
        cameraBoom = mainCamera.transform.parent;
        HUD = GameObject.FindGameObjectWithTag("PlayerHUD").GetComponent<HUDController>();

    }

    // Update is called once per frame
    void Update()
    {

        mech.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        HandleCam();

        currentAim = PlayerAim();

        if (Input.GetMouseButton(0))
            mech.Attack(AttackType.primary);

        if (Input.GetMouseButton(1))
            ZoomIn();
        else
            ZoomOut();

        HUD.SetAim(mainCamera.WorldToViewportPoint(currentAim));

    }

    /// <summary>
    /// Aims the player
    /// </summary>
    /// <returns>Mech's new aim in world position</returns>
    private Vector3 PlayerAim()
    {
        float maxDist = 5000;
        Vector3 aimDir = mainCamera.transform.TransformDirection(Vector3.forward);
        Ray aimRay = new Ray(mainCamera.transform.position, aimDir);
        Vector3 hitPos = mainCamera.transform.position + (maxDist * aimDir);

        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        if (Physics.Raycast(aimRay, out hit, maxDist, layerMask))
        {
            hitPos = hit.point;
        }

        return mech.MechAim(hitPos);
    }
    /*private Vector3 FindPlayerAim()
    {
        Vector3 aimDir = mainCamera.transform.TransformDirection(Vector3.forward);
        Ray aimRay = new Ray(mainCamera.transform.position, aimDir);

        float minDistance = 8f;
        float maxDistance = 1000f;

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        Vector3 hitPos = mainCamera.transform.position + (maxDistance * aimDir);

        RaycastHit[] aimHits = Physics.RaycastAll(aimRay, maxDistance, layerMask);

        if (aimHits.Length > 0)
        {
            RaycastHit hit = aimHits[0];
            float hitDist = -1;
            for (int i = 0; i < aimHits.Length; i++)
            {
                RaycastHit newHit = aimHits[i];
                float dist = Vector3.Distance(mech.transform.position, newHit.point);
                if (dist < minDistance)
                    continue;
                if (hitDist < 0 || dist < hitDist)
                {
                    hit = newHit;
                    hitDist = dist;
                }
            }

            if (hitDist > 0)
            {
                hitPos = hit.point;
            }
        }

        Debug.DrawLine(mainCamera.transform.position, hitPos);
        return hitPos;
    }*/


    private void HandleCam()
    {
        float sensitivity = 2f;
        float verticalSpeed = Input.GetAxis("Mouse Y") * -1 * sensitivity;
        float horizontalSpeed = Input.GetAxis("Mouse X") * sensitivity;

        Vector3 camAngle = cameraBoom.rotation.eulerAngles;
        cameraBoom.rotation = Quaternion.Euler(camAngle.x + verticalSpeed, camAngle.y + horizontalSpeed, 0);
    }

    private void ZoomIn()
    {
        if (zoom < zoomMax)
            zoom += zoomSpeed * Time.deltaTime;
        if (zoom > zoomMax)
            zoom = zoomMax;

        mainCamera.fieldOfView = 75f / zoom;
        HUD.Zoom(zoom);
    }

    private void ZoomOut()
    {
        if (zoom > zoomMin)
            zoom -= zoomSpeed * Time.deltaTime;
        if (zoom < zoomMin)
            zoom = zoomMin;

        mainCamera.fieldOfView = 75f / zoom;
        HUD.Zoom(zoom);
    }
}
