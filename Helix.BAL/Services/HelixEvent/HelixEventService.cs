using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helix.BAL;
using HelixAssignment.DAL;

using HelixAssignment.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace HelixAssignment.BAL
{
    public class HelixEventService : IHelixEventService
    {
        private readonly DbContext _dbContext;
        private readonly IHelixEventDbService _helixEventDbService;
        private readonly IProductDbService _productDbService;
        private readonly IHelixEventProductDbService _eventProductDbService;

        public HelixEventService(DbContext context, IHelixEventDbService helixEventDbService, IProductDbService productDbService, IHelixEventProductDbService eventProductDbService)
        {
            _dbContext = context;
            _helixEventDbService = helixEventDbService;
            _productDbService = productDbService;
            _eventProductDbService = eventProductDbService;
        }

        public async Task<ICollection<HelixEvent>> GetAllEvents()
        {
            ICollection<HelixEvent> list = null;

            try
            {
                list = await _helixEventDbService.GetAllEvents();
            }
            catch (Exception ex)
            {
                /** In real application, 
                 * Encapsulate the details in the layer-specific log files for further troubleshooting 
                 * and wrap it in user-friendly manner
                **/
                throw ex;
            }

            return list;
        }      

        public async Task<HelixEvent> GetEventById(long id)
        {
            HelixEvent item = null;

            try
            {
                item = await _helixEventDbService.GetById(id);
            }
            catch (Exception ex)
            {
                /** In real application, 
                 * Encapsulate the details in the layer-specific log files for further troubleshooting 
                 * and wrap it in user-friendly manner
                **/
                throw ex;
            }
            return item;
        }

        public async Task<bool> Update(HelixEventViewModel requestItem)
        {

            HelixEvent helixEvent = null;
            ICollection<Product> products = null;
            ICollection<HelixEventProduct> eventProducts;

            try
            {
                helixEvent =  ModelConverter.ParseHelixEvent(requestItem);
                products = ModelConverter.ParseProducts(requestItem.Products);
                eventProducts = ModelConverter.ParseEventProducts(requestItem.Id, requestItem.Products);
            }
            catch (Exception ex)
            {
                /** In real application, 
                 * Encapsulate the details in the layer-specific log files for further troubleshooting 
                 * and wrap it in user-friendly manner
                **/
                throw ex;
            }           

            try
            {
                /// Update Event
                bool isEventUpdateOk = await _helixEventDbService.Update(helixEvent);
                
                if (isEventUpdateOk)
                {
                    helixEvent = await _helixEventDbService.GetById(helixEvent.HelixEventId);

                    /// Update Products (if any new product pops up)
                    bool isProductsUpdateOk = await _productDbService.UpdateBatch(products);

                    if (isProductsUpdateOk)
                    {
                        /// Update the event-specific related product info
                        bool isLinkageUpdateOk = await _eventProductDbService.UpdateBatch(helixEvent.HelixEventId, eventProducts);

                        if (isLinkageUpdateOk)
                        {
                            helixEvent.ProductsLink = _eventProductDbService.GetAllByEventId(helixEvent.HelixEventId);

                            await _helixEventDbService.Update(helixEvent);

                            /// Only commit the changes if everything is updated successfully
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                            throw new Exception("Updating Linkage failed!");
                    }
                    else
                        throw new Exception("Updating Products failed!");
                }
                else
                    throw new Exception("Updating Events failed!");
            }
            catch (Exception ex)
            {
                /** In real application, 
                 * Encapsulate the details in the layer-specific log files for further troubleshooting 
                 * and wrap it in user-friendly manner
                **/
                throw ex;
            }

            return true;
        }
    }
}
