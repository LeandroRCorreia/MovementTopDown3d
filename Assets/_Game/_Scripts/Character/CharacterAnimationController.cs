using UnityEngine;

struct MovementAnimationConstantsKeys
{
    public const string Velocity = "Velocity";

}

public class CharacterAnimationController : MonoBehaviour
{   
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterMovement3D characterMovement;

    public int VelocityHash => Animator.StringToHash(MovementAnimationConstantsKeys.Velocity);

    void LateUpdate()
    {
        animator.SetFloat(VelocityHash, characterMovement.Velocity);
    }

}
