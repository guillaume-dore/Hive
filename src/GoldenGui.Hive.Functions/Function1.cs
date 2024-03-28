using Binance.Spot;
using GoldenGui.Hive.Core.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;

namespace GoldenGui.Hive.Functions;

public class Function1(ILoggerFactory loggerFactory, IOptions<BinanceOptions> options)
{
	private readonly ILogger _logger = loggerFactory.CreateLogger<Function1>();

	private readonly BinanceOptions _binanceConfig = options.Value;

	[Function("Function1")]
	public void Run([TimerTrigger("2 */5 * * * *")] TimerInfo myTimer)
	{
		_logger.LogInformation("C# Timer trigger function executed at: {Now}", DateTime.Now);

		Market market = new();
		var serverTimestamp = JsonNode.Parse(market.CheckServerTime().Result)!.AsObject(); // get server time as timestamp
		DateTime serverTime = DateTimeOffset.FromUnixTimeMilliseconds((long)serverTimestamp["serverTime"]!).DateTime;
		_logger.LogInformation("Server time: {ServerTime}", serverTime);

		var result = market.SymbolPriceTicker(symbols: "[\"BTCEUR\",\"SOLEUR\",\"ETHEUR\"]").Result; // get all latest prices for symbols.

		_logger.LogInformation("Ticks: {ticks}", result);

		if (myTimer.ScheduleStatus is not null)
		{
			_logger.LogInformation("Next timer schedule at: {Next}", myTimer.ScheduleStatus.Next);
		}
	}
}
