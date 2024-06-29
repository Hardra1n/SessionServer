namespace Entities.Sessions;

public record SessionForCreationDto(string Name, int NumberOfUsers, int SearchingPeriodSeconds);