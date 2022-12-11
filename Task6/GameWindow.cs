using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Painters;

namespace Task6
{
    public class MainGameWindow : GameWindow
    {
        public MainGameWindow(GameWindowSettings? gws = null, NativeWindowSettings? nws = null)
            : base(gws ?? GameWindowSettings.Default, nws ?? NativeWindowSettings.Default)
        {
            VSync = VSyncMode.On;
        }

        public static MainGameWindow Create(int width = 1920,
                                  int height = 1080,
                                  string title = "OpenTK window",
                                  WindowBorder windowBorder = WindowBorder.Resizable,
                                  WindowState windowState = WindowState.Normal
            )
        {
            var nws = new NativeWindowSettings()
            {//change
                Size = new OpenTK.Mathematics.Vector2i(width, height),
                Title = title,
                WindowState = windowState,
                WindowBorder = windowBorder,
                // no change
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.Default,
                APIVersion = new Version(4, 6),
                API = ContextAPI.OpenGL,
            };

            var gws = new GameWindowSettings()
            {
            };
            return new MainGameWindow(nws: nws);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.Red);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            
            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
