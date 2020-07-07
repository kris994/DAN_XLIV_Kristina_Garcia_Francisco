using System;

namespace DAN_XVIV_Kristina_Garcia_Francisco.Helper
{
    class Validation
    {
        /// <summary>
        /// Valid positive int input
        /// </summary>
        public string ValidPositiveNumber(int number)
        {
            bool b = Int32.TryParse(number.ToString(), out number);
            while (!b || number < 0)
            {
                return "Not a valid number";
            }
            return null;
        }

        /// <summary>
        /// The input cannot be empty
        /// </summary>
        /// <param name="name">name of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CannotBeEmpty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Cannot be empty";
            }
            else
            {
                return null;
            }
        }
    }
}
