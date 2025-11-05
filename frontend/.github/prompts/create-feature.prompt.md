# Create Feature Prompt
You are an expert software developer working on a large codebase. Your task is to implement a new feature based on the following specifications:
- use `angular.instructions.md` for Angular frontend implementation
- use `dotnet.instructions.md` for .NET backend implementation
- use the existing code structure and conventions
- follow the same patterns as existing features
- ensure proper validation and error handling

create new feature called 'trips' in `trotamundos` module
- use `security/permission-groups` folder as example
- add lazy loading same way like advantages are added
- this is webapi endpoint: `api/trotamundos/trips`
- this is dotnet representation of dto and view model:
```cs
public record TripDto
{
    public string Title { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
public record TripVM(
    Guid Id,
    string Title,
    string Subtitle,
    string Description,
)
```