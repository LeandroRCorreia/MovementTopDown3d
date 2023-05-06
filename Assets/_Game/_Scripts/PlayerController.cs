using UnityEngine;



[RequireComponent(typeof(CharacterMovement3D))]
public class PlayerController : MonoBehaviour
{
    CharacterMovement3D characterMovement;


    void Start()
    {
        characterMovement = GetComponent<CharacterMovement3D>();

    }

    void Update()
    {
        


        characterMovement.InputMovement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
