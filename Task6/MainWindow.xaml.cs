using Ninject;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task6.Commands;
using Task6.Extensions;
using Task6.Interfaces;
using Task6.Painters;

namespace Task6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly GLWindow game = new GLWindow();
        public readonly GLWpfControl gameControl;
        private readonly CommandInitializer commandInitializer;
        private readonly InitCommandSettingProvider initCommandSettingProvider;
        public MainWindow()
        {
            gameControl = game.OpenTkControl;
            InitializeComponent();
            InitializeCustom();
            initCommandSettingProvider = new InitCommandSettingProvider(this);
            commandInitializer = new CommandInitializer(initCommandSettingProvider);
            game.Show();
        }

        private void InitializeCustom()
        {
            this.CommandComboBox.SetNamedItems(
                DIContainer.standartKernel.GetAll<IDrawFigureCommand>()
                .Select(command => new NamedItem<IDrawFigureCommand>(command, command.Name)));

            this.FillColorComboBox.SetNamedItems(Color4Extension.GetColorWithName());
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
            var drawFigureCommand = initCommandSettingProvider.GetDrawFigureCommand();
            if (drawFigureCommand != null)
            {
                game.Commands.AddRange(drawFigureCommand.Init(commandInitializer));
            }
            gameControl.InvalidateVisual();
        }
    }

    public class CommandInitializer
    {
        private readonly InitCommandSettingProvider settingProvider;
        public CommandInitializer(InitCommandSettingProvider settingProvider)
        {
            this.settingProvider = settingProvider;
        }

        public IEnumerable<IMyCommand> Visit(Fill clean)
        {
            return Visit(new SetClearColor()).Concat(new[] { clean });
        }

        public IEnumerable<IMyCommand> Visit(DrawArbitaryPoligon drawArbitaryPoligon)
        {
            // do
            yield return drawArbitaryPoligon;
        }

        public IEnumerable<IMyCommand> Visit(DrawRegularPoligon drawRegularPoligon)
        {
            //do
            yield return drawRegularPoligon;
        }

        public IEnumerable<IMyCommand> Visit(Rotate rotate)
        {
            // do
            yield return rotate;
        }
        public IEnumerable<IMyCommand> Visit(SetClearColor setClearColor)
        {
            var clearColor = settingProvider.GetFillColor(); // лень делать combobox для ClearColor
            if (clearColor != null)
            {
                setClearColor.Color = (Color4)clearColor;
                yield return setClearColor;
            }
        }

        public IEnumerable<IMyCommand> Visit(SetFillColor setFillColor)
        {
            var fillColor = settingProvider.GetFillColor();
            if (fillColor != null)
            {
                setFillColor.Color = (Color4)fillColor;
                yield return setFillColor;
            }
        }

        public IEnumerable<IMyCommand> Visit(Translate translate)
        {
            // do
            yield return translate;
        }
    }

    public class InitCommandSettingProvider
    {
        private readonly MainWindow mainWindow;
        public InitCommandSettingProvider(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public IDrawFigureCommand? GetDrawFigureCommand()
            => mainWindow.CommandComboBox.GetSelected<IDrawFigureCommand>()?.Item;


        public Color4? GetFillColor()
           => mainWindow.FillColorComboBox.GetSelected<Color4>()?.Item;

        public Color4? GetClearColor() => Color4.DimGray;


        public Vector3 GetCenter()
        {
            return new Vector3(
                mainWindow.XNumeric.GetFloatOrDefault(),
                mainWindow.YNumeric.GetFloatOrDefault(),
                mainWindow.ZNumeric.GetFloatOrDefault());
        }
    }
}
