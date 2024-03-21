using Binance.Spot;
using GoldenGui.Hive.Core.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
		string serverTime = market.CheckServerTime().Result.ToString();
		_logger.LogInformation("Server time: {ServerTime}", serverTime);

		if (myTimer.ScheduleStatus is not null)
		{
			_logger.LogInformation("Next timer schedule at: {Next}", myTimer.ScheduleStatus.Next);
		}
	}
}
