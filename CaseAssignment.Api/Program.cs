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
		var validationErrors = validationResult.Errors
			.Select(e => new {Error = e.ErrorMessage, e.PropertyName})
			.ToList();
		
		return Results.ValidationProblem(errors: validationErrors.ToDictionary(x => x.PropertyName, y => new[] {y.Error}));
	}

	var score = calculator.CalculateScore(request.Measurements);
	return Results.Ok(new { Score = score });
});

app.Run();
