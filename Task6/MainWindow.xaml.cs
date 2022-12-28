using Ninject;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Task6.Commands;
using Ninject.Extensions.Conventions;
using Task6.Extensions;
using Task6.Interfaces;
using Task6.Painters;
using Task6.Parameters;
using System.Windows.Controls;
using OpenTK.Graphics.OpenGL;

namespace Task6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly GLWindow game = new GLWindow();
        public readonly GLWpfControl gameControl;
        private Dictionary<Type, FrameworkElement[]> paramPairsControl = new Dictionary<Type, FrameworkElement[]>();
        public MainWindow()
        {
            InitDIContainer();
            gameControl = game.OpenTkControl;
            InitializeComponent();
            InitializeCustom();

            game.Show();
        }

        public void InitDIContainer()
        {
            DIContainer.standartKernel
                .Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom<IDrawFigureCommand>().BindAllInterfaces());
            DIContainer.standartKernel.Bind<RotateParams>().ToMethod(context => GetRotate());
            DIContainer.standartKernel.Bind<TranslateParams>().ToMethod(context => GetCenter());
            DIContainer.standartKernel.Bind<FillColorParams>().ToMethod(context => GetFillColor());
            DIContainer.standartKernel.Bind<SidesCountParams>().ToMethod(context => GetSidesCount());
            DIContainer.standartKernel.Bind<RadiusParams>().ToMethod(context => GetRadius());
        }

        private void InitializeCustom()
        {
            var commands = DIContainer.standartKernel.GetAll<IDrawFigureCommand>().ToArray();
            this.CommandComboBox.SetNamedItems(commands
                .Select(command => new NamedItem<IDrawFigureCommand>(command, command.Name)));

            this.FillColorComboBox.SetNamedItems(Color4Extension.GetColorWithName());
            HideInputControls();

            paramPairsControl[typeof(RotateParams)] = new[] { RotateStackPanel };
            paramPairsControl[typeof(TranslateParams)] = new[] { TranslateStackPanel };
            paramPairsControl[typeof(FillColorParams)] = new[] { FillColorComboBox };
            paramPairsControl[typeof(SidesCountParams)] = new[] { FigureFaceStackPanel };
            paramPairsControl[typeof(RadiusParams)] = new[] { RadiusStackPanel }; 
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            game.Commands.AddRange(new IMyCommand[] {
                new SetClearColor() { Color = Color4.DimGray },
                new Fill() });
            gameControl.InvalidateVisual();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            var drawFigureCommand = GetDrawFigureType();
            if (drawFigureCommand != null)
            {
                game.ClearColor = GetClearColor();
                game.Commands.Add(DIContainer.standartKernel.Get<Rotate>());
                game.Commands.Add(DIContainer.standartKernel.Get<Translate>());
                game.Commands.Add((IDrawFigureCommand)DIContainer.standartKernel.Get(drawFigureCommand.GetType()));
                game.Commands.Add(new CustomCommand(() =>
                {
                    GL.LoadMatrix(ref game.Final);
                }));
            }
            gameControl.InvalidateVisual();
        }

        private void OnSelectionCommandChange(object sender, SelectionChangedEventArgs e)
        {
            var command = this.CommandComboBox.GetSelected<IDrawFigureCommand>()?.Item;
            if (command == null)
            {
                return;
            }

            HideInputControls();
            var requiredParameters = command.GetType().GetConstructors().First().GetParameters()
                .Select(p => p.ParameterType).ToArray();
            foreach (var parameter in requiredParameters)
            {
                foreach (var inputControl in paramPairsControl[parameter])
                {
                    inputControl.Visibility = Visibility.Visible;
                }
            }
        }

        private void HideInputControls()
        {
            foreach (var control in this.ControlsStackPanel.Children.OfType<FrameworkElement>())
            {
                control.Visibility = Visibility.Collapsed;
            }
            this.CommandComboBox.Visibility = Visibility.Visible;
            this.ButtonsStackPanel.Visibility = Visibility.Visible;
        }

        private IDrawFigureCommand? GetDrawFigureType()
            => CommandComboBox.GetSelected<IDrawFigureCommand>()?.Item;


        private FillColorParams GetFillColor()
           => new FillColorParams(FillColorComboBox.GetSelected<Color4>()?.Item ?? new Color4());

        private Color4 GetClearColor() => this.ClearColorComboBox.GetSelected<Color4>()?.Item ?? Color4.Black;


        private TranslateParams GetCenter()
        {
            return new TranslateParams(new Vector3(
                XTranslateNumeric.GetFloatOrDefault(),
                YTranslateNumeric.GetFloatOrDefault(),
                ZTranslateNumeric.GetFloatOrDefault()));
        }

        private RadiusParams GetRadius()
            => new RadiusParams(Radius1Numeric.GetFloatOrDefault());

        private SidesCountParams GetSidesCount()
            => new SidesCountParams(SidesCountNumeric.Value ?? 0);

        private RotateParams GetRotate()
        {
            return new RotateParams(AngleRotateNumeric.GetFloatOrDefault(), new Vector3(
                    XRotateNumeric.GetFloatOrDefault(),
                    YRotateNumeric.GetFloatOrDefault(),
                    ZRotateNumeric.GetFloatOrDefault()
                    ));
        }
    }
}
