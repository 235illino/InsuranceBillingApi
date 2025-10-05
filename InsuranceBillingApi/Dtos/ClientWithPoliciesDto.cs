namespace InsuranceBillingApi.Dtos
{
    public class ClientWithPoliciesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<InsurancePolicyDto> Policies { get; set; }
    }
}
