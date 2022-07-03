using ShortageSimulation.ShortageEF;

namespace ShortageSimulation.Models
{
    public class ShortageViewModel
    {
        public  string FGName { get; set; }
        public int Quantity { get; set; }
        public string Mname { get; set; }
        public int Stocks { get; set; }
        public int Demands { get; set; }
        public int ShortageQty { get; set; }
        public string Remark { get; set; }
    }
}
