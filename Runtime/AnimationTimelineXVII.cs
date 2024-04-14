using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationTimelineXVII
{
    public List<AnimationXVII> AnimationParts => animations;
    public AnimationXVII LastAnimation => animations[animations.Count - 1];

    List<AnimationXVII> animations = new List<AnimationXVII>();

    public AnimationTimelineXVII Wait(float seconds)
    {
        animations.Add(new WaitAnimationPart(seconds));
        return this;
    }
    public AnimationTimelineXVII Add(AnimationXVII animation)
    {
        animation.OnCreated();
        animations.Add(animation);
        return this;
    }

    public AnimationTimelineXVII Add(List<AnimationXVII> animations)
    {
        for (int i = 0; i < animations.Count; i++)
        {
            animations[i].OnCreated();
        }
        this.animations.Add(new GroupAnimationPart(animations.ToList()));
        return this;
    }

    public AnimationTimelineXVII Add(params AnimationXVII[] animations)
    {
        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].OnCreated();
        }
        this.animations.Add(new GroupAnimationPart(animations.ToList()));
        return this;
    }

    public AnimationTimelineXVII SetAnimationCurve(AnimationCurve animationCurve)
    {
        LastAnimation.SetAnimationCurve(animationCurve);
        return this;
    }

    public AnimationTimelineXVII SetEaseCurve(EaseXVII.Ease easeCurve)
    {
        LastAnimation.SetEaseCurve(easeCurve);
        return this;
    }

    public AnimationTimelineXVII SetDuration(float duration)
    {
        LastAnimation.SetDuration(duration);
        return this;
    }

    public AnimationTimelineXVII WithDelay(float delay)
    {
        LastAnimation.SetDelay(delay);
        return this;
    }

    public AnimationTimelineXVII AddOnStart(Action action)
    {
        animations.Add(new OnStartAction(action));
        return this;
    }

    public AnimationTimelineXVII AddOnEnd(Action action)
    {
        animations.Add(new OnEndAction(action));
        return this;
    }

    public AnimationTimelineXVII AddOnUpdate(Action<float> action)
    {
        animations.Add(new OnUpdateAction(action));
        return this;
    }

    public AnimationTimelineXVII AddTimeLine(AnimationTimelineXVII otherTimeline)
    {
        animations.AddRange(otherTimeline.AnimationParts);
        return this;
    }

    public AnimationTimelineXVII WithSubAnimation(params AnimationXVII[] subAnimations)
    {
        LastAnimation.AddSubAnimation(subAnimations.ToList());
        return this;
    }
}
