using System;

namespace ParseOrderData.Services
{
    public class DestinationResolver
    {
        public string ResolveDestination(string supplier, string providedDestination)
        {
            if (!string.IsNullOrEmpty(providedDestination))
            {
                return providedDestination;
            }

            switch (supplier)
            {
                case "SFC01":
                    return "Melbourne AUMEL";
                case "YIP-1":
                    return "Sydney AUSYD";
                default:
                    throw new ArgumentException("Unknown supplier code.");
            }
        }
    }
}