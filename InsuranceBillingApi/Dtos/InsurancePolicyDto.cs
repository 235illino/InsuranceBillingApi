using System.ComponentModel.DataAnnotations;

public class InsurancePolicyDto
{
    public int Id { get; set; }

    [Required]
    public string PolicyNumber { get; set; }

    [Required]
    public string CoverageType { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Premium { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int ClientId { get; set; }
}
