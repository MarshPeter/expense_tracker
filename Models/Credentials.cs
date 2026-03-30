namespace expense_tracker.Models;

public class Credentials
{
    public Guid Id {get; set;}
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string HashedPassword {get; set;} = string.Empty;
    public DateTime Created {get; set;}
}
