using Ninject;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        public MainWindow()
        {
            gameControl = game.OpenTkControl;
            InitializeComponent();
            InitializeCustom();
            game.Show();
        }

        private void InitializeCustom()
        {
            this.CommandComboBox.SetNamedItems(
                DIContainer.standartKernel.GetAll<IComboBoxCommand>()
                .Select(command => new NamedItem<IComboBoxCommand>(command, command.CommandName)));

            this.FillColorComboBox.SetNamedItems(Color4Extension.GetColorWithName());
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            game.PaintCommands.Add(new Cleaner());
            gameControl.InvalidateVisual();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            var command = this.CommandComboBox.GetSelected<IComboBoxCommand>()?.Item;
            if (command != null)
            {
                var fillColor = this.FillColorComboBox.GetSelected<Color4>()?.Item;
                if (fillColor != null)
                {
                    command.FillColor = (Color4)fillColor;
                    game.PaintCommands.Add(command);
                }
            }
            gameControl.InvalidateVisual();
        }
    }
}

public static class ComboBoxExtension
{
    public static NamedItem<T>? GetSelected<T>(this ComboBox comboBox)
    {
        if (comboBox?.SelectedItem is ComboBoxItem comboBoxSelectedItem)
        {
            return comboBoxSelectedItem.DataContext is T 
                ? new NamedItem<T>((T)comboBoxSelectedItem.DataContext, comboBoxSelectedItem.Content.ToString())
                : null;
        }

        return null;
    }

    public static void SetNamedItems<T>(this ComboBox comboBox, IEnumerable<NamedItem<T>> namedItems)
    {
        foreach(var namedItem in namedItems)
        {
            var comboBoxItem = new ComboBoxItem();
            comboBoxItem.DataContext = namedItem.Item;
            comboBoxItem.Content = namedItem.Name;
            comboBox.Items.Add(comboBoxItem);
        }
    }
}

public static class Color4Extension
{
    private static NamedItem<Color4>[]? colorsCash = null;
    public static NamedItem<Color4>[] GetColorWithName()
    {
        if (colorsCash != null)
        {
            return colorsCash.Select(c => c).ToArray();
        }

        colorsCash = typeof(Color4).GetProperties(BindingFlags.Static | BindingFlags.Public)
            .Select(propInfo =>
            {
#pragma warning disable CS8605 // Unboxing a possibly null value.
                return new NamedItem<Color4>((Color4)propInfo.GetValue(null), propInfo.Name);
#pragma warning restore CS8605 // Unboxing a possibly null value.
            }).ToArray();

        return colorsCash.Select(c => c).ToArray();
    }
}

public class NamedItem<T>
{
    public readonly T Item;
    public readonly string Name;

    public NamedItem(T item, string name)
    {
        Item = item;
        Name = name;
    }
}
