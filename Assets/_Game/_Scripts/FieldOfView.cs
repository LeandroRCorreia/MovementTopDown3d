using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float viewDistance = 4;
    [SerializeField] private float angleDegress = 50;

    private Transform currentTarget;

    public bool IsInsideFieldOfView(Transform target)
    {
        if(target == null) return false;
        
        currentTarget = target;

        var toTarget = target.position - transform.position;
        toTarget.y = 0; // Define o campo y como zero para projetar apenas nos eixos XZ

        var forward = transform.forward;
        forward.y = 0; // Define o campo y como zero para projetar apenas nos eixos XZ

        var dot = Vector3.Dot(toTarget, forward);
        if (dot < 0) return false;

        var toTargetMag = toTarget.sqrMagnitude;
        var quadraticViewDistance = viewDistance * viewDistance;

        if (toTargetMag > quadraticViewDistance) return false;

        var cos = dot / Mathf.Sqrt(toTargetMag * forward.sqrMagnitude);
        var angleBetweenEnemyToTarget = Mathf.Acos(cos) * Mathf.Rad2Deg;

        return angleBetweenEnemyToTarget < angleDegress * 0.5f;
    }

    private void OnDrawGizmos()
    {
        DrawFOV();
    }

    private void DrawFOV()
    {
        var leftDir = Quaternion.Euler(0, angleDegress * 0.5f, 0) * transform.forward;
        var rightDir = Quaternion.Euler(0, -angleDegress * 0.5f, 0) * transform.forward;

        Gizmos.color = IsInsideFieldOfView(currentTarget) ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, leftDir.normalized * viewDistance);
        Gizmos.DrawRay(transform.position, rightDir.normalized * viewDistance);
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

}
