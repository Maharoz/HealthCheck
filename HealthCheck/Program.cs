using HealthCheck.Services.Health;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace HealthCheck
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddHealthChecks()
				.AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
				.AddCheck<APIHealthCheck>("AirportApiChecks",tags: new string[] { "custom" });
			builder.Services.AddHealthChecksUI().AddInMemoryStorage();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();
			app.MapHealthChecks("/health/custom", new HealthCheckOptions()
			{
				Predicate= reg => reg.Tags.Contains("custom"),
				ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse
			});

			app.MapHealthChecksUI();
			//app.UseHealthChecksUI(options =>
			//{
			//	options.UIPath = "/healthchecks-ui";
			//	options.ApiPath = "/health-ui-api";
			//});

			app.Run();
		}
	}
}