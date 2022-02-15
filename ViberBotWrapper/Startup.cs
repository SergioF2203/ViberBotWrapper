using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViberBotWebApp.ActionsProvider;
using Models;
using static System.Net.Mime.MediaTypeNames;

namespace ViberBotWrapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddMessageService();
            services.AddStateManagerService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applifitime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var _httpClientFactory = app.ApplicationServices.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;

            applifitime.ApplicationStarted.Register(OnStartedApp(_httpClientFactory).Wait);
        }

        private async Task OnStartedApp(IHttpClientFactory httpClientFactory)
        {
            var url = Configuration.GetSection("URL").Value;
            WebHookModel webhookbody = new()
            {
                url = url,
                event_types = new()
                {
                    "delivered",
                    "seen",
                    "failed",
                    "subscribed",
                    "unsubscribed",
                    "conversation_started"
                },
                send_name = true,
                send_photo = true
            };

            JsonSerializerOptions options = new() { IgnoreNullValues = true };

            var messageJSON = new StringContent(JsonSerializer.Serialize(webhookbody, options), System.Text.Encoding.UTF8, Application.Json);


            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.CacheControl = new() { NoCache = true };

            var token = Configuration.GetSection("AuthenticationToken").Value;

            httpClient.DefaultRequestHeaders.Add("X-Viber-Auth-Token", token);

            var result = await httpClient.PostAsync("https://chatapi.viber.com/pa/set_webhook", messageJSON);
        }
    }
}
