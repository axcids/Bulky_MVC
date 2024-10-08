﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;   
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            //I need to learn this more
            return View(objProductList);
        }
        //--------------------------------------------------------------------------------------------------------------------------------Create <- Upsert Now 
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new () {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem { 
                    Text = u.Name, 
                    Value = u.Id.ToString() 
                }),
                Product = new Product()
            };
            if(id == null || id == 0) {
                //It means it is a create
                return View(productVM);

            }
            else {
                //It means it is an update 
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null) {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    //check if the user have uploaded a new image 
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl)) {
                        //delete old image 
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath)) {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create)) {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(productVM.Product.Id == 0) {
                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["Success"] = "Product Created Successfully!";
                }
                else {
                    _unitOfWork.Product.Update(productVM.Product);
                    TempData["Success"] = "Product Updated Successfully!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }

        }
        //---------------------------------------------------------------------------------------------------------------------------------Edit (removed and replaced with upsert)
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}
        //-------------------------------------------------------------------------------------------------------------------------------Delete
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{

        //    Product obj = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["Success"] = "Product deleted successfully!";
        //    return RedirectToAction("Index");

        //}
        //----------------------------------------------------------------------------------------------------------------------------------API Calls
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {

            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objProductList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id) {

            var productToBeDeleted = _unitOfWork.Product.Get(u=>u.Id == id);
            if (productToBeDeleted == null) {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.Trim('\\'));

            if (System.IO.File.Exists(oldImagePath)) {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful"});
        }

        #endregion

    }
}
