// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EncyclopediaItem
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EncyclopediaItem : IComparable<EncyclopediaItem>
  {
    private string _Title;
    private string _Filename;
    private EncyclopediaCategory _Category;
    private EncyclopediaItemList _RelatedItems;
    private bool _IsCategoryRoot;

    public EncyclopediaItem()
    {
    }

    public EncyclopediaItem(string title, string filename, EncyclopediaCategory category)
      : this(title, filename, category, false)
    {
    }

    public EncyclopediaItem(
      string title,
      string filename,
      EncyclopediaCategory category,
      bool isCategoryRoot)
    {
      this._Title = title;
      this._Filename = filename;
      this._Category = category;
      this._IsCategoryRoot = isCategoryRoot;
      this._RelatedItems = new EncyclopediaItemList();
    }

    public string Title
    {
      get => this._Title;
      set => this._Title = value;
    }

    public string Filename
    {
      get => this._Filename;
      set => this._Filename = value;
    }

    public EncyclopediaCategory Category
    {
      get => this._Category;
      set => this._Category = value;
    }

    public EncyclopediaItemList RelatedItems
    {
      get => this._RelatedItems;
      set => this._RelatedItems = value;
    }

    public bool IsCategoryRoot
    {
      get => this._IsCategoryRoot;
      set => this._IsCategoryRoot = value;
    }

    public override string ToString() => this._Title;

    int IComparable<EncyclopediaItem>.CompareTo(EncyclopediaItem other) => this._Title.CompareTo(other._Title);
  }
}
