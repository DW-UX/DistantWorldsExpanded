// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MessageBoxExForm
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  internal class MessageBoxExForm : Form
  {
    private const int LEFT_PADDING = 12;
    private const int RIGHT_PADDING = 12;
    private const int TOP_PADDING = 12;
    private const int BOTTOM_PADDING = 12;
    private const int BUTTON_LEFT_PADDING = 4;
    private const int BUTTON_RIGHT_PADDING = 4;
    private const int BUTTON_TOP_PADDING = 4;
    private const int BUTTON_BOTTOM_PADDING = 4;
    private const int MIN_BUTTON_HEIGHT = 28;
    private const int MIN_BUTTON_WIDTH = 150;
    private const int ITEM_PADDING = 10;
    private const int ICON_MESSAGE_PADDING = 15;
    private const int BUTTON_PADDING = 5;
    private const int CHECKBOX_WIDTH = 20;
    private const int IMAGE_INDEX_EXCLAMATION = 0;
    private const int IMAGE_INDEX_QUESTION = 1;
    private const int IMAGE_INDEX_STOP = 2;
    private const int IMAGE_INDEX_INFORMATION = 3;
    private const int SPI_GETNONCLIENTMETRICS = 41;
    private const int LF_FACESIZE = 32;
    private const int SC_CLOSE = 61536;
    private const int MF_BYCOMMAND = 0;
    private const int MF_GRAYED = 1;
    private const int MF_ENABLED = 0;
    private IContainer components;
    public CheckBox chbSaveResponse;
    private ImageList imageListIcons;
    private ToolTip buttonToolTip;
    private ArrayList _buttons = new ArrayList();
    private bool _allowSaveResponse;
    private bool _playAlert = true;
    private MessageBoxExButton _cancelButton;
    private GlassButton _defaultButtonControl;
    private int _maxLayoutWidth;
    private int _maxLayoutHeight;
    private int _maxWidth;
    private int _maxHeight;
    private bool _allowCancel = true;
    private string _result;
    private MessageBoxIcon _standardIcon;
    private Icon _iconImage;
    private Timer timerTimeout;
    private int _timeout;
    private TimeoutResult _timeoutResult;
    private System.Windows.Forms.Panel panelIcon;
    private TextBox rtbMessage;
    private Hashtable _buttonControlsTable = new Hashtable();

    public string Message
    {
      set
      {
        this.rtbMessage.Font = this.Font;
        this.rtbMessage.Text = value;
        this.rtbMessage.Font = this.Font;
      }
    }

    public string Caption
    {
      set => this.Text = value;
    }

    public ArrayList Buttons => this._buttons;

    public bool AllowSaveResponse
    {
      get => this._allowSaveResponse;
      set => this._allowSaveResponse = value;
    }

    public bool SaveResponse => this.chbSaveResponse.Checked;

    public string SaveResponseText
    {
      set => this.chbSaveResponse.Text = value;
    }

    public MessageBoxIcon StandardIcon
    {
      set => this.SetStandardIcon(value);
    }

    public Icon CustomIcon
    {
      set
      {
        this._standardIcon = MessageBoxIcon.None;
        this._iconImage = value;
      }
    }

    public MessageBoxExButton CustomCancelButton
    {
      set => this._cancelButton = value;
    }

    public string Result => this._result;

    public bool PlayAlertSound
    {
      get => this._playAlert;
      set => this._playAlert = value;
    }

    public int Timeout
    {
      get => this._timeout;
      set => this._timeout = value;
    }

    public TimeoutResult TimeoutResult
    {
      get => this._timeoutResult;
      set => this._timeoutResult = value;
    }

    public MessageBoxExForm()
    {
      this.InitializeComponent();
      this._maxWidth = (int) ((double) SystemInformation.WorkingArea.Width * 0.6);
      this._maxHeight = (int) ((double) SystemInformation.WorkingArea.Height * 0.9);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      var resources = new ComponentResourceManager(typeof (MessageBoxExForm));
      this.panelIcon = new Panel();
      this.chbSaveResponse = new CheckBox();
      this.imageListIcons = new ImageList(this.components);
      this.buttonToolTip = new ToolTip(this.components);
      this.rtbMessage = new TextBox();
      this.SuspendLayout();
      this.panelIcon.BackColor = Color.Transparent;
      this.panelIcon.Location = new Point(8, 8);
      this.panelIcon.Name = "panelIcon";
      this.panelIcon.Size = new Size(32, 32);
      this.panelIcon.TabIndex = 3;
      this.panelIcon.Visible = false;
      this.chbSaveResponse.FlatStyle = FlatStyle.System;
      this.chbSaveResponse.ForeColor = Color.White;
      this.chbSaveResponse.Location = new Point(56, 56);
      this.chbSaveResponse.Name = "chbSaveResponse";
      this.chbSaveResponse.Size = new Size(150, 16);
      this.chbSaveResponse.TabIndex = 0;
      this.imageListIcons.ImageStream = (ImageListStreamer) resources.GetObject("imageListIcons.ImageStream");
      this.imageListIcons.TransparentColor = Color.Transparent;
      this.imageListIcons.Images.SetKeyName(0, "");
      this.imageListIcons.Images.SetKeyName(1, "");
      this.imageListIcons.Images.SetKeyName(2, "");
      this.imageListIcons.Images.SetKeyName(3, "");
      this.rtbMessage.BackColor = SystemColors.Control;
      this.rtbMessage.BorderStyle = BorderStyle.None;
      this.rtbMessage.Location = new Point(200, 8);
      this.rtbMessage.Name = "rtbMessage";
      this.rtbMessage.Multiline = true;
      this.rtbMessage.ReadOnly = true;
      this.rtbMessage.Size = new Size(100, 48);
      this.rtbMessage.TabIndex = 4;
      this.rtbMessage.Text = "";
      this.rtbMessage.Visible = false;
      this.ClientSize = new Size(322, 224);
      this.Controls.Add((Control) this.rtbMessage);
      this.Controls.Add((Control) this.chbSaveResponse);
      this.Controls.Add((Control) this.panelIcon);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name ="MessageBoxExForm";
      this.StartPosition = FormStartPosition.Manual;
      this.ResumeLayout(false);
    }

    protected override void OnLoad(EventArgs e)
    {
      this._result = (string) null;
      this.Size = new Size(this._maxWidth, this._maxHeight);
      this._maxLayoutWidth = this.ClientSize.Width - 12 - 12;
      this._maxLayoutHeight = this.ClientSize.Height - 12 - 12;
      this.AddOkButtonIfNoButtonsPresent();
      this.DisableCloseIfMultipleButtonsAndNoCancelButton();
      this.SetIconSizeAndVisibility();
      this.SetMessageSizeAndVisibility();
      this.SetCheckboxSizeAndVisibility();
      this.SetOptimumSize();
      this.LayoutControls();
      this.PlayAlert();
      this.SelectDefaultButton();
      this.StartTimerIfTimeoutGreaterThanZero();
      base.OnLoad(e);
    }

    protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData) => keyData == (Keys.F4 | Keys.Alt) && !this._allowCancel || base.ProcessCmdKey(ref msg, keyData);

    protected override void OnClosing(CancelEventArgs e)
    {
      if (this._result == null)
      {
        if (this._allowCancel)
        {
          this._result = this._cancelButton.Value;
        }
        else
        {
          e.Cancel = true;
          return;
        }
      }
      if (this.timerTimeout != null)
        this.timerTimeout.Stop();
      base.OnClosing(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this._iconImage == null)
        return;
      e.Graphics.DrawIcon(this._iconImage, new Rectangle(this.panelIcon.Location, new Size(32, 32)));
    }

    private Size MeasureString(string str, int maxWidth, Font font)
    {
      Graphics graphics = this.CreateGraphics();
      SizeF sizeF = graphics.MeasureString(str, font, maxWidth);
      graphics.Dispose();
      return new Size((int) Math.Ceiling((double) sizeF.Width), (int) Math.Ceiling((double) sizeF.Height));
    }

    private Size MeasureString(string str, int maxWidth) => this.MeasureString(str, maxWidth, this.Font);

    private string GetLongestButtonText()
    {
      int num = 0;
      string longestButtonText = (string) null;
      foreach (MessageBoxExButton button in this._buttons)
      {
        if (button.Text != null && button.Text.Length > num)
        {
          num = button.Text.Length;
          longestButtonText = button.Text;
        }
      }
      return longestButtonText;
    }

    private void SetMessageSizeAndVisibility()
    {
      if (this.rtbMessage.Text == null || this.rtbMessage.Text.Trim().Length == 0)
      {
        this.rtbMessage.Size = Size.Empty;
        this.rtbMessage.Visible = false;
      }
      else
      {
        int maxLayoutWidth = this._maxLayoutWidth;
        if (this.panelIcon.Size.Width != 0)
          maxLayoutWidth -= this.panelIcon.Size.Width + 15;
        Size size = this.MeasureString(this.rtbMessage.Text, maxLayoutWidth - SystemInformation.VerticalScrollBarWidth);
        size.Width += SystemInformation.VerticalScrollBarWidth;
        size.Height = Math.Max(this.panelIcon.Height, size.Height) + SystemInformation.HorizontalScrollBarHeight;
        this.rtbMessage.Size = size;
        this.rtbMessage.Visible = true;
      }
    }

    private void SetIconSizeAndVisibility()
    {
      if (this._iconImage == null)
      {
        this.panelIcon.Visible = false;
        this.panelIcon.Size = Size.Empty;
      }
      else
      {
        this.panelIcon.Size = new Size(32, 32);
        this.panelIcon.Visible = true;
      }
    }

    private void SetCheckboxSizeAndVisibility()
    {
      this.chbSaveResponse.ForeColor = Color.White;
      if (!this.AllowSaveResponse)
      {
        this.chbSaveResponse.Visible = false;
        this.chbSaveResponse.Size = Size.Empty;
      }
      else
      {
        Size size = this.MeasureString(this.chbSaveResponse.Text, this._maxLayoutWidth);
        size.Width += 50;
        this.chbSaveResponse.Size = size;
        this.chbSaveResponse.Visible = true;
      }
    }

    private Size GetButtonSize()
    {
      Size size = this.MeasureString(this.GetLongestButtonText(), this._maxLayoutWidth);
      Size buttonSize = new Size(size.Width + 4 + 4, size.Height + 4 + 4);
      if (buttonSize.Width < 150)
        buttonSize.Width = 150;
      if (buttonSize.Height < 28)
        buttonSize.Height = 28;
      return buttonSize;
    }

    private void SetStandardIcon(MessageBoxIcon icon)
    {
      this._standardIcon = icon;
      switch (icon)
      {
        case MessageBoxIcon.None:
          this._iconImage = (Icon) null;
          break;
        case MessageBoxIcon.Hand:
          this._iconImage = SystemIcons.Error;
          break;
        case MessageBoxIcon.Question:
          this._iconImage = SystemIcons.Question;
          break;
        case MessageBoxIcon.Exclamation:
          this._iconImage = SystemIcons.Exclamation;
          break;
        case MessageBoxIcon.Asterisk:
          this._iconImage = SystemIcons.Asterisk;
          break;
      }
    }

    private void AddOkButtonIfNoButtonsPresent()
    {
      if (this._buttons.Count != 0)
        return;
      this._buttons.Add((object) new MessageBoxExButton()
      {
        Text = MessageBoxExButtons.Ok.ToString(),
        Value = MessageBoxExButtons.Ok.ToString()
      });
    }

    private void CenterForm() => this.Location = new Point((SystemInformation.WorkingArea.Width - this.Width) / 2, (SystemInformation.WorkingArea.Height - this.Height) / 2);

    private void SetOptimumSize()
    {
      int num1 = this.Width - this.ClientSize.Width;
      int num2 = this.Height - this.ClientSize.Height;
      int val1_1 = this.rtbMessage.Width + 15 + this.panelIcon.Width;
      int val1_2 = this.chbSaveResponse.Width + this.panelIcon.Width / 2;
      int widthOfAllButtons = this.GetWidthOfAllButtons();
      int width = this.GetCaptionSize().Width;
      int val1_3 = 12 + Math.Max(val1_2, Math.Max(val1_1, widthOfAllButtons)) + 12 + num1;
      if (val1_3 < width)
        val1_3 = width;
      int val1_4 = 12 + Math.Max(this.rtbMessage.Height, this.panelIcon.Height) + 10 + this.chbSaveResponse.Height + 10 + this.GetButtonSize().Height + 12 + num2;
      if (val1_4 > this._maxHeight)
        this.rtbMessage.Height -= val1_4 - this._maxHeight;
      int height = Math.Min(val1_4, this._maxHeight);
      this.Size = new Size(Math.Min(val1_3, this._maxWidth), height);
    }

    private int GetWidthOfAllButtons() => this.GetButtonSize().Width * this._buttons.Count + 5 * (this._buttons.Count - 1);

    private Size GetCaptionSize()
    {
      Size captionSize = this.MeasureString(this.Text, this._maxWidth - SystemInformation.CaptionButtonSize.Width - SystemInformation.Border3DSize.Width * 2, this.GetCaptionFont() ?? new Font("Tahoma", 11f));
      captionSize.Width += SystemInformation.CaptionButtonSize.Width + SystemInformation.Border3DSize.Width * 2;
      return captionSize;
    }

    private void LayoutControls()
    {
      this.panelIcon.Location = new Point(12, 12);
      this.rtbMessage.Location = new Point(12 + this.panelIcon.Width + 15 * (this.panelIcon.Width == 0 ? 0 : 1), 12);
      this.rtbMessage.BackColor = this.BackColor;
      this.rtbMessage.ForeColor = this.ForeColor;
      this.rtbMessage.Font = this.Font;
      this.chbSaveResponse.Location = new Point(12 + this.panelIcon.Width / 2, 12 + Math.Max(this.panelIcon.Height, this.rtbMessage.Height) + 10);
      this.chbSaveResponse.Font = this.Font;
      Size buttonSize = this.GetButtonSize();
      Point location = new Point((this.ClientSize.Width - this.GetWidthOfAllButtons()) / 2, this.ClientSize.Height - 12 - buttonSize.Height);
      bool flag = false;
      foreach (MessageBoxExButton button1 in this._buttons)
      {
        GlassButton button2 = this.GetButton(button1, buttonSize, location);
        if (!flag)
        {
          this._defaultButtonControl = button2;
          flag = true;
        }
        location.X += buttonSize.Width + 5;
      }
    }

    private GlassButton GetButton(MessageBoxExButton button, Size size, Point location)
    {
      GlassButton button1;
      if (this._buttonControlsTable.ContainsKey((object) button))
      {
        button1 = this._buttonControlsTable[(object) button] as GlassButton;
        button1.Size = size;
        button1.Location = location;
      }
      else
      {
        button1 = this.CreateButton(button, size, location);
        this._buttonControlsTable[(object) button] = (object) button1;
        this.Controls.Add((Control) button1);
      }
      return button1;
    }

    private GlassButton CreateButton(MessageBoxExButton button, Size size, Point location)
    {
      GlassButton button1 = new GlassButton();
      button1.Size = size;
      button1.Text = button.Text;
      button1.TextAlign = ContentAlignment.MiddleCenter;
      button1.Font = this.Font;
      if (button.HelpText != null && button.HelpText.Trim().Length != 0)
        this.buttonToolTip.SetToolTip((Control) button1, button.HelpText);
      button1.Location = location;
      button1.Click += new EventHandler(this.OnButtonClicked);
      button1.Tag = (object) button.Value;
      return button1;
    }

    private void DisableCloseIfMultipleButtonsAndNoCancelButton()
    {
      if (this._buttons.Count > 1)
      {
        if (this._cancelButton != null)
          return;
        foreach (MessageBoxExButton button in this._buttons)
        {
          if (button.Text == MessageBoxExButtons.Cancel.ToString() && button.Value == MessageBoxExButtons.Cancel.ToString())
          {
            this._cancelButton = button;
            return;
          }
        }
        this.DisableCloseButton((Form) this);
        this._allowCancel = false;
      }
      else if (this._buttons.Count == 1)
        this._cancelButton = this._buttons[0] as MessageBoxExButton;
      else
        this._allowCancel = false;
    }

    private void PlayAlert()
    {
      if (!this._playAlert)
        return;
      if (this._standardIcon != MessageBoxIcon.None)
        MessageBoxExForm.MessageBeep((uint) this._standardIcon);
      else
        MessageBoxExForm.MessageBeep(0U);
    }

    private void SelectDefaultButton()
    {
      if (this._defaultButtonControl == null)
        return;
      this._defaultButtonControl.Select();
    }

    private void StartTimerIfTimeoutGreaterThanZero()
    {
      if (this._timeout <= 0)
        return;
      if (this.timerTimeout == null)
      {
        this.timerTimeout = new Timer(this.components);
        this.timerTimeout.Tick += new EventHandler(this.timerTimeout_Tick);
      }
      if (this.timerTimeout.Enabled)
        return;
      this.timerTimeout.Interval = this._timeout;
      this.timerTimeout.Start();
    }

    private void SetResultAndClose(string result)
    {
      this._result = result;
      this.DialogResult = DialogResult.OK;
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
      if (!(sender is Button button) || button.Tag == null)
        return;
      this.SetResultAndClose(button.Tag as string);
    }

    private void timerTimeout_Tick(object sender, EventArgs e)
    {
      this.timerTimeout.Stop();
      switch (this._timeoutResult)
      {
        case TimeoutResult.Default:
          this._defaultButtonControl.PerformClick();
          break;
        case TimeoutResult.Cancel:
          if (this._cancelButton != null)
          {
            this.SetResultAndClose(this._cancelButton.Value);
            break;
          }
          this._defaultButtonControl.PerformClick();
          break;
        case TimeoutResult.Timeout:
          this.SetResultAndClose("Timeout");
          break;
      }
    }

    private Font GetCaptionFont() => this.Font;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SystemParametersInfo(
      int uiAction,
      int uiParam,
      ref MessageBoxExForm.NONCLIENTMETRICS ncMetrics,
      int fWinIni);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

    private void DisableCloseButton(Form form)
    {
      try
      {
        MessageBoxExForm.EnableMenuItem(MessageBoxExForm.GetSystemMenu(form.Handle, false), 61536U, 1U);
      }
      catch (Exception ex)
      {
      }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool MessageBeep(uint type);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct LOGFONT
    {
      public int lfHeight;
      public int lfWidth;
      public int lfEscapement;
      public int lfOrientation;
      public int lfWeight;
      public byte lfItalic;
      public byte lfUnderline;
      public byte lfStrikeOut;
      public byte lfCharSet;
      public byte lfOutPrecision;
      public byte lfClipPrecision;
      public byte lfQuality;
      public byte lfPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string lfFaceSize;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct NONCLIENTMETRICS
    {
      public int cbSize;
      public int iBorderWidth;
      public int iScrollWidth;
      public int iScrollHeight;
      public int iCaptionWidth;
      public int iCaptionHeight;
      public MessageBoxExForm.LOGFONT lfCaptionFont;
      public int iSmCaptionWidth;
      public int iSmCaptionHeight;
      public MessageBoxExForm.LOGFONT lfSmCaptionFont;
      public int iMenuWidth;
      public int iMenuHeight;
      public MessageBoxExForm.LOGFONT lfMenuFont;
      public MessageBoxExForm.LOGFONT lfStatusFont;
      public MessageBoxExForm.LOGFONT lfMessageFont;
    }
  }
}
