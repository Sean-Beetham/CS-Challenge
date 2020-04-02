using System;

namespace JokeGenerator
{
    public static class UserInterface
    {
        /// <summary>
        /// Read an integer off the console line from the user.
        /// </summary>
        /// <returns>An integer entered by the user, -1 if not an integer.</returns>
        static public int ReadInteger()
        {
            int selected = 0;
            string input = Console.ReadLine().Trim();
            if (!int.TryParse(input, out selected))
            {
                return -1;
            }
            return selected;
        }

        /// <summary>
        /// Read a char off the console line from the user.
        /// </summary>
        /// <returns>A char entered by the user, 0 if not an integer.</returns>
        internal static char ReadChar()
        {
            char selected = '0';
            string input = Console.ReadLine().Trim();
            if (!char.TryParse(input, out selected))
            {
                return (char)0;
            }
            return char.ToLower(selected);
        }

        /// <summary>
        /// Display an error message to the user
        /// </summary>
        /// <param name="reason">Reason for the error</param>
        /// <param name="message">More information about the error</param>
        internal static void DisplayError(string reason, string message)
        {
            Console.WriteLine("An error occurred: ");
            Console.WriteLine("Reason: " + reason);
            Console.WriteLine("Error Message:");
            Console.WriteLine(message);
        }

        /// <summary>
        /// Display a string to the screen
        /// </summary>
        /// <param name="message">Message to display to user</param>
        internal static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Display an array of strings to the user. Display numPerLine items per line
        /// </summary>
        /// <param name="label">Title before the array</param>
        /// <param name="array">Array of items to display to the user</param>
        /// <param name="numPerLine">Number of items displayed per line</param>
        internal static void DisplayArray(string label, string[] array, int numPerLine = 1)
        {
            int x = 0;
            Console.WriteLine(label + ":");
            if (numPerLine == 0) numPerLine = 1;
            foreach(string buff in array)
            {
                Console.Write((x+1) + ". " + buff);
                x++;
                if(x % numPerLine == 0)
                {
                    Console.Write('\n');
                }
                else
                {
                    Console.Write("\t");
                }
            }
            Console.Write('\n');
        }
    }
}
