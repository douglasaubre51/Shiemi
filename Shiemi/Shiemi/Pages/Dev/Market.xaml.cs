using System.Diagnostics;
using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.Services;

namespace Shiemi.Pages.Dev;

public partial class Market : ContentPage
{
	private readonly DevService _devServ;

	public Market(DevService devServ)
	{
		InitializeComponent();
		_devServ = devServ;
	}

	protected override async void OnAppearing()
	{
		try
		{
			List<DevDto>? devDtos = await _devServ.GetAll();
			if (devDtos is null)
				return;

			List<DevModel> devModels = [];
			foreach (var d in devDtos)
			{
				DevModel dev = new()
				{
					Profile = d.Profile,
					UserName = d.FirstName + " " + d.LastName
				};

				devModels.Add(dev);
			}

			DevCollectionView.ItemsSource = devModels;
		}
		catch (Exception ex)
		{
			Debug.WriteLine("Market: OnAppearing: " + ex.Message);
		}
		finally
		{
			base.OnAppearing();
		}
	}
}