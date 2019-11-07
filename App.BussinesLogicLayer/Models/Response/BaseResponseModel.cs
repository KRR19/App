using System.Collections.Generic;

namespace App.BussinesLogicLayer
{
    public class BaseResponseModel
    {
        public BaseResponseModel()
        {
            Message = new List<string>();
            IsValid = true;
        }
        public List<string> Message { get; set; }
        public bool IsValid { get; set; }
    }
}
