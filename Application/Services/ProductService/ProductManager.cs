using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Tables.Rules;
using Application.Helpers;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Domain.Entities.AbstractEntities;

namespace Application.Services.ProductService
{
    public class ProductManager : IProductService
    {
        public ProductManager(IProductRepository productRepository, TableBusinessRules tableBusinessRules)
        {
            _productRepository = productRepository;
            _tableBusinessRules = tableBusinessRules;
        }

        private readonly IProductRepository _productRepository;
        private readonly TableBusinessRules _tableBusinessRules;


        public void DoProductsBelongRestaurant(List<Product> products, int restaurantId)
        {
            products.ToList().ForEach(x=>{
                if (x.RestaurantId != restaurantId)
                {
                    throw new BusinessException("Products restaurantId is not equal to restaurantId");
                }
            });
        }

        public async Task<List<Product>> GetSpecifiedProducts(List<int> productIds)
        {
            return await _productRepository.GetProductsByIdsAsync(productIds);
        }

        public void AddProductsToCurrentOrder(List<Product> products, Table table, List<int> productIds)
        {
            Dictionary<int, int> counts = CreateDictionary(productIds);

            if (!DoesTableHaveCurrentOrder(table))
            {

                table.CurrentOrder = new Order();
            }

            foreach (Product product in products)
            {
                for (int i = 0; i < counts[product.Id]; i++)
                {
                    table.AddProductToOrder(product);
                }

            }
        }

        public void PayProductsPrice(Table table, List<int> toBePaidroductIds)
        {
            toBePaidroductIds.ForEach(productId =>
            {
                OrderedProduct? paidProduct = table.CurrentOrder.Products.Where(x => x.IsPaid == false).FirstOrDefault(x=>x.ProductId == productId);
                _tableBusinessRules.IsProductOrdered(paidProduct);
                    table.CurrentOrder.PaidPrice += paidProduct.Product.Price;
                    paidProduct.IsPaid = true;
                
            });
        }

        #region private methods


        private bool DoesTableHaveCurrentOrder(Table table)
        {

            return table.CurrentOrder != null;
        }

        private Dictionary<int, int> CreateDictionary(List<int> productIds)
        {
            Dictionary<int, int> productIdsToAdd = new Dictionary<int, int>();
            foreach (int productId in productIds)
            {

                if (productIdsToAdd.TryGetValue(productId, out int count))
                {
                    productIdsToAdd[productId] = count + 1;
                }
                else
                {
                    productIdsToAdd.Add(productId, 1);
                }
            }

            return productIdsToAdd;
        }
    }
#endregion
}
