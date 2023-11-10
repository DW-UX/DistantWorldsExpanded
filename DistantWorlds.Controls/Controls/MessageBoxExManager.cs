// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MessageBoxExManager
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DistantWorlds.Controls
{
  public class MessageBoxExManager
  {
    private static Hashtable _messageBoxes = new Hashtable();
    private static Hashtable _savedResponses = new Hashtable();
    private static Hashtable _standardButtonsText = new Hashtable();

    static MessageBoxExManager()
    {
      MessageBoxExManager._standardButtonsText[(object) MessageBoxExButtons.Ok.ToString()] = (object) TextResolver.GetText("Ok");
      MessageBoxExManager._standardButtonsText[(object) MessageBoxExButtons.Cancel.ToString()] = (object) TextResolver.GetText("Cancel");
      MessageBoxExManager._standardButtonsText[(object) MessageBoxExButtons.Yes.ToString()] = (object) TextResolver.GetText("Yes");
      MessageBoxExManager._standardButtonsText[(object) MessageBoxExButtons.No.ToString()] = (object) TextResolver.GetText("No");
      MessageBoxExManager._standardButtonsText[(object) MessageBoxExButtons.Abort.ToString()] = (object) TextResolver.GetText("Abort");
      MessageBoxExManager._standardButtonsText[(object) MessageBoxExButtons.Retry.ToString()] = (object) TextResolver.GetText("Retry");
      MessageBoxExManager._standardButtonsText[(object) MessageBoxExButtons.Ignore.ToString()] = (object) TextResolver.GetText("Ignore");
      MessageBoxExManager._standardButtonsText[(object) MessageBoxExButtons.Show.ToString()] = (object) TextResolver.GetText("Show me first");
    }

    public static MessageBoxEx CreateMessageBox(string name, Font font)
    {
      if (name != null && MessageBoxExManager._messageBoxes.ContainsKey((object) name))
        throw new ArgumentException(string.Format("A MessageBox with the name {0} already exists.", (object) name), nameof (name));
      MessageBoxEx messageBox = new MessageBoxEx(font);
      messageBox.Name = name;
      if (messageBox.Name != null)
        MessageBoxExManager._messageBoxes[(object) name] = (object) messageBox;
      return messageBox;
    }

    public static MessageBoxEx GetMessageBox(string name) => MessageBoxExManager._messageBoxes.Contains((object) name) ? MessageBoxExManager._messageBoxes[(object) name] as MessageBoxEx : (MessageBoxEx) null;

    public static void DeleteMessageBox(string name)
    {
      if (name == null || !MessageBoxExManager._messageBoxes.Contains((object) name))
        return;
      (MessageBoxExManager._messageBoxes[(object) name] as MessageBoxEx).Dispose();
      MessageBoxExManager._messageBoxes.Remove((object) name);
    }

    public static void WriteSavedResponses(string filename)
    {
      try
      {
        FileStream serializationStream = new FileStream(filename, FileMode.Create);
        try
        {
          new BinaryFormatter().Serialize((Stream) serializationStream, (object) MessageBoxExManager._savedResponses);
        }
        finally
        {
          serializationStream.Close();
        }
      }
      catch
      {
      }
    }

    public static void ReadSavedResponses(string filename)
    {
      if (!File.Exists(filename))
        return;
      try
      {
        FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
        try
        {
          object obj = new BinaryFormatter().Deserialize((Stream) serializationStream);
          if (!(obj is Hashtable))
            return;
          MessageBoxExManager._savedResponses = (Hashtable) obj;
        }
        finally
        {
          serializationStream.Close();
        }
      }
      catch
      {
      }
    }

    public static void ResetSavedResponse(string messageBoxName)
    {
      if (messageBoxName == null || !MessageBoxExManager._savedResponses.ContainsKey((object) messageBoxName))
        return;
      MessageBoxExManager._savedResponses.Remove((object) messageBoxName);
    }

    public static void ResetAllSavedResponses() => MessageBoxExManager._savedResponses.Clear();

    internal static void SetSavedResponse(MessageBoxEx msgBox, string response)
    {
      if (msgBox.Name == null)
        return;
      MessageBoxExManager._savedResponses[(object) msgBox.Name] = (object) response;
    }

    internal static string GetSavedResponse(MessageBoxEx msgBox)
    {
      string name = msgBox.Name;
      if (name == null)
        return (string) null;
      return MessageBoxExManager._savedResponses.ContainsKey((object) name) ? MessageBoxExManager._savedResponses[(object) msgBox.Name].ToString() : (string) null;
    }

    internal static string GetLocalizedString(string key) => MessageBoxExManager._standardButtonsText.ContainsKey((object) key) ? (string) MessageBoxExManager._standardButtonsText[(object) key] : (string) null;
  }
}
