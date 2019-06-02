using System;
using System.Collections.Generic;
using HelixAssignment.DAL;
using HelixAssignment.ViewModel;

namespace Helix.BAL
{
    public class ModelConverter
    {    
        public static HelixEvent ParseHelixEvent(HelixEventViewModel requestItem)
        {
            HelixEvent eventItem = new HelixEvent();

            eventItem.HelixEventId = requestItem.Id;
            eventItem.Timestamp = requestItem.Timestamp;

            return eventItem;
        }

        public static ICollection<Product> ParseProducts(ICollection<ProductViewModel> requestProducts)
        {
            List<Product> products = new List<Product>();

            if (requestProducts != null)
            {
                foreach (ProductViewModel requestProduct in requestProducts)
                {
                    Product product = new Product();
                    product.ProductId = requestProduct.Id;
                    product.Name = requestProduct.Name;

                    products.Add(product);
                }
            }

            return (ICollection<Product>)products;
        }

        public static ICollection<HelixEventProduct> ParseEventProducts(long eventId, ICollection<ProductViewModel> requestProducts)
        {
            List<HelixEventProduct> eventProducts = new List<HelixEventProduct>();

            if (requestProducts != null)
            {
                foreach (ProductViewModel requestProduct in requestProducts)
                {
                    HelixEventProduct eventProduct = new HelixEventProduct();
                    eventProduct.EventId = eventId;
                    eventProduct.ProductId = requestProduct.Id;
                    eventProduct.Quantity = requestProduct.Quantity;
                    eventProduct.Sale_amount = requestProduct.Sale_amount;

                    eventProducts.Add(eventProduct);
                }
            }

            return (ICollection<HelixEventProduct>)eventProducts;
        }
    }
}
