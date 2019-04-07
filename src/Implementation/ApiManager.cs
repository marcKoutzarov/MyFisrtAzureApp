using System;
using System.Collections.Generic;

namespace Implementation
{
    public class ApiManager : Interfaces.IApiManager
    {
        public string GetValue(int index)
        {
            return "value: "  + index;
        }

        public string[] GetValues()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
