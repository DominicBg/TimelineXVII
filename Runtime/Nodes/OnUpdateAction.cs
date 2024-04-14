using System;

public class OnUpdateAction : AnimationXVII
{
    Action<float> action;
    public OnUpdateAction(Action<float> action)
    {
        this.action = action;
    }

    protected override void OnEnd()
    {
    }

    protected override void OnStart()
    {
    }

    protected override void OnUpdateAnimation(float timeRatio)
    {
        action.Invoke(timeRatio);
    }
}