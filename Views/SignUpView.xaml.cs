using Shiemi.ViewModels;
using System.ComponentModel.DataAnnotations;
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
        Debug.WriteLine("processing phone no...");
        var context = BindingContext as SignUpVM;
        context.PhoneNoValidationMessage = "";

        string value = e.NewTextValue;

        // empty entry
        if (string.IsNullOrWhiteSpace(value)) return;

        // no match
        if (!Regex.Match(value, "^[0-9]+$").Success)
        {
            context.PhoneNoValidationMessage = "enter digits!";
        }
    }
}