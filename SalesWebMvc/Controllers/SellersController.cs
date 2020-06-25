using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); //Acessa o model
            return View(list);
        }

        //Get
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll(); //Busca todos departamentos
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        //Post (set)
        [HttpPost]
        [ValidateAntiForgeryToken] //Segurança
        //Não precisa alterar pois o framework já faz tudo. Pega o DepartmentId e instancia o Seller
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}