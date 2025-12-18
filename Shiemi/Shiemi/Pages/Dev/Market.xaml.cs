using System.Diagnostics;
using AutoMapper;
using Microsoft.Maui.Platform;
using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Utilities.ServiceProviders;

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

			Mapper mapper = MapperProvider.GetMapper<DevDto, DevModel>()!;
			List<DevModel> devModels = mapper.Map<List<DevModel>>(devDtos);
			for (int i = 0; i < devModels.Count; i++)
				devModels[i].StartingPrice = Math.Round(devModels[i].StartingPrice);

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