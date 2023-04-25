using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour{

    // O Alvo a ser seguido
    public Transform target;
    // Dist�ncia do alvo ao longo do seu eixo Z
    private float distance = 10.0f; // private - n�o acess�vel ao player
    public float maxDistance = 10.0f;
    public float minDistance = 4.0f;
    // Altura desejada para que a c�mera fique acima do Alvo
    private float height = 5.0f; // private - n�o acess�vel ao player
    public float minHeight = -1.0f;
    public float maxHeight = 10.0f;
    // Com que rapidez se deseja chegar � posi��o desejada
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    // Sensibilidades do eixo X, eixo Y e no z como dist�ncia
    public Vector3 sensibility;

    // Executa depois dos outros updates do frame, para depois renderizar a c�mera 
    void LateUpdate(){
        if (!target)
            return;

        // Calcula nova dist�ncia
        distance -= Input.GetAxis("Mouse ScrollWheel") * sensibility.z;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calcula nova altura 
        height -= Input.GetAxis("Mouse Y") * sensibility.z; //eixo vertical
        height = Mathf.Clamp(height, minHeight, maxHeight);

        // Calcula os �ngulos de rota��o corrente
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Converte �ngulo em rota��o
        Quaternion currentRotation = Quaternion.Euler(0, transform.eulerAngles.y + Input.GetAxis("Mouse X") * sensibility.x, 0);

        // Setar posi��o da c�mera no plano x-z para: 
        // metros de dist�ncia atr�s do alvo
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Setar altura da c�mera
        transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);

        // Sempre "olhando" para o Alvo
        transform.LookAt(target);
    }
}

