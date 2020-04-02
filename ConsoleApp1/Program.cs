using JokeGenerator.Feeds;
using System;

namespace JokeGenerator
{
    class Program
    {
        static ChuckNorrisJokeFeed joke;
        const int exit = 3;
        static string[] categories = null;
        /// <summary>
        /// Options is the menu options that are available to the end user.
        /// </summary>
        static string[] options = new string[] { "Retrieve a Joke", "Display Categories", "Exit" };
        static NameFeed name;

        static void Main(string[] args)
        {
            joke = new ChuckNorrisJokeFeed();
            name = new NameFeed();
            int selection = 0;
            do
            {
                selection = DisplayMainMenu();
                switch (selection)
                {
                    case 1:
                        DisplayRandomJokes();
                        break;
                    case 2:
                        DisplayCategories();
                        break;
                    case 3:
                        UserInterface.DisplayMessage("Have a good day");
                        break;
                    default:
                        UserInterface.DisplayMessage("Invalid Option Please Select Something Else");
                        break;
                }
            } while (selection != Program.exit);
        }

        
        /// <summary>
        /// Method to display a number of random jokes to the end user.
        /// This asks the user a couple questions and based on those chooses
        /// proper joke(s) to print.
        /// </summary>
        private static void DisplayRandomJokes()
        {
            char nmChoice = '0';
            char ctgChoice = '0';
            int category = -1;
            Tuple<string, string> selectedName = null;
            int numOfJokes = 0;

            //Ask the user if we want to use a random name.
            UserInterface.DisplayMessage("Do you want to use a random name? Enter Y/N");
            do
            {
                nmChoice = UserInterface.ReadChar();
            } while (nmChoice != 'n' && nmChoice != 'y');

            //Ask the user if they want to select a category
            UserInterface.DisplayMessage("Do you want to select a category? Enter Y/N");
            do
            {
                ctgChoice = UserInterface.ReadChar();
            } while (ctgChoice != 'n' && ctgChoice != 'y');
            if(ctgChoice == 'y')
            {
                //If they do, display and have them select a category
                if (!DisplayCategories()) return;
                UserInterface.DisplayMessage("Select a category by its number.");
                do
                {
                    category = UserInterface.ReadInteger()-1;
                } while (category > categories.Length || category<0);
            }

            //Ask how many jokes the user wants.
            UserInterface.DisplayMessage("How many jokes do you want? Select 1-9");
            do
            {
                numOfJokes = UserInterface.ReadInteger();
            } while (numOfJokes < 1 || numOfJokes > 9);
            try
            {
                //Process the variables and display the jokes.
                if (nmChoice == 'y') selectedName = name.GetName();
                for(int i = 0; i < numOfJokes; i++)
                {
                    string rndJoke = joke.GetRandomJoke(selectedName?.Item1, selectedName?.Item2,
                        category < 0 ? null : categories[category]);
                    UserInterface.DisplayMessage("Here is joke #" + (i + 1) + ":");
                    UserInterface.DisplayMessage(rndJoke);
                }
            }
            catch (Exception e)
            {
                UserInterface.DisplayError("Unable to retrieve categories", e.Message);
                return;
            }
        }

        /// <summary>
        /// Display the main menu to the user and have them select an option
        /// </summary>
        /// <returns>Integer representing the input from the user.</returns>
        private static int DisplayMainMenu()
        {
            UserInterface.DisplayArray("Select one of the following options by entering the corresponding number:", options);
            return UserInterface.ReadInteger();
        }

        /// <summary>
        /// Display the categories to the user, if categories is null, attempt
        /// to retreive them from the website
        /// </summary>
        /// <returns>True, if we are able to successfully display the categories
        ///          False, if there is an error retrieving them from the website</returns>
        private static bool DisplayCategories()
        {
            if(categories == null)
            {
                try
                {
                    //Attempt to retrieve the categories from the site
                    categories = joke.GetCategories();
                }
                catch (Exception e)
                {
                    UserInterface.DisplayError("Unable to retrieve categories", e.Message);
                    return false;
                }
            }
            UserInterface.DisplayArray("List of Categories", categories,3);
            return true;
        }
    }
}
