using System;
using System.Collections.Generic;
using System.Linq;

namespace ParseOrderData.Utilities
{
    public static class ValidationHelper
    {
        public static bool ValidateHeaderRecord(string[] headerFields)
        {
            if (headerFields.Length != 6)
            {
                return false;
            }

            
            return true;
        }

        public static bool ValidateDetailRecord(string[] detailFields, HashSet<int> existingLineNumbers)
        {
            if (detailFields.Length != 5)
            {
                return false;
            }

            if (!int.TryParse(detailFields[2], out int lineNumber) || existingLineNumbers.Contains(lineNumber))
            {
                return false;
            }

            existingLineNumbers.Add(lineNumber);
            return true;
        }
    }
}