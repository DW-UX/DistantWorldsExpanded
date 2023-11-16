// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.OrderList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class OrderList : SyncList<Order>
  {
    private List<int> _HabitatIndexes = new List<int>();
    private List<int> _BuiltObjectIndexes = new List<int>();
    private List<int> _EmpireIndexes = new List<int>();
    private List<List<Order>> _HabitatOrders = new List<List<Order>>();
    private List<List<Order>> _BuiltObjectOrders = new List<List<Order>>();
    private List<List<Order>> _EmpireOrders = new List<List<Order>>();
    private bool _IsIndexed;

    public bool IsIndexed => this._IsIndexed;

    public void EnableIndexing() => this._IsIndexed = true;

    public new void Add(Order order)
    {
      lock (this._LockObject)
      {
        base.Add(order);
        if (!this._IsIndexed)
          return;
        Empire empire = (Empire) null;
        if (order.RequestingBuiltObject != null)
        {
          empire = order.RequestingBuiltObject.ActualEmpire;
          int index = this._BuiltObjectIndexes.IndexOf(order.RequestingBuiltObject.BuiltObjectID);
          if (index >= 0)
          {
            if (this._BuiltObjectOrders[index] == null)
              this._BuiltObjectOrders[index] = new List<Order>();
            this._BuiltObjectOrders[index].Add(order);
          }
          else
          {
            int count = this._BuiltObjectIndexes.Count;
            this._BuiltObjectIndexes.Add(order.RequestingBuiltObject.BuiltObjectID);
            this._BuiltObjectOrders.Add(new List<Order>());
            this._BuiltObjectOrders[count].Add(order);
          }
        }
        if (order.RequestingColony != null)
        {
          empire = order.RequestingColony.Empire;
          int index = this._HabitatIndexes.IndexOf(order.RequestingColony.HabitatIndex);
          if (index >= 0)
          {
            if (this._HabitatOrders[index] == null)
              this._HabitatOrders[index] = new List<Order>();
            this._HabitatOrders[index].Add(order);
          }
          else
          {
            int count = this._HabitatIndexes.Count;
            this._HabitatIndexes.Add(order.RequestingColony.HabitatIndex);
            this._HabitatOrders.Add(new List<Order>());
            this._HabitatOrders[count].Add(order);
          }
        }
        if (empire == null)
          return;
        int index1 = this._EmpireIndexes.IndexOf(empire.EmpireId);
        if (index1 >= 0)
        {
          if (this._EmpireOrders[index1] == null)
            this._EmpireOrders[index1] = new List<Order>();
          this._EmpireOrders[index1].Add(order);
        }
        else
        {
          int count = this._EmpireIndexes.Count;
          this._EmpireIndexes.Add(empire.EmpireId);
          this._EmpireOrders.Add(new List<Order>());
          this._EmpireOrders[count].Add(order);
        }
      }
    }

    public new void Remove(Order order)
    {
      lock (this._LockObject)
      {
        base.Remove(order);
        if (!this._IsIndexed)
          return;
        Empire empire = (Empire) null;
        if (order.RequestingBuiltObject != null)
        {
          empire = order.RequestingBuiltObject.ActualEmpire;
          int index = this._BuiltObjectIndexes.IndexOf(order.RequestingBuiltObject.BuiltObjectID);
          if (index >= 0)
          {
            this._BuiltObjectOrders[index].Remove(order);
            if (this._BuiltObjectOrders[index].Count == 0)
            {
              this._BuiltObjectOrders.RemoveAt(index);
              this._BuiltObjectIndexes.RemoveAt(index);
            }
          }
        }
        if (order.RequestingColony != null)
        {
          empire = order.RequestingColony.Empire;
          int index = this._HabitatIndexes.IndexOf(order.RequestingColony.HabitatIndex);
          if (index >= 0)
          {
            this._HabitatOrders[index].Remove(order);
            if (this._HabitatOrders[index].Count == 0)
            {
              this._HabitatOrders.RemoveAt(index);
              this._HabitatIndexes.RemoveAt(index);
            }
          }
        }
        if (empire == null)
          return;
        int index1 = this._EmpireIndexes.IndexOf(empire.EmpireId);
        if (index1 < 0)
          return;
        this._EmpireOrders[index1].Remove(order);
        if (this._EmpireOrders[index1].Count != 0)
          return;
        this._EmpireOrders.RemoveAt(index1);
        this._EmpireIndexes.RemoveAt(index1);
      }
    }

    public void UpdateHabitatIndexes(int startIndex, int offset)
    {
      if (!this._IsIndexed)
        return;
      lock (this._LockObject)
      {
        for (int index = 0; index < this._HabitatIndexes.Count; ++index)
        {
          int habitatIndex = this._HabitatIndexes[index];
          if (habitatIndex >= startIndex)
          {
            int num = habitatIndex + offset;
            this._HabitatIndexes[index] = num;
          }
        }
      }
    }

    public OrderList GetOrders(Habitat colony)
    {
      OrderList orders = new OrderList();
      if (this._IsIndexed)
      {
        lock (this._LockObject)
        {
          int index = this._HabitatIndexes.IndexOf(colony.HabitatIndex);
          if (index >= 0)
          {
            if (this._HabitatOrders.Count > index)
            {
              List<Order> habitatOrder = this._HabitatOrders[index];
              orders.AddRange((IEnumerable<Order>) habitatOrder.ToArray());
            }
          }
        }
      }
      else
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Order order = this[index];
          if (order.RequestingColony == colony)
            orders.Add(order);
        }
      }
      return orders;
    }

    public OrderList GetOrders(BuiltObject builtObject)
    {
      OrderList orders = new OrderList();
      if (this._IsIndexed)
      {
        lock (this._LockObject)
        {
          int index = this._BuiltObjectIndexes.IndexOf(builtObject.BuiltObjectID);
          if (index >= 0)
          {
            if (this._BuiltObjectOrders.Count > index)
            {
              List<Order> builtObjectOrder = this._BuiltObjectOrders[index];
              orders.AddRange((IEnumerable<Order>) builtObjectOrder.ToArray());
            }
          }
        }
      }
      else
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Order order = this[index];
          if (order.RequestingBuiltObject == builtObject)
            orders.Add(order);
        }
      }
      return orders;
    }

    public OrderList GetOrders(Empire empire)
    {
      OrderList orders = new OrderList();
      if (this._IsIndexed)
      {
        lock (this._LockObject)
        {
          int index = this._EmpireIndexes.IndexOf(empire.EmpireId);
          if (index >= 0)
          {
            if (this._EmpireOrders.Count > index)
            {
              List<Order> empireOrder = this._EmpireOrders[index];
              orders.AddRange((IEnumerable<Order>) empireOrder.ToArray());
            }
          }
        }
      }
      else
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Order order = this[index];
          if (order.RequestingColony != null && order.RequestingColony.Owner == empire)
            orders.Add(order);
          else if (order.RequestingBuiltObject != null && order.RequestingBuiltObject.ActualEmpire == empire)
            orders.Add(order);
        }
      }
      return orders;
    }

    public OrderList GetOrders(byte resourceId)
    {
      OrderList orders = new OrderList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].CommodityResource != null && (int) this[index].CommodityResource.ResourceID == (int) resourceId)
          orders.Add(this[index]);
      }
      return orders;
    }

    public int IndexOf(ResourceGroup resourceGroup, int startIndex)
    {
      if (startIndex >= this.Count)
        return -1;
      lock (this._LockObject)
      {
        for (int index = startIndex; index < this.Count; ++index)
        {
          if (this[index].CommodityResource != null && this[index].CommodityResource.Group == resourceGroup)
            return index;
        }
      }
      return -1;
    }

    public int IndexOf(byte resourceId, int startIndex)
    {
      if (startIndex >= this.Count)
        return -1;
      lock (this._LockObject)
      {
        for (int index = startIndex; index < this.Count; ++index)
        {
          if (this[index].CommodityResource != null && (int) this[index].CommodityResource.ResourceID == (int) resourceId)
            return index;
        }
      }
      return -1;
    }

    public void SplitOrdersByType(
      out OrderList standardOrders,
      out OrderList constructionShortageOrders,
      out OrderList constructionShortageMobileOrders,
      out OrderList retrofitResourcesForBaseOrders)
    {
      standardOrders = new OrderList();
      constructionShortageOrders = new OrderList();
      constructionShortageMobileOrders = new OrderList();
      retrofitResourcesForBaseOrders = new OrderList();
      for (int index = 0; index < this.Count; ++index)
      {
        Order order = this[index];
        if (order != null)
        {
          switch (order.Type)
          {
            case OrderType.Standard:
              standardOrders.Add(order);
              continue;
            case OrderType.RetrofitResourcesForBase:
              retrofitResourcesForBaseOrders.Add(order);
              continue;
            case OrderType.ConstructionShortage:
              constructionShortageOrders.Add(order);
              continue;
            case OrderType.ConstructionShortageMobile:
              constructionShortageMobileOrders.Add(order);
              continue;
            default:
              continue;
          }
        }
      }
    }

    public void MergeRange(OrderList orders)
    {
      for (int index = 0; index < orders.Count; ++index)
      {
        Order order = orders[index];
        if (order != null && !this.Contains(order))
          this.Add(order);
      }
    }
  }
}
