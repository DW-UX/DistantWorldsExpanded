// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MessageBoxExButton
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

namespace DistantWorlds.Controls
{
  public class MessageBoxExButton
  {
    private string _text;
    private string _value;
    private string _helpText;
    private bool _isCancelButton;

    public string Text
    {
      get => this._text;
      set => this._text = value;
    }

    public string Value
    {
      get => this._value;
      set => this._value = value;
    }

    public string HelpText
    {
      get => this._helpText;
      set => this._helpText = value;
    }

    public bool IsCancelButton
    {
      get => this._isCancelButton;
      set => this._isCancelButton = value;
    }
  }
}
