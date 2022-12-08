using UnityEngine;

public class UIBilboard : MonoBehaviour
{
    private Transform _cameraTransform;
    private Quaternion _originalRotation;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _originalRotation = transform.localRotation;
    }

    private void Update()
    {
        transform.localRotation = _cameraTransform.localRotation * _originalRotation;
    }
}
