using System;

namespace TapBlitz.Util
{
    public class Assert
    {
        public static void IsTrue(bool condition, string message = "")
        {
            if (!condition)
                throw new Exception(@"Assertion failed: {message}");
        }
    }
}
