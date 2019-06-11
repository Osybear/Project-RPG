using UnityEngine;

internal interface IFollowTarget
{
    void SetTransformOffset(Transform gameObject, Transform target);
    void SetTransformPosition(Transform gameObject, Transform target);
}
