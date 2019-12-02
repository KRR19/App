namespace App.BussinesLogicLayer.Models.Users
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
    }
}
