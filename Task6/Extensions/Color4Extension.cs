using OpenTK.Mathematics;
using System.Linq;
using System.Reflection;

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
