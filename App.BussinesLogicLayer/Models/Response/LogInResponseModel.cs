namespace App.BussinesLogicLayer.Models.Response
{
    public class LogInResponseModel : BaseResponseModel
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string Role { get; set; }
        public string User { get; set; }

    }
}
