using System;

namespace TapBlitzUtils
{
    public class Assert
    {
        public static void IsTrue(bool condition, string errorMessage)
        {
            if (!condition)
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Assertion failed";

                throw new Exception(errorMessage);
            }
        }
    }
}
