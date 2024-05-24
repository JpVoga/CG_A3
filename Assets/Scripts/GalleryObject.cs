using UnityEngine;
using Cinemachine;

public class GalleryObject : MonoBehaviour {
    [SerializeField()] private CinemachineVirtualCameraBase virtualCamera;

    public CinemachineVirtualCameraBase VirtualCamera => virtualCamera;
}