using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        public Task<string> Delete(OrderItem item);
    }
}
