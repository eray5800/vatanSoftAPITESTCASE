using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using vatansoftAPITESTCASE.HelperClasses;

namespace vatansoftAPITESTCASE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogAPIController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl;
        private const int ByteLimit = 1050000;

        public DogAPIController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiUrl = configuration["DogApiSettings:ApiUrl"];
        }

        [HttpGet("check-api-sizes")]
        public async Task<IActionResult> CheckApiSizes()
        {
            int greaterCount = 0;
            int smallerCount = 0;

            var client = _httpClientFactory.CreateClient();

            for (int i = 0; i < 100; i++)
            {
                var response = await client.GetAsync(_apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var apiResult = JsonSerializer.Deserialize<DogApiResponseDTO>(jsonString);

                    if (apiResult.FileSizeBytes > ByteLimit)
                    {
                        greaterCount++;
                    }
                    else
                    {
                        smallerCount++;
                    }
                }
            }

            return Ok(new
            {
                GreaterThanLimit = greaterCount,
                SmallerThanLimit = smallerCount
            });
        }
    }
}
