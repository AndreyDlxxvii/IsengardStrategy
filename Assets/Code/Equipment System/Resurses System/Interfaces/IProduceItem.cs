using System.Collections;
using System.Collections.Generic;
using EquipSystem;
using ResurseSystem;
using UnityEngine;

public interface IProduceItem:IProduce
{

   public Item—arrierHolder ProducedItem { get; }
     
   public Item—arrierHolder GetItem(int value);
}
