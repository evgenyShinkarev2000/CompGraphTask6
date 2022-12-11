using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace Task6.Extensions
{
    internal static class DoubleUpDownExtension
    {
        public static float GetFloatOrDefault(this DoubleUpDown doubleUpDown, float defaultValue = 0f)
        {
            return (float)(doubleUpDown.Value ?? defaultValue);
        }
    }
}
