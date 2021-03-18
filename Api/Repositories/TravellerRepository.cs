using Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class TravellerRepository : ITravellerRepository
    {
        private readonly TravellerContext context;

        public TravellerRepository(TravellerContext context)
        {
            this.context = context;
        }

        public List<string> Create(List<string> travellers)
        {
            foreach(string travel in travellers)
            {
                Traveller t = new Traveller();
                var pair = travel.Replace(" ", "").Split("-");
                t.From = pair[0];
                t.To = pair[1];
                context.Travellers.Add(t);
            }

            context.SaveChanges();
            return travellers;
        }

        public List<Traveller> GetStrings()
        {
            return context.Travellers.ToList();
        }

        public async Task Delete()
        {
            var travellersToDelete = context.Travellers;
            context.Travellers.RemoveRange(travellersToDelete);
            await context.SaveChangesAsync();
        }
    }
}