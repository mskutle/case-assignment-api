using System.Text.Json.Serialization;

namespace CaseAssignment.Api;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MeasurementType
{
	TEMP,
	HR,
	RR
}