using Binance.Spot;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GoldenGui.Hive.Functions;

public class Function1(ILoggerFactory loggerFactory)
{
	private readonly ILogger _logger = loggerFactory.CreateLogger<Function1>();

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
