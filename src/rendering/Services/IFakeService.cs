using System;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IFakeService
    {
        DateTime FakeDate { get; }
        Task<int> GetFakeValueAsync();
    }
}
