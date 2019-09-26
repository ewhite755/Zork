using System;
using System.Diagnostics;

namespace Zork
{
    public static class Assert
    {
        [Conditional("DEBUH")]
        public static void IsTrue(bool expression, string message = null)
        {
            if (expression == false)
            {
                throw new Exception(message);
            }
        }
    }
}