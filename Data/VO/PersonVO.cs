using RestWithASPNET.Hypermedia;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Data.VO
{

    public class PersonVO : ISupportsHyperMedia
    { 
        public long Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; } 

        public string Address { get; set; } = string.Empty;
     
        public string Gender { get; set; } = string.Empty;

        public bool Enable { get; set; }

        public List<HyperMediaLink> Links { get ; set; } = new List<HyperMediaLink>();
    }
}
