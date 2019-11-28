namespace App.BussinesLogicLayer.Models.Response
{
    public class LogInResponseModel : BaseResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public string User { get; set; }

    }
}
