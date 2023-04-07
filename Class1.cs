// Decompiled with JetBrains decompiler
// Type: Class1
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;

internal class Class1 : IGraphicsDeviceService
{
    private static Class1 class1_0;
    private static int int_0;
    private GraphicsDevice graphicsDevice_0;
    private PresentationParameters presentationParameters_0;
    private EventHandler<EventArgs> eventHandler_0;
    // .field private class [mscorlib]System.EventHandler`1<class [mscorlib]System.EventArgs> eventHandler_1
    private EventHandler<EventArgs> eventHandler_1;
    // .field private class [mscorlib]System.EventHandler`1<class [mscorlib]System.EventArgs> eventHandler_2
    private EventHandler<EventArgs> eventHandler_2;
    // .field private class [mscorlib]System.EventHandler`1<class [mscorlib]System.EventArgs> eventHandler_3
    private EventHandler<EventArgs> eventHandler_3;
    private Class1(IntPtr intptr_0, int int_1, int int_2) : base()
    {
        Class7.VEFSJNszvZKMZ();
        this.presentationParameters_0 = new PresentationParameters();
        this.presentationParameters_0.BackBufferWidth = Math.Max(int_1, 1);
        this.presentationParameters_0.BackBufferHeight = Math.Max(int_2, 1);
        this.presentationParameters_0.BackBufferFormat = SurfaceFormat.Color;
        this.presentationParameters_0.IsFullScreen = false;
        this.presentationParameters_0.MultiSampleCount = 4;
        this.presentationParameters_0.DepthStencilFormat = DepthFormat.Depth24;
        this.presentationParameters_0.DeviceWindowHandle = intptr_0;
        this.presentationParameters_0.PresentationInterval = PresentInterval.Default;
        this.graphicsDevice_0 = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, this.presentationParameters_0);
    }

    public static Class1 smethod_0(IntPtr intptr_0, int int_1, int int_2)
    {
        if (Interlocked.Increment(ref Class1.int_0) == 1)
            Class1.class1_0 = new Class1(intptr_0, int_1, int_2);
        return Class1.class1_0;
    }

    public void method_0(bool bool_0)
    {
        if (Interlocked.Decrement(ref Class1.int_0) != 0)
            return;
        if (bool_0)
        {
            if (this.eventHandler_1 != null)
                this.eventHandler_1((object)this, EventArgs.Empty);
            this.graphicsDevice_0.Dispose();
        }
        this.graphicsDevice_0 = (GraphicsDevice)null;
    }

    public void method_1(int int_1, int int_2)
    {
        if (this.eventHandler_3 != null)
            this.eventHandler_3((object)this, EventArgs.Empty);
        this.presentationParameters_0.BackBufferWidth = Math.Max(this.presentationParameters_0.BackBufferWidth, int_1);
        this.presentationParameters_0.BackBufferHeight = Math.Max(this.presentationParameters_0.BackBufferHeight, int_2);
        Thread.Sleep(100);
        if (this.graphicsDevice_0 != null)
            this.graphicsDevice_0.Reset(this.presentationParameters_0);
        if (this.eventHandler_2 == null)
            return;
        this.eventHandler_2((object)this, EventArgs.Empty);
    }

    public GraphicsDevice GraphicsDevice => this.graphicsDevice_0;

    public event EventHandler<EventArgs> DeviceCreated;

    public event EventHandler<EventArgs> DeviceDisposing;

    public event EventHandler<EventArgs> DeviceReset;

    public event EventHandler<EventArgs> DeviceResetting;
}
