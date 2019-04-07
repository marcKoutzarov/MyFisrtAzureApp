using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
   public interface IApiManager
    {
        /// <summary>
        ///  Gets one  value item
        /// </summary>
        /// <returns>String</returns>
        string GetValue(int index);
        
        /// <summary>
        ///  Gets a value collection 
        /// </summary>
        /// <returns>String</returns>
       string[] GetValues();
    }
}
