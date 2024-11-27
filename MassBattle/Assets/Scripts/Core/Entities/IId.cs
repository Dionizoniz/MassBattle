namespace MassBattle.Core.Entities
{
    public interface IId
    {
        string Id { get; }

        void GenerateId(int index);
    }
}
