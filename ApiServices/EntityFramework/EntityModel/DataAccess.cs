using System;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace ApiServices.EntityFramework.EntityModel
{
    public class DataAccess
    {
        private static SmartMunEntities _dataContext = null;


        /// <summary>
        /// Gets All the Entities from the Database
        /// </summary>
        /// 
        internal static SmartMunEntities Context
        {
            get
            {
                // If the context is missing, create a new one
                if (_dataContext == null)
                {
                    _dataContext = new SmartMunEntities();
                    _dataContext.Configuration.LazyLoadingEnabled = false;
                }

                return _dataContext;
            }
        }

        /// <summary>
        /// Save changes in the context with error handling and trace the 
        /// property that executes the error and the error message
        /// </summary>
        public static void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}