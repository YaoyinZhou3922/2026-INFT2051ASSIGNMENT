using System.Collections.ObjectModel;
using UseItUp.Models;
using UseItUp.Services;

namespace UseItUp.Views
{
    public partial class IngredientListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public IngredientListPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var ingredientsFromDb = await _databaseService.GetIngredientsAsync();
            IngredientsList.ItemsSource = new ObservableCollection<Ingredient>(ingredientsFromDb);
        }

        private async void OnAddIngredientClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddIngredientPage");
        }
        // Event handler to delete an ingredient
        private async void OnDeleteIngredientClicked(object sender, EventArgs e)
        {
            // 1. Get the button that was clicked
            var button = sender as Button;

            // 2. Extract the specific Ingredient object attached to this button
            var ingredientToDelete = button?.BindingContext as Ingredient;

            if (ingredientToDelete != null)
            {
                // 3. Ask for user confirmation
                bool isConfirmed = await DisplayAlert(
                    "Delete Item",
                    $"Are you sure you want to delete {ingredientToDelete.Name}?",
                    "Yes", "No");

                if (isConfirmed)
                {
                    // 4. Delete from SQLite database
                    await _databaseService.DeleteIngredientAsync(ingredientToDelete);

                    // 5. Reload the list to refresh the UI
                    await LoadDataAsync();
                }
            }
        }
    }
}