﻿using System;

namespace App.DataAccessLayer.Entities
{
    public class Cover : Base.Base
    {
        public string Base64Image { get; set; }
        public Guid? PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
    }
}
