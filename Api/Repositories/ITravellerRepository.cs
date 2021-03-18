using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ITravellerRepository
    {
        List<Traveller> GetStrings();
        Task Delete();
        List<string> Create(List<string> cards);
        
    }
}