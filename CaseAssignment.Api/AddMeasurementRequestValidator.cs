using FluentValidation;

namespace CaseAssignment.Api;

public class AddMeasurementRequestValidator : AbstractValidator<AddMeasurementRequest>
{
	public AddMeasurementRequestValidator()
	{
		RuleFor(x => x).NotNull();
		RuleFor(x => x.Measurements)
			.NotEmpty()
			.Must(x => x.Count() == 3).WithMessage($"You must provide 3 measurements")
			.Must(x => x.Any(m => m.Type == MeasurementType.TEMP))
			.Must(x => x.Any(m => m.Type == MeasurementType.HR))
			.Must(x => x.Any(m => m.Type == MeasurementType.RR))
			.WithMessage("You must provide measurements for temperature, heart rate and respiratory rate");
		RuleForEach(x => x.Measurements).SetValidator(new MeasurementValidator());
	}
}

public class MeasurementValidator : AbstractValidator<Measurement>
{
	public MeasurementValidator()
	{
		RuleFor(x => x.Type).NotNull().IsInEnum().WithMessage("You supplied an invalid measurement type.");
		RuleFor(x => x.Value)
			.NotEmpty()
			.GreaterThan(NewsScoreCalculator.TemperatureMinValue)
			.LessThanOrEqualTo(NewsScoreCalculator.TemperatureMaxValue)
			.When(m => m.Type == MeasurementType.TEMP)
			.WithName("Temperature");

		RuleFor(x => x.Value)
			.NotEmpty()
			.GreaterThan(NewsScoreCalculator.HeartRateMinValue)
			.LessThanOrEqualTo(NewsScoreCalculator.HeartRateMaxValue)
			.When(m => m.Type == MeasurementType.HR)
			.WithName("Heart rate");

		RuleFor(x => x.Value)
			.NotEmpty()
			.GreaterThan(NewsScoreCalculator.RespiratoryRateMinValue)
			.LessThanOrEqualTo(NewsScoreCalculator.RespiratoryRateMaxValue)
			.When(m => m.Type == MeasurementType.RR)
			.WithName("Respiratory rate");
	}
}