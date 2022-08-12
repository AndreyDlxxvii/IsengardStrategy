using EquipmentSystem;
using EquipSystem;
using ResurseSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Produce Item Building Model", menuName = "Buildings/ProduceItemBuildingModel", order = 1)]
    public class ProduceItemBuildingModel : ProduceProductBuildingModel<ItemModel, ItemProduct,ItemÑarrierHolder>
    {        

        public ProduceItemBuildingModel (ProduceItemBuildingModel basebuilding):base(basebuilding)
        {
            
        }

        public override void AddProductForProduce(ItemProduct product)
        {
            ItemProduct tempProduct = new ItemProduct(product);
            ProductsWaitPaid.Add(tempProduct);
        }

        public override void GetPaidForProducts(GlobalResurseStock stock)
        {
            if (ProductsWaitPaid!=null)
            { 
                foreach (ItemProduct product in ProductsWaitPaid)
                {
                    if (product.ProducePrice.PricePaidFlag)
                    { 
                        stock.GetResurseForProduceFromGlobalStock(product);
                    }
                }
            }
        }
    }
}
