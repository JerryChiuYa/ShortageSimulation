using ShortageSimulation.ShortageCT;
using ShortageSimulation.ShortageEF;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ShortageSimulation.Models
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ShortageContext _context;

        public CustomerServices(ShortageContext context)
        {
            _context = context;
        }

        public async Task<List<FinishedGoods>> GetAllFgGoods()
        {
            var entity=await _context.FinishedGoods.ToListAsync();
            return entity;
        }

        public async Task<List<ShortageViewModel>> GetAllFgNames()
        {
            var entity = (from c in _context.FinishedGoods select c.Fname).Distinct();
            var nameList = await entity.ToListAsync();
            var result=new List<ShortageViewModel>();
            foreach (var name in nameList)
            {
                var ef = new ShortageViewModel();
                ef.FGName = name;
                result.Add(ef);
            }
            return result;

        }

        public async Task<List<Materials>> GetAllMaterialStocks()
        {
            var entity=await _context.Materials.ToListAsync();
            return entity;
        }
    }
}
