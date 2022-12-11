using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FindTheBuilder.Applications.Services.PaymentAppServices.Model;
using Microsoft.Extensions.Configuration;

namespace FindTheBuilder.Applications.Services.PaymentAppServices
{
    public class PaymentAppService : IPaymentAppService
	{
		private readonly IConfiguration _configuration;
		public PaymentAppService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<object> Post()
		{
			string startDate = DateTime.Now.AddDays(-3).ToString("yyyy-MM-ddT00:00:00-0700");
			string endDate = DateTime.Now.ToString("yyyy-MM-ddT23:59:59-0700");

			const SecurityProtocolType tls13 = (SecurityProtocolType)12288;
			ServicePointManager.SecurityProtocol = tls13 | SecurityProtocolType.Tls12;

			TokenJson Token = new TokenJson();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

				var clientId = _configuration["Paypal:Client"];
				var clientSecret = _configuration["Paypal:Secret"];
				var bytes = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

				var keyValues = new List<KeyValuePair<string, string>>();
				keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
				var responseMessage = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", new FormUrlEncodedContent(keyValues));
				var response = await responseMessage.Content.ReadAsStringAsync();
				Token = JsonConvert.DeserializeObject<TokenJson>(response);
			}
			if (Token != null)
			{
				var transactionHistoryUrl = "https://api-m.sandbox.paypal.com/v1/reporting/transactions?start_date=" + startDate + "&end_date=" + endDate
					+ "&fields=all&page_size=100&page=1";
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);

					var responseMessage = await client.GetAsync(transactionHistoryUrl);
					var response = await responseMessage.Content.ReadAsStringAsync();
					var Transaction = JsonConvert.DeserializeObject(response);

					return Transaction;
				}
			}

			return "Failed, Try Again";
		}
	}
}
