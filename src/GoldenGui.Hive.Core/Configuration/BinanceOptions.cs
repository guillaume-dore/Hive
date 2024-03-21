using System.ComponentModel.DataAnnotations;

namespace GoldenGui.Hive.Core.Configuration;

public class BinanceOptions
{
	public const string Section = "Binance";

	[Required]
	public string BaseUrl { get; set; } = null!;

	[Required]
	public string ApiKey { get; set; } = null!;

	[Required]
	public string ApiSecret { get; set; } = null!;
}
