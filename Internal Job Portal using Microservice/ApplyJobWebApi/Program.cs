
using ApplyJobLibrary.Repos;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json.Linq;
using ApplyJobLibrary.Models;

namespace ApplyJobWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ListenForIntegrationEvents();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IApplyJob, ApplyJobRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        private static void ListenForIntegrationEvents()
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                if (type == "JobPost.Add")
                {
                    JobPost jobpost = new JobPost() { PostId = data["PostId"].Value<int>() };

                    IApplyJob fs = new ApplyJobRepo();
                    fs.InsertJobPost(jobpost);
                }
            };
            channel.BasicConsume(queue: "JobPostqueue", autoAck: true, consumer: consumer);
        }
    }
}
