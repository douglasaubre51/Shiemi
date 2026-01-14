using AutoMapper;
using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.PageModels.Dev;
using Shiemi.Services;
using Shiemi.Utilities.ServiceProviders;
using System.Diagnostics;

namespace Shiemi.Pages.Dev;

public partial class Market : ContentPage
{
    private readonly DevService _devServ;

    public Market(MarketpageModel pageModel, DevService devServ)
    {
        InitializeComponent();
        BindingContext = pageModel;
        _devServ = devServ;
    }

    protected override void OnDisappearing()
    {
        Provider.GetTitleBarWidget()!.SearchBarIsEnabled = false;
        base.OnDisappearing();
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
            {
                devModels[i].StartingPrice = Math.Round(devModels[i].StartingPrice);
            }
            DevCollectionView.ItemsSource = devModels;

            Provider.GetTitleBarWidget()!.SearchBarIsEnabled = true;
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