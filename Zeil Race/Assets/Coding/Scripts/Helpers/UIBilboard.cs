using UnityEngine;

public class UIBilboard : MonoBehaviour
{
    private Transform _cameraTransform;
    private Quaternion _originalRotation;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = _cameraTransform.rotation * _originalRotation;
    }
}
