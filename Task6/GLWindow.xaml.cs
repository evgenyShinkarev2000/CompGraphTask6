﻿using OpenTK.Graphics.OpenGL;
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
            if (Commands.Count > 0)
            {
                foreach (var painter in Commands)
                {
                    painter.Execute();
                }
            }
            Commands.Clear();
        }
    }
}
