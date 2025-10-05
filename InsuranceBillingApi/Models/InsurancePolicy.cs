namespace InsuranceBillingApi.Models
{
    public class InsurancePolicy
    {
        public int Id { get; set; }

        public string PolicyNumber { get; set; }       // Унікальний номер полісу
        public string CoverageType { get; set; }       // Тип покриття (наприклад, "Авто", "Медичне")
        public decimal Premium { get; set; }           // Вартість полісу

        public DateTime StartDate { get; set; }        // Початок дії
        public DateTime EndDate { get; set; }          // Закінчення дії

        // Зв’язок з клієнтом
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
