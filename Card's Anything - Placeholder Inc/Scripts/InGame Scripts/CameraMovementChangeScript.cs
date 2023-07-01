using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovementChangeScript : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera outTableCamera, handViewCamera, boardViewCamera;

    int cameraIndex;

    [SerializeField]
    GameObject hand;


    void Update()
    {
        if (!GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>()._gameEnded)
        {
            CameraChange();
        }
    }

    public void CameraChange()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.mouseScrollDelta.y * 0.1f > 0)
        {
            if (cameraIndex == 0)
            {
                cameraIndex = 1;

                hand.SetActive(false);

                boardViewCamera.Priority = 100;
                handViewCamera.Priority = 1;
                outTableCamera.Priority = 1;
            }
            else if (cameraIndex == -1)
            {
                cameraIndex = 0;

                boardViewCamera.Priority = 1;
                handViewCamera.Priority = 100;
                outTableCamera.Priority = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.mouseScrollDelta.y * 0.1f < 0)
        {
            if (cameraIndex == 0)
            {
                cameraIndex = -1;

                boardViewCamera.Priority = 1;
                handViewCamera.Priority = 1;
                outTableCamera.Priority = 100;
            }
            else if (cameraIndex == 1)
            {
                cameraIndex = 0;

                hand.SetActive(true);

                boardViewCamera.Priority = 1;
                handViewCamera.Priority = 100;
                outTableCamera.Priority = 1;
            }
        }
    }
}
