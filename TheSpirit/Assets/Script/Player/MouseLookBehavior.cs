using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookBehavior : MonoBehaviour
{
    enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    [Header("Degree")]
    [SerializeField] RotationAxes _axes = RotationAxes.MouseXandY;

    [Header("Sensitivity")]
    [SerializeField] float _sensitivityHor = 9.0f;
    [SerializeField] float _sensitivityVert = 9.0f;

    [Header("Constraints")]
    [SerializeField] float _minVert = -45.0f;
    [SerializeField] float _maxVert = 45.0f;

    private float _verticalRot = 0;

    private bool _isPaused = false; // Add this variable to track pause state

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.freezeRotation = true;
    }

    // This method will be called to pause or resume mouse look
    public void SetPauseState(bool isPaused)
    {
        _isPaused = isPaused;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPaused)
            return; // If the game is paused, stop mouse look behavior

        switch (_axes)
        {
            case RotationAxes.MouseX:
                // Rotate upon the Y axis (Yaw)
                transform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivityHor, 0);
                break;

            case RotationAxes.MouseY:
                // Set Pitch about Y axis
                _verticalRot -= Input.GetAxis("Mouse Y") * _sensitivityVert;
                _verticalRot = Mathf.Clamp(_verticalRot, _minVert, _maxVert);

                float horizontalRot = transform.localEulerAngles.y;

                // Update rotation
                transform.localEulerAngles = new Vector3(_verticalRot, horizontalRot, 0);
                break;

            case RotationAxes.MouseXandY:
                // Set Pitch about Y axis
                _verticalRot -= Input.GetAxis("Mouse Y") * _sensitivityVert;
                _verticalRot = Mathf.Clamp(_verticalRot, _minVert, _maxVert);

                float deltaX = Input.GetAxis("Mouse X") * _sensitivityHor;
                horizontalRot = transform.localEulerAngles.y + deltaX;

                // Update rotation
                transform.localEulerAngles = new Vector3(_verticalRot, horizontalRot, 0);
                break;

            default:
                break;
        }
    }
}
