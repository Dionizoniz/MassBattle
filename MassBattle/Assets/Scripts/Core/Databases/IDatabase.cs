using System.Collections.Generic;
using MassBattle.Core.Entities;

namespace MassBattle.Core.Databases
{
    public interface IDatabase<T> where T : IDescriptorId
    {
        IEnumerable<string> FindAllDescriptorIds();
        T TryFindElementBy(int index);
        T TryFindNextElementFor(int index);
        T TryFindElementBy(string id);
        T FindDefaultElement();
    }
}
