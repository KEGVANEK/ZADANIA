using Microsoft.AspNetCore.Mvc;

namespace ErrorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestErrorsController : ControllerBase
    {
        private readonly ILogger<TestErrorsController> _logger;

        public TestErrorsController(ILogger<TestErrorsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Wywołano endpoint testowy błędów");

            throw new Exception("Testowy wyjątek");
        }
    }
}