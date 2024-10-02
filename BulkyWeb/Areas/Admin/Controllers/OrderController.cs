﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers {
    [Area("Admin")]
    public class OrderController : Controller {

        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork) {

            _unitOfWork = unitOfWork;

        }
        public IActionResult Index() {


            return View();
        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {

            List<OrderHeader> objOrderHeader = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new { data = objOrderHeader });
        }

        #endregion
    }
}
