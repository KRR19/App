﻿using System.Collections.Generic;

namespace App.BussinesLogicLayer
{
    public class BaseResponseModel
    {
        public List<string> Message { get; set; }
        public bool Validation { get; set; }
    }
}
