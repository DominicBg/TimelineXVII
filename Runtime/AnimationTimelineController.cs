using System.Collections.Generic;
using UnityEngine;

public class AnimationTimelineController
{
    List<AnimationXVII> animations;
    bool cycleAnimations;
    AnimationTimelineXVII timeline;
    private float internalTimer;
    private int currentAnimationIndex = 0;
    public bool IsRunning { get; private set; }
    public float TotalDuration { get; private set; }

    public AnimationTimelineController(AnimationTimelineFactory factory)
    {
        timeline = new AnimationTimelineXVII();
        factory.CreateAnimationTimeLine(timeline);

        IsRunning = false;
        animations = timeline.AnimationParts;

        TotalDuration = 0;
        for (int i = 0; i < animations.Count; i++)
        {
            TotalDuration += animations[i].duration;
            TryCreateDefaultSettings(animations[i]);
            for (int j = 0; j < animations[i].subAnimations.Count; j++)
            {
                TryCreateDefaultSettings(animations[i].subAnimations[j]);
            }
        }
    }

    void TryCreateDefaultSettings(AnimationXVII animation)
    {
        if (animation.duration == AnimationXVII.InvalidDuration)
        {
            Debug.LogError(animation + " has a unset duration, will be of 0 secs");
            animation.duration = 0;
        }
    }

    public void Start(bool cycleAnimations = false)
    {
        this.cycleAnimations = cycleAnimations;
        IsRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (!IsRunning)
        {
            Debug.LogError("Can't run without using Start()");
            return;
        }

        internalTimer += deltaTime;

        var animationAutomation = animations[currentAnimationIndex];
        animationAutomation.UpdateAnimation(internalTimer);

        if (internalTimer > animationAutomation.TotalDuration)
        {
            internalTimer -= animationAutomation.TotalDuration;

            currentAnimationIndex++;
            if (cycleAnimations)
            {
                currentAnimationIndex = currentAnimationIndex % animations.Count;

                bool justReset = currentAnimationIndex == 0;
                if (justReset)
                {
                    for (int i = 0; i < animations.Count; i++)
                    {
                        animations[i].Reset();
                    }
                }
            }
            if (currentAnimationIndex >= animations.Count)
            {
                IsRunning = false;
                return;
            }
        }
    }
}