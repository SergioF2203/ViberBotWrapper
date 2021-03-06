using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ViberBotWebApp.ActionsProvider;
using Utils;
using ViberBotWebApp.DAL;
using Models.CallbackData;

namespace ViberBotWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViberBotController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly MessageActionProvider _messageAction;

        public ViberBotController(IHttpClientFactory httpClientFactory, IConfiguration configuration, MessageActionProvider messageAction)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _messageAction = messageAction;
        }


        [HttpGet]
        public IActionResult WelcomeMessageGet()
        {
            return Ok("The Bot has started ...");
        }

        [HttpPost]
        public async Task<IActionResult> RecieveCallbackEvent([FromBody] BaseIncomingMessage data)
        {   
            if (!data.Subscribed && data.User is not null)
                data.User.subscribed = false;

            var ev = data.Event;
            await HelperActions.WriteToFile("viber_bot_log", JsonSerializer.Serialize(data));

            var databaseAction = new DatabaseController(_configuration);

            switch (ev)
            {
                case "message":
                    if (data.User is not null)
                        await databaseAction.SaveNewUserToDb(data.User);

                    await _messageAction.SetResponseMessage(data);
                    return Ok();

                case "conversation_started":
                    await databaseAction.SaveNewUserToDb(data.User);
                    await _messageAction.SetResponseMessage(new() { 
                        Sender = new() {id = "IQCp+qDMhxCX8DMF33/Osg==" }, 
                        Message = new() { Text = "user_added" }
                    });

                    return Ok(_messageAction.SetWelcomeMessage(data.User.name));

                case "subscribed":
                    return Ok();

                case "delivered":
                case "seen":
                    return Ok(); ;

                case "set_webhook":
                    return Ok();

                default:
                    return Ok();
            }
        }
    }
}
