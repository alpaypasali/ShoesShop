using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Abstract
{
    public interface IStockRepository : IRepository<Stock>
    {
        ICollection<StockDto> GetSizes(int id);

    }
}
