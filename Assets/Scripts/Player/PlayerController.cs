using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Inputs")]
    public float xInput;
    public float zInput;    
    
    [Header("Camera Inputs")]
    public float xMouseInput;
    public float yMouseInput;
    public float lookSpeed;
    public float xCameraRot;

    [Header("Player Settings")]
    public float speed;
    public CharacterController character;
    public Vector3 movement;

    [Header("Camera Settings")]
    public Transform cameraTransform;

    [Header("Jump Settings")]
    [Range(0, -100)] public float gravity;
    public float jumpForce;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        MovePlayer();
        LookPlayer();
    }

    void  LookPlayer()
    {
        //Rotar la posicion del player cuando el mouse se mueve hacia los costados
        transform.Rotate(0, xMouseInput * lookSpeed * Time.deltaTime, 0);


        //Rotacion de la camara del jugador
        xCameraRot -= yMouseInput * lookSpeed * Time.deltaTime;

        //Se limita el ángulo vertical para evitar que el jugador mire completamente hacia arriba o abajo
        xCameraRot = Math.Clamp(xCameraRot, -80, 80);

        //Aplica la rotación vertical(pitch) a la cámara, no al jugador completo.
        //Euler(...): crea una rotación a partir de ángulos en grados.
        cameraTransform.localRotation = Quaternion.Euler(xCameraRot, 0, 0);

    }
    void MovePlayer()
    {
        /** 
         * transform.right crea un vector horizontal eje X (Linea Roja del transform)
         * Lo multiplico por xInput o sea si es positivo (0.1) va a ir a la derecha y si es negativo hacia la izquierda (-0.1)
         * 
         * transform.forward crea un vector frontal eje Z (Linea azul del transform)
         * Lo multiplico por zInput o sea si es positivo (0.1) va a ir hacia adelante y si es negativo hacia la atras (-0.1)
         * **/
        movement = (transform.right * xInput + transform.forward * zInput) * speed * Time.deltaTime;

        //JUMP LOGIC
        if (Input.GetButtonDown("Jump"))
        {
            movement.y = jumpForce;
        }
        movement.y += gravity;

        //MOVMENT LOGIC
        if (character != null)
        {
            character.Move(movement);
        }
    }

    void GetInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        xMouseInput = Input.GetAxis("Mouse X");
        yMouseInput = Input.GetAxis("Mouse Y");

    }
}
