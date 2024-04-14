using System;

public class OnEndAction : AnimationXVII
{
    Action action;
    public OnEndAction(Action action)
    {
        this.action = action;
        duration = 0;
    }

    protected override void OnEnd()
    {
        action.Invoke();
    }

    protected override void OnStart()
    {
    }

    protected override void OnUpdateAnimation(float timeRatio)
    {
    }
}