using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task6.Commands;
using Task6.Interfaces;
using Task6.Painters;

namespace Task6
{
    /// <summary>
    /// Interaction logic for GLWindow.xaml
    /// </summary>
    public partial class GLWindow : Window
    {
        internal readonly List<IMyCommand> Commands = new List<IMyCommand>();
        public Matrix4 RotationX = Matrix4.Identity;
        public Matrix4 RotationY = Matrix4.Identity;
        public Matrix4 Translation = Matrix4.Identity;
        public Matrix4 Final = Matrix4.Identity;
        private readonly List<IMyCommand> previousCommands = new List<IMyCommand>();
        private Vector2 deltaCamera;
        private Vector2 TranslateDelta;
        private Vector2 previousMousePosition;

        public GLWindow()
        {
            InitializeComponent();
            OpenTkControl.Start(new GLWpfControlSettings() { MajorVersion = 2, MinorVersion = 1, RenderContinuously = false });
            Commands.Add(new Fill());
            Commands.Add(new DrawArbitaryPoligon().WithVectors(new[] { new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(1, -1, -0.5f) }));
            OpenTkControl.InvalidateVisual();
        }

        public void OpenTkControl_OnRender(TimeSpan delta)
        {
            new Fill().Execute();
            Final = RotationX * RotationY * Translation;
            GL.LoadMatrix(ref Final);
            foreach (var painter in Commands)
            {
                painter.Execute();
            }
            new DrawCoordinates().Execute();

            previousCommands.Clear();
            previousCommands.AddRange(Commands.Select(c => c));
            Commands.Clear();
            GL.LoadIdentity();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var currentMouseX = (float)e.GetPosition(this).X;
            var currentMouseY = (float)e.GetPosition(this).Y;
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var dX = previousMousePosition.X - currentMouseX;
                var dY = previousMousePosition.Y - currentMouseY;
                RotateCamera(dX, dY);
            }

            previousMousePosition = new Vector2(currentMouseX, currentMouseY);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var sensetivity = 0.02f;
            switch (e.Key)
            {
                case Key.W:
                    MoveCamera(sensetivity, 0);
                    break;
                case Key.S:
                    MoveCamera(-sensetivity, 0);
                    break;
                case Key.D:
                    MoveCamera(0, sensetivity);
                    break;
                case Key.A:
                    MoveCamera(0, -sensetivity);
                    break;
            }
        }

        private void RotateCamera(float dX, float dY)
        {
            deltaCamera = new Vector2(deltaCamera.X + dX, deltaCamera.Y + dY);
            Commands.AddRange(previousCommands.Select(c => c));
            previousCommands.Clear();
            Matrix4.CreateRotationX(-(float)deltaCamera.Y / 50, out RotationX);
            Matrix4.CreateRotationY(-(float)deltaCamera.X / 50, out RotationY);
            OpenTkControl.InvalidateVisual();
        }

        private void MoveCamera(float x, float y)
        {
            TranslateDelta = new Vector2(TranslateDelta.X + x, TranslateDelta.Y + y);
            Matrix4.CreateTranslation(TranslateDelta.Y, TranslateDelta.X, 0, out Translation);
            Commands.AddRange(previousCommands.Select(c => c));
            previousCommands.Clear();
            OpenTkControl.InvalidateVisual();
        }

    }
}
