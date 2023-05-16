
using UnityEngine;
using Cinemachine;

public class CameraPlayer : MonoBehaviour
{
    private CinemachineTargetGroup cinemachineTargetGroup;
    private GameObject jugador;

    private float cameraWeight = 1; // Peso/Weight de la cámara
    private float cameraRadius = 0; // Radio de la cámara

    private void Start(){
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        // transform del obj, peso/Weight, radio
        cinemachineTargetGroup.AddMember(jugador.transform, cameraWeight, cameraRadius);
    }

}
