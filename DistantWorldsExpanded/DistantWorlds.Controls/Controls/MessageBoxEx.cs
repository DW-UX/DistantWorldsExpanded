// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MessageBoxEx
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class MessageBoxEx
  {
    private MessageBoxExForm _msgBox = new MessageBoxExForm();
    private bool _useSavedResponse = true;
    private string _name;

    internal string Name
    {
      get => this._name;
      set => this._name = value;
    }

    public string Caption
    {
      set => this._msgBox.Caption = value;
    }

    public string Text
    {
      set => this._msgBox.Message = value;
    }

    public System.Drawing.Icon CustomIcon
    {
      set => this._msgBox.CustomIcon = value;
    }

    public MessageBoxExIcon Icon
    {
      set => this._msgBox.StandardIcon = (MessageBoxIcon) Enum.Parse(typeof (MessageBoxIcon), value.ToString());
    }

    public Font Font
    {
      get => this._msgBox.Font;
      set => this._msgBox.Font = value;
    }

    public bool AllowSaveResponse
    {
      get => this._msgBox.AllowSaveResponse;
      set => this._msgBox.AllowSaveResponse = value;
    }

    public string SaveResponseText
    {
      set => this._msgBox.SaveResponseText = value;
    }

    public bool UseSavedResponse
    {
      get => this._useSavedResponse;
      set => this._useSavedResponse = value;
    }

    public bool PlayAlertSound
    {
      get => this._msgBox.PlayAlertSound;
      set => this._msgBox.PlayAlertSound = value;
    }

    public int Timeout
    {
      get => this._msgBox.Timeout;
      set => this._msgBox.Timeout = value;
    }

    public TimeoutResult TimeoutResult
    {
      get => this._msgBox.TimeoutResult;
      set => this._msgBox.TimeoutResult = value;
    }

    public string Show() => this.Show((IWin32Window) null, Point.Empty);

    public string Show(IWin32Window owner) => this.Show(owner, Point.Empty);

    public string Show(IWin32Window owner, Point location)
    {
      this._msgBox.chbSaveResponse.UseVisualStyleBackColor = false;
      this._msgBox.chbSaveResponse.FlatStyle = FlatStyle.Standard;
      this._msgBox.chbSaveResponse.ForeColor = Color.FromArgb(170, 170, 170);
      if (this._useSavedResponse && this.Name != null)
      {
        string savedResponse = MessageBoxExManager.GetSavedResponse(this);
        if (savedResponse != null)
          return savedResponse;
      }
      if (location != Point.Empty)
      {
        this._msgBox.StartPosition = FormStartPosition.Manual;
        this._msgBox.Location = location;
      }
      else
        this._msgBox.StartPosition = FormStartPosition.CenterParent;
      if (owner == null)
      {
        int num1 = (int) this._msgBox.ShowDialog();
      }
      else
      {
        int num2 = (int) this._msgBox.ShowDialog(owner);
      }
      if (this.Name != null)
      {
        if (this._msgBox.AllowSaveResponse && this._msgBox.SaveResponse)
          MessageBoxExManager.SetSavedResponse(this, this._msgBox.Result);
        else
          MessageBoxExManager.ResetSavedResponse(this.Name);
      }
      else
        this.Dispose();
      return this._msgBox.Result;
    }

    public void AddButton(MessageBoxExButton button)
    {
      if (button == null)
        throw new ArgumentNullException(nameof (button), "A null button cannot be added");
      this._msgBox.Buttons.Add((object) button);
      if (!button.IsCancelButton)
        return;
      this._msgBox.CustomCancelButton = button;
    }

    public void AddButton(string text, string val)
    {
      if (text == null)
        throw new ArgumentNullException(nameof (text), "Text of a button cannot be null");
      if (val == null)
        throw new ArgumentNullException(nameof (val), "Value of a button cannot be null");
      this.AddButton(new MessageBoxExButton()
      {
        Text = text,
        Value = val
      });
    }

    public void AddButton(MessageBoxExButtons button)
    {
      string str1 = MessageBoxExManager.GetLocalizedString(button.ToString()) ?? button.ToString();
      string str2 = button.ToString();
      MessageBoxExButton button1 = new MessageBoxExButton();
      button1.Text = str1;
      button1.Value = str2;
      if (button == MessageBoxExButtons.Cancel)
        button1.IsCancelButton = true;
      this.AddButton(button1);
    }

    public void AddButtons(MessageBoxButtons buttons)
    {
      switch (buttons)
      {
        case MessageBoxButtons.OK:
          this.AddButton(MessageBoxExButtons.Ok);
          break;
        case MessageBoxButtons.OKCancel:
          this.AddButton(MessageBoxExButtons.Ok);
          this.AddButton(MessageBoxExButtons.Cancel);
          break;
        case MessageBoxButtons.AbortRetryIgnore:
          this.AddButton(MessageBoxExButtons.Abort);
          this.AddButton(MessageBoxExButtons.Retry);
          this.AddButton(MessageBoxExButtons.Ignore);
          break;
        case MessageBoxButtons.YesNoCancel:
          this.AddButton(MessageBoxExButtons.Yes);
          this.AddButton(MessageBoxExButtons.No);
          this.AddButton(MessageBoxExButtons.Cancel);
          break;
        case MessageBoxButtons.YesNo:
          this.AddButton(MessageBoxExButtons.Yes);
          this.AddButton(MessageBoxExButtons.No);
          break;
        case MessageBoxButtons.RetryCancel:
          this.AddButton(MessageBoxExButtons.Retry);
          this.AddButton(MessageBoxExButtons.Cancel);
          break;
      }
    }

    internal MessageBoxEx(Font font)
    {
      this.Font = font;
      this._msgBox.Font = font;
      this._msgBox.Message = string.Empty;
      this._msgBox.ForeColor = Color.FromArgb(170, 170, 170);
      this._msgBox.BackColor = Color.FromArgb(48, 48, 64);
    }

    internal void Dispose()
    {
      if (this._msgBox == null)
        return;
      this._msgBox.Dispose();
    }
  }
}
