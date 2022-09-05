using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // target
    [SerializeField]
    private Transform target;

    // camera distance
    [SerializeField]
    public float dist = 5f;

    // camera rotate speed
    private float xSpeed = 220.0f;
    private float ySpeed = 100.0f;

    // camera first position
    private float x = 0.0f;
    private float y = 0.0f;

    // Limit
    private float yMinLimit = -20f;
    private float yMaxLimit = 80f;

    private Transform cam;

    float ClampAngle(float angle, float min, float max)
    {

        if(angle < -360)
        {
            angle += 360;
        }
        if(angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }

    private void Start()
    {
        cam = GetComponent<Transform>();

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void LateUpdate()
    {
        if(target)
        {
            dist -= .5f * Input.mouseScrollDelta.y;

            if(dist < 0.5)
            {
                dist = 1;
            }

            if(dist >= 10)
            {
                dist = 10;
            }

            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 0.0f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);

            this.transform.rotation = rotation;
            this.transform.position = position;
        }
    }
}
