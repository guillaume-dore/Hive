using GoldenGui.Hive.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoldenGui.Hive.Functions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExchangeOptions(this IServiceCollection services)
	{
		services.AddOptions<BinanceOptions>()
			.BindConfiguration(BinanceOptions.Section)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		return services;
	}
}
