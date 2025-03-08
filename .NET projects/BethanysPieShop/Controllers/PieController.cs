using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private const string CategoryHeader = "All pies";
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPieRepository _pieRepository;
        
        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult List()
        {
            var pieListViewModel = new PieListViewModel(_pieRepository.AllPies, CategoryHeader);
            return View(pieListViewModel);
        }
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);

            if (pie is null)
            {
                return NotFound();
            }

            return View(pie);
        }
    }
}
