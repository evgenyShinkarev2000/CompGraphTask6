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
