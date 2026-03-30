using System.ComponentModel.DataAnnotations;

namespace expense_tracker.DTO.Requests;

public class CreateUserRequest
{
    [Required]
    [Length(3, 50)]
    [RegularExpression("^[a-zA-Z0-9!@#$%^&*?]+$")]
    public required string Username {get; set;}

    [Required]
    [Length(3, 50)]
    [RegularExpression("^[a-zA-Z0-9!@#$%^&*?]+$")]
    public required string Password {get; set;}

    [Required]
    [EmailAddress]
    public required string Email {get; set;}
}
