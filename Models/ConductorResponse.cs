using Refit;
using System.Text.Json.Serialization;

namespace Locaserv.Bdv.App.Models
{
    public class ConductorResponse
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public interface IConductor
    {
        [Get("/conductors")]
        Task<IEnumerable<ConductorResponse>> GetAll();

        [Get("/conductors/{uuid}")]
        Task<ConductorResponse> GetById(Guid uuid);
    }
}