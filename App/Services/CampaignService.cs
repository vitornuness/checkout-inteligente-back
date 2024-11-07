namespace App.Services;

using App.DTOs;
using App.Models;
using App.Repositories;

public class CampaignService(
    CampaignRepository campaignRepository,
    ProductService productService,
    FileService fileService,
    IWebHostEnvironment environment
    )
{
    private readonly CampaignRepository _campaignRepository = campaignRepository;
    private readonly ProductService _productService = productService;
    private readonly FileService _fileService = fileService;
    private readonly IWebHostEnvironment _environment = environment;

    public async Task<List<Campaign>> GetAll() => await this._campaignRepository.GetAll();

    public async Task<Campaign> GetById(int id) => await this._campaignRepository.FindOrFail(id);

    public async Task<Campaign> Create(CampaignInputDTO campaignInputDTO, IFormFile? image)
    {
        IEnumerable<Product> products = [];

        if (campaignInputDTO.ProductsId?.Count > 0)
        {
            products = await this._productService.GetWhereIds(campaignInputDTO.ProductsId);
        }

        Campaign campaign = new(
            campaignInputDTO.Title,
            campaignInputDTO.Active
        )
        {
            Products = products,
        };

        await this._campaignRepository.Store(campaign);

        await this.Update(campaign.Id, campaignInputDTO, image);

        return campaign;
    }

    public async Task<Campaign> Update(int id, CampaignInputDTO campaignInputDTO, IFormFile? image)
    {
        IEnumerable<Product> products = [];

        if (campaignInputDTO.ProductsId?.Count > 0)
        {
            products = await this._productService.GetWhereIds(campaignInputDTO.ProductsId);
        }

        var oldCampaign = await this._campaignRepository.FindOrFail(id);

        Campaign newCampaign = new(
            campaignInputDTO.Title,
            campaignInputDTO.Active
        )
        {
            Id = oldCampaign.Id,
            Products = products,
            ImageUrl = oldCampaign.ImageUrl,
        };
        if (image?.Length > 0)
        {
            var path = this.GetCampaignImagesPath(newCampaign.Id);
            await this._fileService.SaveFile(image, path);

            newCampaign.ImageUrl = GetCampaignImagesUrl(newCampaign.Id);
        }

        return await this._campaignRepository.Update(oldCampaign, newCampaign);
    }

    public async Task Delete(int id)
    {
        var campaign = await this.GetById(id);
        await this._campaignRepository.Delete(campaign);

        if (campaign.ImageUrl != GetCampaignImagesUrl(0))
        {
            await this._fileService.RemoveFile(this.GetCampaignImagesPath(id));
        }
    }

    private string GetCampaignImagesPath(int id) => Path.Combine(this._environment.WebRootPath, "files/images/categories", id.ToString() + ".png");

    private static string GetCampaignImagesUrl(int id) => "http://localhost:5102/files/images/categories/" + id.ToString() + ".png";
}
