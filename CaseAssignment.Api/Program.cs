using System.Reflection;
using CaseAssignment.Api;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<INewsScoreCalculator, NewsScoreCalculator>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapPost("/measurements", (AddMeasurementRequest request, IValidator<AddMeasurementRequest> validator, INewsScoreCalculator calculator) =>
{
	var validationResult = validator.Validate(request);
	if (!validationResult.IsValid)
	{
		return Results.ValidationProblem(validationResult.Errors.ToDictionary(
			x => x.PropertyName,
			x => new[] { x.ErrorMessage }));
	}

	var score = calculator.CalculateScore(request.Measurements);
	return Results.Ok(new { Score = score });
});

app.Run();
