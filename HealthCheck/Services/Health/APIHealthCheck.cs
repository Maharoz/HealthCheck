using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace HealthCheck.Services.Health
{
	public class APIHealthCheck : IHealthCheck
	{
		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
		{
			var url = "https://airport-info.p.rapidapi.com/airport";
			var client = new RestClient();
			var request = new RestRequest(url,Method.Get);
			request.AddHeader("X-RapidAPI-Key", "3e00994523msh7d316919fbba66ap1c7386jsn84b5700f44d6");
			request.AddHeader("X-RapidAPI-Host", "airport-info.p.rapidapi.com");
			var response = client.Execute(request);
			if (response.IsSuccessful)
			{
				return Task.FromResult(HealthCheckResult.Healthy());
			}
			else
			{
				return Task.FromResult(HealthCheckResult.Unhealthy());
			}
		}
	}
}
