using System;
using UnityEngine;

[Serializable]
public class InterfaceReference<Tinterface, Tclass> where Tclass : MonoBehaviour, Tinterface
{
    [SerializeField] private Tclass _value;

    public Tinterface Value => _value;
}
