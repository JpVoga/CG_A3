# Galeria interativa de 10 objetos 3D em Unity.

## Autores
 * João Pedro Voga de Oliveira - RA: 1262216592
 * Leonardo Loiola Loureiro - RA: 1262222969

## Visão geral:
 * Trata-se de uma galeria de 10 objetos 3D onde se pode trocar o objeto em foco e mover a câmera, por meio das setas e pelo mouse, respectivamente.

## Classes:
 * Gallery: Representa o objeto pai que contém todos os objetos. Essa classe contém a lista dos objetos e o índice do objeto em foco. Essa classe escuta eventos da classe GameInput para realizar as ações da câmera. Segue o código abaixo:


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


 * GalleryObject: Contém as informações sobre seu respectivo objeto. Especificamente, relaciona o objeto com sua câmera. Segue o código abaixo:


        using UnityEngine;
        using Cinemachine;

        public class GalleryObject : MonoBehaviour {
            [SerializeField()] private CinemachineVirtualCameraBase virtualCamera;

            public CinemachineVirtualCameraBase VirtualCamera => virtualCamera;
        }


 * GameInput: Dispara eventos que vão ser ouvidos por outras classes quando as ações de controle são executadas pelo usuário.


        using System;
        using UnityEngine;
        using UnityEngine.InputSystem;

        public class GameInput: MonoBehaviour {
            private InputActions actions;

            public event Action OnGoRight, OnGoLeft;

            private void Awake() {
                actions = new InputActions();
            }

            private void Start() {
                actions.Enable();
                actions.Default.Enable();

                actions.Default.GoRight.performed += context => OnGoRight?.Invoke();
                actions.Default.GoLeft.performed  += context => OnGoLeft?.Invoke();
            }

            private void OnDestroy() {
                actions.Enable();
                actions.Default.Enable();
                actions.Dispose();
            }

            public InputAction GetRotateCameraAction() {return actions.Default.RotateCamera;}
        }


## Como executar:
 * O executável se encontra na pasta "Build", é o arquivo "CG_A3.exe"

## Todos os Assets do projeto se encontram logo na pasta "Assets" do arquivo .zip, incluindo os códigos na pasta "Assets/Scripts"