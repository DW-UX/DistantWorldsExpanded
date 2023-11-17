// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Windows.Forms.Design;

namespace DistantWorlds.Controls.Design;

/// <summary>
///  <para>Provides a base implementation of a designer for user controls.</para>
/// </summary>
[ToolboxItemFilter("DistantWorlds.Controls.Panel", ToolboxItemFilterType.Custom)]
internal class PanelDocumentDesigner : DocumentDesigner {

  public PanelDocumentDesigner()
    => AutoResizeHandles = true;

  /// <summary>
  ///  On user controls, size == client size.  We do this so we can mess around
  ///  with the non-client area of the user control when editing menus and not
  ///  mess up the size property.
  /// </summary>
  private Size Size {
    get => Control.ClientSize;
    set => Control.ClientSize = value;
  }

  /// <summary>
  ///  Allows a designer to filter the set of properties
  ///  the component it is designing will expose through the
  ///  TypeDescriptor object.  This method is called
  ///  immediately before its corresponding "Post" method.
  ///  If you are overriding this method you should call
  ///  the base implementation before you perform your own
  ///  filtering.
  /// </summary>
  protected override void PreFilterProperties(IDictionary properties) {
    base.PreFilterProperties(properties);

    // Handle shadowed properties
    string[] shadowProps = { "Size" };

    var empty = Array.Empty<Attribute>();

    for (var i = 0; i < shadowProps.Length; i++) {
      if (properties[shadowProps[i]] is PropertyDescriptor prop)
        properties[shadowProps[i]] = TypeDescriptor.CreateProperty
          (typeof(ScreenPanelDocumentDesigner), prop, empty);
    }
  }

}