using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DrinkCategories.Dtos
{
    public class DeletedDrinkCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MenuId { get; set; }
        public ICollection<DeletedDrinkByDeletionOfCategoryDto> Drinks { get; set; }

    }

    public class DeletedDrinkByDeletionOfCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Calories { get; set; }
        public int Volume { get; set; }
        public string Explanation { get; set; }

    }
}
