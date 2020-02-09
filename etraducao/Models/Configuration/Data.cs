using System;

namespace etraducao.Models.Configuration
{
    public static class Data
    {
        public static DateTime ConverterData(string data)
        {
            try
            {
                return DateTime.Parse(data);
            }
            catch (System.Exception)
            {
                return DateTime.Now;
            }
        }
    }
}
