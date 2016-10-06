using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccessLayer
{
    /// <summary>
    /// It is inherited from an abstract layer for testability purposes.
    /// </summary>
    public class DataProviderFactory: IDataProviderFactory
    {
        public IDataProvider<Manufacturer> CreateManufacturerDataProvider() 
        {
            return new ManufacturerDataProvider();
        }
        public IDataProvider<Product> CreateProductDataProvider()
        {
            return new ProductDataProvider();
        }
    }
}
