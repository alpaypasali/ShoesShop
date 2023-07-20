﻿using AutoMapper;
using ShoeShop.Businness.Abstract;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.Businness.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public ICollection<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _categoryRepository.GetById(id);
            var request = _mapper.Map<CategoryDto>(category);
            return request;
        }

        public void UpdateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _categoryRepository.Update(category);
        }

        public bool isExist(int id)
        {
            return _categoryRepository.IsExists(id);
        }

        public int AddCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            return _categoryRepository.Add(category);
        }

        public void DeleteCategoryById(int categoryDtoId)
        {
            _categoryRepository.DeleteById(categoryDtoId);
        }
    }
}
