using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TeddyView : MonoBehaviour {

    public Vector2 input;
    //public TransitionDictionaryExample transitions;
    public AnimationReferenceAsset walk;
    public AnimationReferenceAsset run;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset fall;

    SkeletonAnimation skeletonAnimation;
    AnimationReferenceAsset targetAnimation;
    AnimationReferenceAsset previousTargetAnimation;

    void Start ()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

    private void Update()
    {
        // Animation
        // Determine target animation.
        
        if (input.x == 0)
        {
            targetAnimation = idle;
        }
        else
        {
            targetAnimation = Mathf.Abs(input.x) > 0.6f ? run : walk;
        }



        // Handle change in target animation.
        if (previousTargetAnimation != targetAnimation)
        {
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
