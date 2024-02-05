using CaseAssignment.Api;
using FluentAssertions;

namespace CaseAssignment.Tests;

public class NewsScoreCalculatorTests
{
	[Fact]
	public void CalculateScore_Returns_Correct_Score_Given_List_Of_Measurements()
	{
		var calculator = new NewsScoreCalculator();
		
		var measurements = new List<Measurement>
		{
			new(MeasurementType.TEMP, 39),
			new(MeasurementType.HR, 43),
			new(MeasurementType.RR, 19)
		};

		calculator.CalculateScore(measurements).Should().Be(2);
	}
}