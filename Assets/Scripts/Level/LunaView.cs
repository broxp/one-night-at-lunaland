using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class LunaView : MonoBehaviour
{
    public AnimationReferenceAsset walk;
    public AnimationReferenceAsset idle;

    TeddyView teddyView;
    GameObject teddy;
    SkeletonAnimation skeletonAnimation;

    AnimationReferenceAsset targetAnimation;
    AnimationReferenceAsset previousTargetAnimation;

    void Start()
    {
        teddy = GameObject.FindGameObjectWithTag("Player");
        teddyView = teddy.GetComponent<TeddyView>();
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    private void Update()
    {
        // Animation
        // Determine target animation.

        Vector2 _temp = new Vector2(teddy.transform.position.x, gameObject.transform.position.y);
        gameObject.transform.position = _temp;

        if (teddyView.input.x == 0)
        {
            targetAnimation = idle;
        }
        else
        {
            targetAnimation = walk;
        }



        // Handle change in target animation.
        if (previousTargetAnimation != targetAnimation)
        {
            Spine.Animation transition = null;

                skeletonAnimation.AnimationState.SetAnimation(0, targetAnimation, true);
           
        }
        previousTargetAnimation = targetAnimation;

        // Face intended direction.
        if (teddyView.input.x != 0)
        {
            skeletonAnimation.Skeleton.FlipX = teddyView.input.x > 0;
        }
    }
}

