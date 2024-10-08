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
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return View(objCompanyList);
        }
        //--------------------------------------------------------------------------------------------------------------------------------Create <- Upsert Now 
        public IActionResult Upsert(int? id)
        {

            if(id == null || id == 0) {

                //It means it is a create
                return View(new Company());

            }
            else {
                //It means it is an update 
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {

            if (ModelState.IsValid)
            {

                if(companyObj.Id == 0) {
                    _unitOfWork.Company.Add(companyObj);
                    TempData["Success"] = "Company Created Successfully!";
                }
                else {
                    _unitOfWork.Company.Update(companyObj);
                    TempData["Success"] = "Company Updated Successfully!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else {
                return View(companyObj);
            }

        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {

            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data = objCompanyList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id) {

            var CompanyToBeDeleted = _unitOfWork.Company.Get(u=>u.Id == id);
            
            if (CompanyToBeDeleted == null) {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful"});
        }

        #endregion

    }
}
