namespace Persistence.EntityFramework;

/// <summary>
/// Used to autogather all db models for EF Core
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DatabaseModelAttribute : Attribute
{
}