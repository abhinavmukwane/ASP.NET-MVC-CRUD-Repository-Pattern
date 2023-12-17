using CRUD_Repository.Core;
using CRUD_Repository.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Repository.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepo.GetAll();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new Product());
            }
            else
            {
                try
                {
                    Product product = await _productRepo.GetById(id);
                    if (product != null)
                    {
                        return View(product);
                    }
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                    return RedirectToAction("Index");
                }
                TempData["errorMessage"] = $"Product details not found with ProductID : {id}";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Product modal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(modal.ProductID == 0)
                    {
                        await _productRepo.Add(modal);
                        TempData["successMessage"] = "Product created successfully!";
                    }
                    else
                    {
                        await _productRepo.Update(modal);
                        TempData["successMessage"] = "Product details updated successfully!";
                    }
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["errorMessage"] = "Modal State is Invalid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product product = await _productRepo.GetById(id);
                if (product != null)
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["errorMessage"] = $"Product details not found with ProductID : {id}";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Product model)
        {
            try
            {
                await _productRepo.Delete(model.ProductID);
                TempData["successMessage"] = "Product details deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
