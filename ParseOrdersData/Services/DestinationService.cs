using System;

namespace ParseOrderData.Services
{
    public class DestinationService
    {
        public string MapDestination(string supplier, string providedDestination)
        {
            try
            {


                if (!string.IsNullOrEmpty(providedDestination))
                {
                    return providedDestination;
                }

                switch (supplier)
                {

                    case "YIP-1":
                        return "Sydney AUSYD";
                    case "SFC01":
                        return "Melbourne AUMEL";
                    default:
                        throw new ArgumentException("Unknown supplier code or Name.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}