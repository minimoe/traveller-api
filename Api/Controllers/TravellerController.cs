using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    public class TravellerController : ControllerBase
    {
        private readonly TravellerContext travellerContext;

        public TravellerController(TravellerContext travellerContext)
        {
            this.travellerContext = travellerContext;
        }

        [HttpPost("api/traveller")]
        public ActionResult<List<string>> Post([FromBody] List<string> list)
        {
            Dictionary<string, int> mapped = new Dictionary<string, int>();
            foreach(string card in list)
            {
                var pair = card.Replace(" ", "").Split("-");
                travellerContext.Travellers.Add(new Traveller{
                    From = pair[0],
                    To = pair[1],
                    Card = card
                });
            }

            travellerContext.SaveChanges();
            return Created("api/traveller", list);
        }


        [HttpGet("api/cards")]
        public ActionResult<List<string>> GetCards()
        {
            List<string> cards = new List<string>();
            List<Traveller> sorted = new List<Traveller>();
            Dictionary<string, string> map = new Dictionary<string, string>();

            var items = travellerContext.Travellers;
            foreach(Traveller traveller in items)
            {
                map[traveller.From] = traveller.To;
            }

            // Reverse map is used to identify the starting point.
            Dictionary<string, string> reverseMap = new Dictionary<string, string>();
            foreach(KeyValuePair<string, string> pair in map)
            {
                string _from = pair.Key;
                string _to = pair.Value;

                reverseMap[_to] = _from;
            }

            string start = null;
            foreach(KeyValuePair<string, string> pair in map)
            {
                // If key not here, then pair.Key from dataset is the starting point.
                if(!reverseMap.ContainsKey(pair.Key))
                {
                    string card = pair.Key + "-" + pair.Value;
                    sorted.Add(new Traveller{
                        From = pair.Key,
                        To = pair.Value,
                        Card = card
                    });

                    start = pair.Key;
                    cards.Add(card);
                    break;
                }
            }

            string end = map[start];
            // Use start destination along with hashtables to map out itinerary.
            while (sorted.Count != map.Count)
            {
                start = end;
                end = map[end];
                cards.Add(start + "-" + end);

                sorted.Add(new Traveller{
                    From = start,
                    To = end,
                    Card = start + "-" + end
                });
            }

            return Created("api/cards", cards);
        }

    }
}