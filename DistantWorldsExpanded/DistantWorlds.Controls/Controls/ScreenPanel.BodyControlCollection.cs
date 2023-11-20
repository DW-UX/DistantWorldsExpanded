using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DistantWorlds.Controls;

public partial class ScreenPanel {

  private class BodyControlCollection : ControlCollection {

    public new ScreenPanel Owner => (ScreenPanel)base.Owner;

    public BodyControlCollection(ScreenPanel screenPanel) : base(screenPanel)
      => InnerList = GetInnerList();

    private ArrayList GetInnerList() {
      var typeAec = typeof(ArrangedElementCollection);
      var innerListField = typeAec
        .GetProperty("InnerList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
      var arrayList = (ArrayList)innerListField
        .GetValue(this);
      return arrayList;
    }

    private ArrayList InnerList { get; }

    public override void Add(Control value) {
      var owner = Owner;

      if (value == owner.pnlHeader)
        base.Add(value);
      else if (value == owner.pnlBody)
        base.Add(value);
      else {
        if (owner.pnlBody.Controls.Contains(value))
          return;

        base.Add(value);
        owner.pnlBody.Controls.Add(value);
      }
    }

    public override void Clear()
      => Owner.pnlBody.Controls.Clear();

    public override void AddRange(Control[] controls) {
      foreach (var control in controls)
        Add(control);
    }

    public override void Remove(Control value) {
      try {
        Owner.pnlBody.Controls.Remove(value);
        InnerList.Remove(value);
      }
      catch {
        // ignore
      }
    }

    public override void RemoveByKey(string key) {
      //Owner.pnlBody.Controls.RemoveByKey(key);
      var index = Owner.pnlBody.Controls.IndexOfKey(key);
      var child = Owner.pnlBody.Controls[index];
      Remove(child);
    }

  }

}