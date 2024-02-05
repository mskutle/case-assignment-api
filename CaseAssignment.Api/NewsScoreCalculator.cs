namespace CaseAssignment.Api;

public interface INewsScoreCalculator
{
	int CalculateScore(IEnumerable<Measurement> measurements);
}

public class NewsScoreCalculator : INewsScoreCalculator
{
	public const int TemperatureMinValue = 31;
	public const int TemperatureMaxValue = 42;

	public const int HeartRateMinValue = 25;
	public const int HeartRateMaxValue = 220;

	public const int RespiratoryRateMinValue = 3;
	public const int RespiratoryRateMaxValue = 60;
	
	public int CalculateScore(IEnumerable<Measurement> measurements) => measurements.Sum(CalculateScore);

	private static int CalculateScore(Measurement measurement)
	{
		return measurement.Type switch
		{
			MeasurementType.TEMP => GetTemperatureScore(measurement.Value),
			MeasurementType.HR => GetHeartRateScore(measurement.Value),
			MeasurementType.RR => GetRespiratoryRateScore(measurement.Value),

			_ => throw new ArgumentOutOfRangeException()
		};
	}

	private static int GetTemperatureScore(int temperature) => temperature switch
	{
		> TemperatureMinValue and <= 35 => 3,
		> 35 and <= 36 => 1,
		> 36 and <= 38 => 0,
		> 38 and <= 39 => 1,
		> 39 and <= TemperatureMaxValue => 2,
		_ => throw new ArgumentOutOfRangeException()
	};

	private static int GetHeartRateScore(int heartRate) => heartRate switch
	{
		> HeartRateMinValue and <= 40 => 3,
		> 40 and <= 50 => 1,
		> 50 and <= 90 => 0,
		> 90 and <= 110 => 1,
		> 110 and <= 130 => 2,
		> 130 and <= HeartRateMaxValue => 3,
		_ => throw new ArgumentOutOfRangeException()
	};

	private static int GetRespiratoryRateScore(int respiratoryRate) => respiratoryRate switch
	{
		> RespiratoryRateMinValue and <= 8 => 3,
		> 8 and <= 11 => 1,
		> 11 and <= 20 => 0,
		> 20 and <= 24 => 2,
		> 24 and <= RespiratoryRateMaxValue => 3,
		_ => throw new ArgumentOutOfRangeException()
	};
}