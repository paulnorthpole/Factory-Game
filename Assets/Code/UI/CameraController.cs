using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Pan")]
    public bool relativeBorderSize = true;
    public float panBorderSize = 0.1f;
    public float panSmoothTime = 0.15f;
    public float panSpeed = 0.02f;
    public float keyboardPanSpeedMultiplier = 0.01f;
    public Vector3 minBounds;
    public Vector3 maxBounds;
    [Header("Zoom")]
    public string scrollAxis = "Mouse ScrollWheel";
    public float minSize = 3f;
    public float maxSize = 30f;
    public float zoomSmoothTime = 0.2f;
    public float zoomSpeed = 10f;
    [Header("Rotate")]
    public int rotateMouseButton = 2;
    public float rotateSpeed = 1f;
    public float rotateMinChange = 0.2f;
    public float rotateSmoothTime = 0.2f;
    private Vector3 currentPanSpeed = Vector3.zero;
    private Vector3 currentPanVelocity = Vector3.zero;
    private float targetZoomValue = 0f;
    private float currentZoomVelocity = 0f;
    private Vector3 rotateStartMousePosition = Vector3.zero;
    private float currentRotationSpeed = 0f;
    private float currentRotationVelocity = 0f;
    void Start ()
    {
        targetZoomValue = Camera.main.orthographicSize;
    }
    void Update ()
    {
        if (Input.GetMouseButtonDown(rotateMouseButton)) {
            rotateStartMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(rotateMouseButton)) {
            rotateStartMousePosition = Vector3.zero;
        }
        // pan
        var cameraPosition = Camera.main.transform.position;
        Vector3 targetSpeed = Vector3.zero;
        if (rotateStartMousePosition == Vector3.zero
            && !EventSystem.current.IsPointerOverGameObject()) {
            //Vector3 mouse = Input.mousePosition;
            //float borderSize = relativeBorderSize ? Mathf.Min(Screen.width, Screen.height) * panBorderSize : panBorderSize;
            //if (mouse.x > 0 && mouse.x < borderSize) {
            //    targetSpeed.x -= panSpeed;
            //} else if (mouse.x < Screen.width && mouse.x > Screen.width - borderSize) {
            //    targetSpeed.x += panSpeed;
            //}
            //if (mouse.y > 0 && mouse.y < borderSize) {
            //    targetSpeed.y -= panSpeed;
            //} else if (mouse.y < Screen.height && mouse.y > Screen.height - borderSize) {
            //    targetSpeed.y += panSpeed;
            //}
        }
        float horizAxis = Input.GetAxis("Horizontal");
        float vertiAxis = Input.GetAxis("Vertical");
        targetSpeed += new Vector3(horizAxis + vertiAxis,  vertiAxis - horizAxis, 0) * keyboardPanSpeedMultiplier;

        
        currentPanSpeed = Vector3.SmoothDamp(currentPanSpeed, targetSpeed, ref currentPanVelocity, panSmoothTime);
        var delta = currentPanSpeed * Camera.main.orthographicSize + Camera.main.transform.localPosition;
        Camera.main.transform.localPosition 
            = new Vector3(Mathf.Min(maxBounds.x, Mathf.Max(minBounds.x, delta.x)),
            Mathf.Min(maxBounds.y, Mathf.Max(minBounds.y, delta.y)),
            Mathf.Min(maxBounds.z, Mathf.Max(minBounds.z, delta.z)));

        // zoom
        targetZoomValue += -zoomSpeed * Input.GetAxis(scrollAxis);
        if (targetZoomValue < minSize) {
            targetZoomValue = minSize;
        } else if (targetZoomValue > maxSize) {
            targetZoomValue = maxSize;
        }
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetZoomValue, ref currentZoomVelocity, zoomSmoothTime);
        // rotate
        if (rotateStartMousePosition != Vector3.zero) {
            var dx = (Input.mousePosition.x - rotateStartMousePosition.x) / (Screen.width * 0.5f);
            if (Mathf.Abs(dx) < rotateMinChange) {
                dx = 0f;
            }
            currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, dx * rotateSpeed, ref currentRotationVelocity, rotateSmoothTime);
        } else {
            currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, 0f, ref currentRotationVelocity, rotateSmoothTime);
        }
        Camera.main.transform.Rotate(Vector3.up, currentRotationSpeed, Space.World);
    }
}
