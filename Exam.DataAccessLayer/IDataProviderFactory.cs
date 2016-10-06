/* -------------------------------------------------------------------------------------------------
   Restricted - Copyright (C) Siemens Healthcare GmbH/Siemens Medical Solutions USA, Inc., 2016. All rights reserved
   ------------------------------------------------------------------------------------------------- */
   
namespace Exam.DataAccessLayer
{
    public interface IDataProviderFactory
    {
        IDataProvider<Manufacturer> CreateManufacturerDataProvider();
        IDataProvider<Product> CreateProductDataProvider();
    }
}