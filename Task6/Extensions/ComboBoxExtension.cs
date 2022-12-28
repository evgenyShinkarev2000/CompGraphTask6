using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

public static class ComboBoxExtension
{
    public static NamedItem<T>? GetSelected<T>(this ComboBox comboBox)
    {
        if (comboBox?.SelectedItem is ComboBoxItem comboBoxSelectedItem)
        {
            return comboBoxSelectedItem.DataContext is T 
                ? new NamedItem<T>((T)comboBoxSelectedItem.DataContext, comboBoxSelectedItem.Content?.ToString() ?? "")
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
