using System.ComponentModel.DataAnnotations;

public class ClientDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

