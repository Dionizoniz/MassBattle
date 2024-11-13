using System.Collections.Generic;

namespace MassBattle.Core.Entities.Database
{
    public interface IDatabase<T> where T : IId
    {
        List<string> FindAllElementIds();
        T TryFindElementBy(int index);
        T TryFindNextElementFor(int index);
        T TryFindElementBy(string id);
        T FindDefaultElement();
    }
}
