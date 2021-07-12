namespace Wallet.Api.Data
{
    // TODO: Fluent Validator
    // TODO: Base entity
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
    }
}