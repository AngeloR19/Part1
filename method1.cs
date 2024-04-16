using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgPOE
{
 
    public class Recipe
    {
        //getters and setters
        public string Name { get; set; } //stores the name of the recipe
        public string[] Ingredients { get; } //array of the ingedients
        public string[] Steps { get; } // array of the steps

        // constructor to initialize a Recipe object
        public Recipe(string name, string[] ingredients, string[] steps)
        {
            Name = name; 
            Ingredients = ingredients; 
            Steps = steps; 
        }
    }

   
    public class method1
    {
        private Recipe originalRecipe; // original recipe
        private Recipe currentRecipe; // currently recipe

        // Method to display menu
        public void menu()
        {
            Console.WriteLine("Please enter your name:"); // asking user for their name
            string name = Console.ReadLine(); //reads users name

            Console.WriteLine("\nHello " + name + ", please choose an option:");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1. Add recipe");
                Console.WriteLine("2. Display recipe");
                Console.WriteLine("3. Scale recipe");
                Console.WriteLine("4. Reset quantities");
                Console.WriteLine("5. Clear recipe");
                Console.WriteLine("6. Exit");

                string input = Console.ReadLine(); // read user's menu choice
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        AddRecipe(); // calls method to add recipe
                        break;
                    case "2":
                        DisplayRecipe(); // calls method to display recipe
                        break;
                    case "3":
                        ScaleRecipe(); // calls method to scale recipe quantities
                        break;
                    case "4":
                        ResetQuantities(); // calls method to reset ingredient quantities
                        break;
                    case "5":
                        ClearRecipe(); // calls method to clear the recipe
                        break;
                    case "6":
                        exit = true; // exits program
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again."); //will display if user entered invalid number option
                        break;
                }
            }
        }

      
        private void AddRecipe()
        {
            Console.WriteLine("Enter recipe name:");
            string name = Console.ReadLine(); // reads name of the recipe the user entered

            Console.WriteLine("\nEnter number of ingredients:");
            int numOfIngredients = Convert.ToInt32(Console.ReadLine()); // gets number of ingredients

            string[] ingredients = new string[numOfIngredients]; // array for ingredients
            for (int i = 0; i < numOfIngredients; i++)
            {
                Console.WriteLine($"\nIngredient {i + 1}");
                Console.Write("Name: ");
                string ingredientName = Console.ReadLine(); // reads ingredient name

                Console.Write("Quantity: ");
                double quantity;
                while (!double.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)//if user enters anything less than or equal to 0 they message will appear
                {
                    Console.Write("Please enter a valid quantity greater than 0: ");
                }

                Console.Write("Unit of measurement: ");
                string unit = Console.ReadLine(); // gets unit of measurement

                // ingredient detials
                ingredients[i] = $"Ingredient: {ingredientName}\n" +
                                 $"Quantity: {quantity}\n" +
                                 $"Measure of unit: {unit}\n";
            }

            Console.WriteLine("\nEnter number of steps:");
            int stepCount = Convert.ToInt32(Console.ReadLine()); // gets number of steps

            string[] steps = new string[stepCount]; // array for steps
            for (int i = 0; i < stepCount; i++)
            {
                Console.WriteLine($"\nStep {i + 1}");
                Console.Write("Description: ");
                string description = Console.ReadLine(); // reads the description
                steps[i] = description;
            }

            // creates a recipe object
            originalRecipe = new Recipe(name, ingredients, steps);
            currentRecipe = originalRecipe; // set current recipe to the original recipe
            Console.WriteLine("\nRecipe added successfully!");
        }

        
        private void DisplayRecipe()
        {
            //if else statement 
            if (currentRecipe == null) //if no recipe is avaliable this message will show
            {
                Console.WriteLine("\nNo recipe available. Please add a recipe first.");
            }
            else
            {
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine($"\nRecipe: {currentRecipe.Name}\n");
                Console.WriteLine("----------------------------------------------------------------------------");

                Console.WriteLine("\nIngredients\n");
                for (int i = 0; i < currentRecipe.Ingredients.Length; i++) //will display each ingredient in the array and the increments the number of ingedients by 1
                {
                    Console.WriteLine($"{i + 1}. {currentRecipe.Ingredients[i]}");
                    Console.WriteLine("");
                }

                Console.WriteLine("----------------------------------------------------------------------------");

                Console.WriteLine("\nSteps\n");
                for (int i = 0; i < currentRecipe.Steps.Length; i++)
                {
                    Console.WriteLine($"Step {i + 1}: \n{currentRecipe.Steps[i]}");//will display each step in the array and the increments the number of steps by 1
                    Console.WriteLine("");
                }
                Console.WriteLine("----------------------------------------------------------------------------");
            }
        }


        private void ScaleRecipe()
        {
            if (currentRecipe == null)//if there is no recipe a message will display
            {
                Console.WriteLine("\nNo recipe available. Please add a recipe first.");
                return;
            }

            Console.WriteLine("Enter scaling factor:");//asks user for the scaling factor
            double factor;
            if (!double.TryParse(Console.ReadLine(), out factor) || factor <= 0) //if scalling factor is less or equal to 0 than a message will display
            {
                Console.WriteLine("Invalid scaling factor. Please enter a valid number greater than 0.");
                return;
            }

            // array to store scaled ingredients
            string[] scaledIngredients = new string[currentRecipe.Ingredients.Length];

            for (int i = 0; i < currentRecipe.Ingredients.Length; i++)
            {
                string ingredient = currentRecipe.Ingredients[i];

                // parse the ingredient to extract name, quantit and unit of measurement
                string[] lines = ingredient.Split('\n');
                string ingredientName = "";
                double quantity = 0;
                string unit = "";

                foreach (string line in lines)
                {
                    if (line.StartsWith("Ingredient: "))
                    {
                        ingredientName = line.Substring("Ingredient: ".Length).Trim();
                    }
                    else if (line.StartsWith("Quantity: "))
                    {
                        double.TryParse(line.Substring("Quantity: ".Length), out quantity);
                    }
                    else if (line.StartsWith("Measure of unit: "))
                    {
                        unit = line.Substring("Measure of unit: ".Length).Trim();
                    }
                }

                // scaling the quantity
                double scaledQuantity = quantity * factor;

                // new scaled quantities
                string scaledIngredient = $"Ingredient: {ingredientName}\n" +
                                          $"Quantity: {scaledQuantity}\n" +
                                          $"Measure of unit: {unit}\n";

                scaledIngredients[i] = scaledIngredient;
            }

            // create a new recipe object with scaled ingredients and updates the current recipe
            currentRecipe = new Recipe(currentRecipe.Name, scaledIngredients, currentRecipe.Steps);

            Console.WriteLine($"\nRecipe scaled by a factor of {factor}.");
        }

     
        private void ResetQuantities()
        {
            if (originalRecipe == null)//if there is no recipe a message will display
            {
                Console.WriteLine("\nNo original recipe available. Please add a recipe first.");
                return;
            }

            currentRecipe = originalRecipe; // reset current recipe to the original recipe
            Console.WriteLine("\nQuantities reset to original values.");
        }

        
        private void ClearRecipe()
        {
            currentRecipe = null; // clears the current recipe
            originalRecipe = null; // clears the original recipe
            Console.WriteLine("\nRecipe cleared."); //message will show when it is completed
        }
    }
}