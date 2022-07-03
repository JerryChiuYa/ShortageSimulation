using System.Collections.Generic;

namespace ShortageSimulation.Models
{
    public class ResultViewModel
    {
        public List<SalesOrder> orders { get; set; }
        public List<ShortageViewModel> shortages { get; set; }
    }
}
