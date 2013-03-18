namespace nTestSwarm
{
    public interface INamedItem<T>
    {
        T Name { get; }
    }

    public interface INamedItem : INamedItem<string>
    {
    }

    public interface INamedEntity : INamedItem
    {
        long Id { get; }
    }
}