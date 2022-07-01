using ShortageSimulation.ShortageEF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortageSimulation.Models
{
    public interface ICustomerServices
    {
        public Task<List<ShortageViewModel>> GetAllFinishedGoods();
    }
}
