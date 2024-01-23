using System.ComponentModel.DataAnnotations;

namespace EDDIESCARDEALAERSHIP.Models
{
    public class CheckoutViewModel
    {
        // Properties for credit/debit card information
        [Required(ErrorMessage = "Card number is required")]
        public string? CardNumber { get; set; }

        [Required(ErrorMessage = "Cardholder name is required")]
        public string? CardholderName { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        public string? ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        public string? CVV { get; set; }

        // Properties for banking information
        [Required(ErrorMessage = "Bank name is required")]
        public string? BankName { get; set; }

        [Required(ErrorMessage = "Account number is required")]
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "Routing number is required")]
        public string? RoutingNumber { get; set; }

        // Properties for loan application
        [Required(ErrorMessage = "Applicant name is required")]
        public string? ApplicantName { get; set; }

        [Required(ErrorMessage = "Social security number is required")]
        public string? SocialSecurityNumber { get; set; }

        [Required(ErrorMessage = "Loan amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid loan amount")]
        public decimal LoanAmount { get; set; }
    }
}
