using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{
    public bool mIsGround;
    public float mCheckRaduis = 0.2f;
    public LayerMask mGroundLayer;
    public Vector2 mBottomOffset;

    void Start()
    {
        // this is
    }

    //
    void Update()
    {
        Check();
    }

    void Check()
    {
        mIsGround = Physics2D.OverlapCircle((Vector2)transform.position-mBottomOffset,
                                            mCheckRaduis, mGroundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position-mBottomOffset, mCheckRaduis);
    }

}
