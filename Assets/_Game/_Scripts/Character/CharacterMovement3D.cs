using UnityEngine;

public class CharacterMovement3D : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 12;
    [SerializeField] private float acelleration = 50;
    [SerializeField] private float rotMultiplierY;
    private Vector3 targetVelocity;
    private Vector3 velocity;

    [Space]

    [Header("Collision")]

    [Space]

    [SerializeField] private Vector3 colliderSize;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsObstacle;
    
    private RaycastHit[] hitVertical = new RaycastHit[5];
    private RaycastHit[] hitHorizontal = new RaycastHit[5];

    private Vector3 ColliderExtents => colliderSize * 0.5f;
    private Vector3 ColliderCenter => transform.position + (Vector3.up * ColliderExtents.y);

    public float Velocity => velocity.magnitude;

    public bool IsMoving => velocity.magnitude > 0;

    public void InputMovement(float horizontal, float vertical)
    {
        Vector3 inputMovement = new Vector3(horizontal, 0, vertical).normalized;
        targetVelocity = inputMovement * moveSpeed;
    }

    private void FixedUpdate()
    {
        velocity = Vector3.MoveTowards(velocity, targetVelocity, acelleration * Time.fixedDeltaTime);

        RotationCharacterY();
        CheckCollisionVertical();
        CheckCollisionHorizontal();

        var targetPosition = transform.position + velocity * Time.fixedDeltaTime;
        transform.position = targetPosition;

    }

    private void RotationCharacterY()
    {
        var velocityXZ = new Vector3(targetVelocity.x, 0, targetVelocity.z).normalized; 

        transform.forward = Vector3.Lerp(transform.forward, velocity,
        rotMultiplierY * Time.fixedDeltaTime);
    }

    private void CheckCollisionVertical()
    {
        var rayLenght = 5;
        Ray ray = new Ray(ColliderCenter, Vector3.down);
        var hitCount = Physics.RaycastNonAlloc(ray,
        hitVertical,
        rayLenght,
        whatIsGround);

        for(int i = 0 ; i < hitCount; i++)
        {
            var hited = hitVertical[i];
            var projected = Vector3.ProjectOnPlane(transform.position - hited.point, hited.normal.normalized);
            var onPlaneY = hited.point.y + projected.y;
            transform.position = new Vector3(transform.position.x, onPlaneY, transform.position.z);
            break;
        }

    }

    private void CheckCollisionHorizontal()
    {
        var rayLenght = velocity.magnitude * Time.fixedDeltaTime;

        var hitCount = Physics.BoxCastNonAlloc(ColliderCenter,
        ColliderExtents,
        velocity.normalized,
        hitHorizontal,
        Quaternion.identity,
        rayLenght,
        whatIsObstacle
        );


        for(int i = 0; i < hitCount; i++)
        {
            var hit = hitHorizontal[i];
            var normal = hit.normal;


            var projected = Vector3.ProjectOnPlane(velocity, normal);
            
            velocity = projected.normalized * velocity.magnitude;
            break;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        GizmosUtils.DrawRay(transform.position, velocity);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ColliderCenter, colliderSize);

    }

}
