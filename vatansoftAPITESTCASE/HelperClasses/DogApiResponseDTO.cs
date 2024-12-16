using System.Text.Json.Serialization;

namespace vatansoftAPITESTCASE.HelperClasses
{
    public class DogApiResponseDTO
    {
        [JsonPropertyName("fileSizeBytes")]
        public int FileSizeBytes { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

}
