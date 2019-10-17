using System.Collections.Generic;

namespace App.BussinesLogicLayer
{
    public class BaseResponseModel
    {
        public string Message { get; set; }
        public List<string> Error { get; set; }
    }
}
