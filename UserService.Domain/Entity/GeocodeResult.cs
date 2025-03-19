using Newtonsoft.Json;

namespace PlotLink.DAL.Entities
{
    public class GeocodeResult
    {
        
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }
        
    }
}
