using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccessLayer.Repository.Interfaces;


namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        public void Create(string Name);
    }
}
