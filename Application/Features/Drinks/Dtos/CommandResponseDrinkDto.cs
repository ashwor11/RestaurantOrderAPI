using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Dtos
{
    public class CommandResponseDrinkDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public int Calories { get; set; }
    }
}
