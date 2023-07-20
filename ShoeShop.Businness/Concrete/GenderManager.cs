using ShoeShop.Businness.Abstract;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.Businness.Concrete
{
    public class GenderManager : IGenderService
    {
        private readonly IGenderRepository _genderRepository;

        public GenderManager(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        public ICollection<Gender> GetAllGenders()
        {
            return _genderRepository.GetAll();
        }

        public Gender GetGender(int id)
        {
            return _genderRepository.GetById(id);
        }
    }
}
