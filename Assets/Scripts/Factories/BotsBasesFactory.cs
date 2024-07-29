using UnityEngine;

public class BotsBasesFactory : MonoBehaviourFactory<BotsBase>
{
    public BotsBasesFactory(BotsBase prefab) : base(prefab) { }

    public override BotsBase Create(Transform parent) =>
        base.Create(parent);
}
