using Domain.Abstractions;

namespace Domain.Models;

public class User : DomainModel
{
    public User(string name, bool isAdmin)
    {
        Name = name;
        IsAdmin = isAdmin;
    }

    public string Name { get; private set; }
    public bool IsAdmin { get; private set; }

    public User UpdateName(string newName)
    {
        Name = newName;
        return this;
    }

    public User MakeAdmin()
    {
        IsAdmin = true;
        return this;
    }

    public User DisableAdmin()
    {
        IsAdmin = false; // Todo: validate on change
        return this;
    }
}