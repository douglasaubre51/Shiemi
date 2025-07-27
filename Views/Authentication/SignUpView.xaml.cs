using Shiemi.ViewModels;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Shiemi.Views;

public partial class SignUpView : ContentPage
{
    public SignUpView(SignUpVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // check if string is a number!
    private void PhoneNo_TextChanged(object sender, TextChangedEventArgs e)
    {
        var context = BindingContext as SignUpVM;
        context.PhoneNoMessage = "";

        string value = e.NewTextValue;

        // empty entry
        if (string.IsNullOrWhiteSpace(value)) return;

        // no match
        if (!Regex.Match(value, "^[0-9]+$").Success)
        {
            context.PhoneNoMessage = "enter digits!";
        }
    }
}