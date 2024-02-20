namespace Client.Contracts;

public abstract record RequestBase
{
    public abstract string GetActionRoute();
}
