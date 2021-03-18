using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Traveller
    {
        [Key]
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Card { get; set; }
    }
}