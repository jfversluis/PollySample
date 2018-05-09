using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using PollySample.Models;

namespace PollySample.Services
{
	public class BackendService
	{
		private readonly HttpClient _httpClient;

		private const string ApiBaseUrl = "https://airplanemodeproof-api.azurewebsites.net/api/";

		public BackendService()
		{
			_httpClient = new HttpClient
			{
				BaseAddress = new Uri(ApiBaseUrl)
			};
		}

		public async Task<IEnumerable<Superhero>> GetSuperheroes()
		{
			var resultJson = await _httpClient.GetStringAsync("superhero");

			return JsonConvert.DeserializeObject<IEnumerable<Superhero>>(resultJson);
		}

		public async Task<IEnumerable<Comic>> GetComicsForSuperhero(int id)
		{
			return await Policy
			.Handle<HttpRequestException>(ex => !ex.Message.ToLower().Contains("404"))
			.WaitAndRetryAsync
			(
				retryCount: 3,
				sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
				onRetry: (ex, time) =>
				{
					Console.WriteLine($"Something went wrong: {ex.Message}, retrying...");
				}
			)
			.ExecuteAsync(async () =>
			{
				Console.WriteLine($"Trying to fetch remote data...");

				var resultJson = await _httpClient.GetStringAsync($"comic/{id}");

				return JsonConvert.DeserializeObject<IEnumerable<Comic>>(resultJson);
			});
		}
	}
}