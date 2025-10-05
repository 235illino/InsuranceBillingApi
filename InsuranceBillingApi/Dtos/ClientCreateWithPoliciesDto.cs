using System.ComponentModel.DataAnnotations;

namespace InsuranceBillingApi.Dtos
{
    public class ClientCreateWithPoliciesDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<InsurancePolicyDto> Policies { get; set; } = new();
    }
}
