using System;

public class FromPrioritySetterToIdleBaseTransition : BaseTransition
{
    private readonly Flag _flag;

    public FromPrioritySetterToIdleBaseTransition(BaseState nextState, Flag flag) : base(nextState) =>
        _flag = flag != null ? flag : throw new ArgumentNullException(nameof(flag));

    public override void Update()
    {
        if (_flag.IsFree == false)
            Open();
    }
}
