using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthHolder :ISelectable
{
    public float Health { get; }
    public float MaxHealth { get; }

}
