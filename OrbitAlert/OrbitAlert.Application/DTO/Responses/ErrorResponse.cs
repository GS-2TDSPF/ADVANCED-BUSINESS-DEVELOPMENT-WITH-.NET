namespace OrbitAlert.Application.DTO.Responses;

public record ErrorResponse(string Message)
{
    public static ErrorResponse FromMessage(string message) => new(message);
}
