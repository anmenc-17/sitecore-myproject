using System;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class FakeService : IFakeService
    {
        public DateTime FakeDate => DateTime.Now;

        public async Task<int> GetFakeValueAsync()
        {
            return await Task.Run(() => new Random().Next());
        }
    }
}
