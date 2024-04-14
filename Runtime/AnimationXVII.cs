using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public abstract class AnimationXVII
{
    public float TotalDuration => duration + delay;
    public const float InvalidDuration = -1;

    public float duration = InvalidDuration;
    public float delay = 0;
    public AnimationCurve animationCurve = null;
    public EaseXVII.Ease easeCurve = EaseXVII.Ease.InOutQuad;
    public bool useAnimationCurve = false;
    public bool isSubAnimation;

    private bool isStarted;
    private bool isCompleted;

    public void UpdateAnimation(float timer)
    {
        float timeRatio = math.saturate((timer - delay) / duration);
        if (!isStarted && timeRatio > 0)
        {
            OnStart();
            isStarted = true;
        }

        timeRatio =
           useAnimationCurve ?
           animationCurve.Evaluate(timeRatio) :
           EaseXVII.Evaluate(timeRatio, easeCurve);

        OnUpdateAnimation(timeRatio);

        if (timeRatio >= 1 && !isCompleted)
        {
            OnEnd();
            isCompleted = true;
        }

        for (int i = 0; i < subAnimations.Count; i++)
        {
            subAnimations[i].UpdateAnimation(timer);
        }
    }

    public void Reset()
    {
        isStarted = false;
        isCompleted = false;
        for (int i = 0; i < subAnimations.Count; i++)
        {
            subAnimations[i].Reset();
        }
    }

    protected abstract void OnStart();
    protected abstract void OnUpdateAnimation(float timeRatio);
    protected abstract void OnEnd();
    public virtual void OnCreated() { }

    public AnimationXVII SetDuration(float duration)
    {
        this.duration = duration;
        for (int i = 0; i < subAnimations.Count; i++)
        {
            if (subAnimations[i].duration == InvalidDuration)
            {
                subAnimations[i].duration = math.clamp(duration, 0, duration - subAnimations[i].delay);
            }
            else
            {
                subAnimations[i].duration = math.clamp(subAnimations[i].duration, 0, duration - subAnimations[i].delay);
            }
        }
        return this;
    }
    public AnimationXVII SetDelay(float delay)
    {
        this.delay = delay;
        for (int i = 0; i < subAnimations.Count; i++)
        {
            subAnimations[i].SetDelay(delay);
        }
        return this;
    }

    public AnimationXVII SetAnimationCurve(AnimationCurve animationCurve)
    {
        this.animationCurve = animationCurve;
        useAnimationCurve = true;

        for (int i = 0; i < subAnimations.Count; i++)
        {
            subAnimations[i].SetAnimationCurve(animationCurve);
        }
        return this;
    }

    public AnimationXVII SetEaseCurve(EaseXVII.Ease easeCurve)
    {
        this.easeCurve = easeCurve;
        useAnimationCurve = false;

        for (int i = 0; i < subAnimations.Count; i++)
        {
            subAnimations[i].SetEaseCurve(easeCurve);
        }
        return this;
    }

    public AnimationXVII AddSubAnimation(List<AnimationXVII> subAnimations)
    {
        if (isSubAnimation)
        {
            Debug.LogError("SubAnimations can't contains subanimations");
            return this;
        }

        subAnimations.AddRange(subAnimations);
        for (int i = 0; i < subAnimations.Count; i++)
        {
            subAnimations[i].isSubAnimation = true;
            duration = math.max(duration, subAnimations[i].duration + subAnimations[i].delay);
        }

        return this;
    }

    public List<AnimationXVII> subAnimations = new List<AnimationXVII>();
}
