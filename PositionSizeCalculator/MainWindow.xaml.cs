using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockCalculator
{
    public partial class MainWindow : Window
    {
        private bool isPinned = false; // Tracks the pinned state

        public MainWindow()
        {
            InitializeComponent();
        }

        // Triggered when either input box changes
        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculatePositionSize();
        }

        private void CalculatePositionSize()
        {
            // Validate inputs
            if (!decimal.TryParse(AccountBalanceInput.Text, out decimal accountBalance) || accountBalance <= 0)
            {
                ResultText.Text = "Invalid account balance.";
                return;
            }

            if (!decimal.TryParse(StockPriceInput.Text, out decimal stockPrice) || stockPrice <= 0)
            {
                ResultText.Text = "Invalid stock price.";
                return;
            }

            // Calculate position size
            int positionSize = (int)(accountBalance / stockPrice);

            // Display result
            ResultText.Text = $"You can buy {positionSize} shares.";
        }

        // Handles the Pin Button click
        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            isPinned = !isPinned; // Toggle the pinned state
            Topmost = isPinned; // Set window's Topmost property

            // Update button text based on state
            PinButton.Content = isPinned ? "📌 Unpin" : "📌 Pin";
        }

        // Preview input for Account Balance field to allow decimal and number input
        private void AccountBalanceInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumericInput(e.Text);

            if (!e.Handled && e.Text == ".")
            {
                // Check if there's already a decimal point in the input
                if (!AccountBalanceInput.Text.Contains("."))
                {
                    e.Handled = false;  // Allow decimal point
                }
                else
                {
                    e.Handled = true;  // Prevent more than one decimal point
                }
            }
        }

        // Preview input for Stock Price field to allow decimal and number input
        private void StockPriceInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumericInput(e.Text);

            if (!e.Handled && e.Text == ".")
            {
                // Check if there's already a decimal point in the input
                if (!StockPriceInput.Text.Contains("."))
                {
                    e.Handled = false;  // Allow decimal point
                }
                else
                {
                    e.Handled = true;  // Prevent more than one decimal point
                }
            }
        }

        private bool IsNumericInput(string text)
        {
            // Allow numeric input and the decimal point
            return decimal.TryParse(text, out _) || text == ".";
        }
    }
}
