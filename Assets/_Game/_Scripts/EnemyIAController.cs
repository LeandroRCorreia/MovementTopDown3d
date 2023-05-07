using UnityEngine;

[RequireComponent(typeof(CharacterMovement3D))]
[RequireComponent(typeof(FieldOfView))]
public class EnemyIAController : MonoBehaviour
{
    private FieldOfView fieldOfView;
    
    private CharacterMovement3D CharacterMovement;

    [Header("Target")]

    [Space]
    
    [SerializeField] private Transform target;
    private bool canSeeTarget = false;

    private void Awake()
    {
        fieldOfView = GetComponent<FieldOfView>();    
        CharacterMovement = GetComponent<CharacterMovement3D>();
    }

    private void Update()
    {
        canSeeTarget = fieldOfView.IsInsideFieldOfView(target);

        if(canSeeTarget)
        {
            FollowTarget();
        }
        else
        {
            CharacterMovement.InputMovement(0, 0);
        }

    }

    private void FollowTarget()
    {
        var dirInput = (target.position - transform.position).normalized;
        CharacterMovement.InputMovement(dirInput.x, dirInput.z);
    }


}
