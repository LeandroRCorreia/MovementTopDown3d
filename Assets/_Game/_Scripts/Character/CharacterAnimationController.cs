using UnityEngine;

struct AnimationConstantsKeys
{
    public const string Velocity = "Velocity";

}

public class CharacterAnimationController : MonoBehaviour
{   
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterMovement3D characterMovement;

    public int VelocityHash => Animator.StringToHash(AnimationConstantsKeys.Velocity);

    void LateUpdate()
    {
        animator.SetFloat(VelocityHash, characterMovement.Velocity);
    }

}
