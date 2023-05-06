using UnityEngine;


[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform following;

    public Vector3 FollowingPosition => following.position;


    [SerializeField] private Vector3 distanceFollowing;

    private void LateUpdate() 
    {
        var arm = FollowingPosition + distanceFollowing;
        
        transform.position = arm;

    }


}
