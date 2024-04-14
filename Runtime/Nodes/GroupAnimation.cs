using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;


public class GroupAnimationPart : AnimationXVII
{
    public GroupAnimationPart(List<AnimationXVII> animations)
    {
        if (isSubAnimation)
        {
            Debug.LogError("SubAnimations can't contains subanimations");
            return;
        }

        subAnimations.AddRange(animations);
        for (int i = 0; i < subAnimations.Count; i++)
        {
            duration = math.max(duration, TotalDuration);
            subAnimations[i].isSubAnimation = true;
        }
    }

    protected override void OnEnd()
    {
        //handled in subanim
    }

    protected override void OnStart()
    {
        //handled in subanim
    }

    protected override void OnUpdateAnimation(float timeRatio)
    {
        //handled in subanim
    }
}