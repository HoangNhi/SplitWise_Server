using Microsoft.AspNetCore.Identity;

namespace BE_WiseWallet.Auth
{
    public class AuthRespone
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public IdentityResult? Result { get; set; }
    }
}
