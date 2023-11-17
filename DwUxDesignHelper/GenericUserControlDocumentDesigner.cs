using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Windows.Forms.Design;

namespace DistantWorlds.Controls.Design;

[ToolboxItemFilter("System.Windows.Forms.Control", ToolboxItemFilterType.Allow)]
internal class GenericControlDocumentDesigner : DocumentDesigner {

  public GenericControlDocumentDesigner()
    => AutoResizeHandles = true;

  private Size Size {
    get => Control.ClientSize;
    set => Control.ClientSize = value;
  }

  protected override void PreFilterProperties(IDictionary properties) {
    base.PreFilterProperties(properties);

    // Handle shadowed properties
    string[] shadowProps = { "Size" };

    var empty = Array.Empty<Attribute>();

    for (var i = 0; i < shadowProps.Length; i++) {
      if (properties[shadowProps[i]] is PropertyDescriptor prop)
        properties[shadowProps[i]] = TypeDescriptor.CreateProperty
          (typeof(GenericControlDocumentDesigner), prop, empty);
    }
  }

}