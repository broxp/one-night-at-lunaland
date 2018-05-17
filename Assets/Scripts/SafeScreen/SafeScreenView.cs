using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SafeScreenView : MonoBehaviour
{
    public SkeletonDataAsset frontSkeletonData, backSkeletonData;
    public Vector2 input;
    public float walkAnimationSpeed;
    //public TransitionDictionaryExample transitions;
    public AnimationReferenceAsset walkFront, walkBack, idle;

    SkeletonAnimation skeletonAnimation;
    AnimationReferenceAsset targetAnimation;
    AnimationReferenceAsset previousTargetAnimation;

    void Start()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    private void Update()
    {
        // Animation
        // Determine target animation.

        if (input.magnitude == 0)
        {
            targetAnimation = idle;
            skeletonAnimation.timeScale = 1;
            skeletonAnimation.skeletonDataAsset = frontSkeletonData;
        }
        else if(input.y > 0)
        {
            targetAnimation = walkBack;
            skeletonAnimation.timeScale = walkAnimationSpeed;
            skeletonAnimation.skeletonDataAsset = backSkeletonData;
            //skeletonAnimation.skeleton.up
              
        }
        else
        {
            targetAnimation = walkFront;
            skeletonAnimation.timeScale = walkAnimationSpeed;
            skeletonAnimation.skeletonDataAsset = frontSkeletonData;
        }



        // Handle change in target animation.
        if (previousTargetAnimation != targetAnimation)
        {
            skeletonAnimation.ClearState();
            skeletonAnimation.Initialize(true);
            Spine.Animation transition = null;
            //if (transitions != null && previousTargetAnimation != null)
            //{
            //transition = transitions.GetTransition(previousTargetAnimation, targetAnimation);
            //}

            if (transition != null)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, transition, false).MixDuration = 0.05f;
                skeletonAnimation.AnimationState.AddAnimation(0, targetAnimation, true, 0f);
            }
            else
            {
                skeletonAnimation.AnimationState.SetAnimation(0, targetAnimation, true);
            }
        }
        previousTargetAnimation = targetAnimation;

        // Face intended direction.
        if (input.x != 0)
        {
            skeletonAnimation.Skeleton.FlipX = input.x > 0;
        }
    }
}