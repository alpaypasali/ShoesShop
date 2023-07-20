using ShoeShop.DataAccess.Abstract;
using ShoeShop.DataAccess.Concrete.Data;
using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ShoeShop.DataAccess.Concrete.Repository
{
    public class EfStockRepository : IStockRepository
    {
        private readonly ShoeShopDbContext _dbContext;

        public EfStockRepository(ShoeShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<Stock> GetAll()
        {
            return _dbContext.Stocks.ToList();
        }

        public Stock GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Add(Stock entity)
        {
            _dbContext.Stocks.Add(entity);
            _dbContext.SaveChanges();
            return entity.ID;
        }

        public int Update(Stock entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<StockDto> GetSizes(int id)
        {
            var sizeList = new List<StockDto>();
            var innerJoin = from stk in _dbContext.Stocks
                            where stk.ProductID == id
                            select new
                            {
                                Size = stk.Size,
                                ID = stk.ID,
                            };
            foreach (var item in innerJoin)
            {
                var size = new StockDto
                {
                    SizeName = item.Size,
                    StockID = item.ID,
                };
                sizeList.Add(size);
            }
            return sizeList;
        }

    }
}
