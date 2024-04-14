using System;

public class OnStartAction : AnimationXVII
{
    Action action;
    public OnStartAction(Action action)
    {
        this.action = action;
        duration = 0;
    }

    protected override void OnEnd()
    {
    }

    protected override void OnStart()
    {
        action.Invoke();
    }

    protected override void OnUpdateAnimation(float timeRatio)
    {
    }
}
