using MessageBrokerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MQController : ControllerBase
    {
        private readonly IMessageBroker _rabbitMQService;
        public MQController(IMessageBroker _rabbitMQ)
        {
            _rabbitMQService = _rabbitMQ;
        }

        [HttpGet]
        [Route("Produce")]
        public async Task<string> Produce(string msg)
        {
            _rabbitMQService.SendMessage(msg, "TestMQData");
            return "Done";
        }

        [HttpGet]
        [Route("Consume")]
        public async Task<string> Consume()
        {
            Action<string> action = (string msg) =>
            {
                Console.WriteLine($"Received message: {msg}");
                Thread.Sleep(10000);
            };
            _rabbitMQService.ReceiveMessage(action, "TestMQData");
            return "Done";
        }

        [HttpGet]
        [Route("Healthy")]
        public async Task<string> Healthy()
        {
            return "OK";
        }

        private static void HandleMessage(string message)
        {
            Console.WriteLine($"Received message: {message}");
        }
    }
}
