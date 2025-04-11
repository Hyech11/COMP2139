using System.ComponentModel.DataAnnotations;

public class EditProfileViewModel
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public string ContactInfo { get; set; }

    public string? PreferredCategory { get; set; }
}