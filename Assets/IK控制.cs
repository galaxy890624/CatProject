using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK控制 : MonoBehaviour
{
    /// <summary>要看的物件</summary>
    [SerializeField] Transform lookObj = null;

    //[SerializeField] Transform 手電筒 = null;

    [SerializeField] Animator 動畫控制器 = null;
    // 動畫可以被干涉的瞬間
    private void OnAnimatorIK(int layerIndex)
    {
        動畫控制器.SetLookAtPosition(lookObj.position);
        動畫控制器.SetLookAtWeight(1f, 0.3f, 1f);

        // 設定手的IK位置
        //動畫控制器.SetIKPosition(AvatarIKGoal.RightHand, 手電筒.position);
        //動畫控制器.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);

        // 設定手的IK旋轉值
        //動畫控制器.SetIKRotation(AvatarIKGoal.RightHand, 手電筒.rotation);
        //動畫控制器.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
    }
}
