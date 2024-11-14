namespace MassBattle.Core.Entities.Database
{
    public interface IId
    {
        string Id { get; }

        void GenerateId(int index);
    }
}
