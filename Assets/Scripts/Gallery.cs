using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gallery : MonoBehaviour
{
    [SerializeField()] private List<GalleryObject> galleryObjects;
    [SerializeField()] private GameInput gameInput;

    private Camera mainCamera;
    private CinemachineBrain mainCameraBrain;

    private int _objectIndex = 0;
    public int ObjectIndex {
        get => _objectIndex;
        set {
            int oldObjectIndex = _objectIndex;
            _objectIndex = Mathf.Clamp(value, 0, galleryObjects.Count-1);
            int newObjectIndex = _objectIndex;

            GalleryObject oldObject = galleryObjects[oldObjectIndex];
            GalleryObject newObject = galleryObjects[newObjectIndex];

            oldObject.VirtualCamera.Priority = 0;
            newObject.VirtualCamera.Priority = 1;

            IEnumerator AfterTransitionRoutine() {
                yield return new WaitUntil(() => ((mainCameraBrain.ActiveVirtualCamera == newObject.VirtualCamera) && (mainCameraBrain.ActiveBlend == null)));
                DisableCameraRotation(oldObject.VirtualCamera);
                EnableCameraRotation(newObject.VirtualCamera);

                if (oldObject.VirtualCamera is CinemachineFreeLook oldFreeLook) {
                    oldFreeLook.m_XAxis.Value = 0;
                    oldFreeLook.m_YAxis.Value = 0;
                }
            }

            StartCoroutine(AfterTransitionRoutine());
        }
    }

    private void Awake() {
        mainCamera = Camera.main;
        mainCameraBrain = mainCamera.GetComponent<CinemachineBrain>();
    }

    private void Start() {
        gameInput.OnGoLeft += () => {--ObjectIndex;};
        gameInput.OnGoRight += () => {++ObjectIndex;};

        EnableCameraRotation(galleryObjects[ObjectIndex].VirtualCamera);
    }

    private void EnableCameraRotation(CinemachineVirtualCameraBase virtualCamera) {
        if (virtualCamera.TryGetComponent<CinemachineInputProvider>(out CinemachineInputProvider inputProvider)) {
            inputProvider.XYAxis = InputActionReference.Create(gameInput.GetRotateCameraAction());
        }
    }

    private void DisableCameraRotation(CinemachineVirtualCameraBase virtualCamera) {
        if (virtualCamera.TryGetComponent<CinemachineInputProvider>(out CinemachineInputProvider inputProvider)) {
            inputProvider.XYAxis = null;
        }
    }
}
