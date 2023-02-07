using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Practice_01._02_KazanovAlexandr.Models;
using System.Text;
using System.Text.Json;
using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Practice_01._02_KazanovAlexandr.Controllers
{
    public class ClothesShopController : Controller
    {
        private readonly string pathToFile = "E:\\Programms\\Practice_01.02_KazanovAlexandr\\wwwroot\\json\\clothes.json";

        public IActionResult AddClothes(ushort price, string name, string description, byte size)
        {
            var collection = new ClothesCollectionModel();
            using (var stream = new FileStream(pathToFile, FileMode.Open))
            {
                if (stream.Length != 0)
                {
                    collection = JsonSerializer.Deserialize<ClothesCollectionModel>(stream);
                }
            }
            var clothes = new ClothesModel(price, name, description, size);
            collection.ListOfClothes.Add(clothes);
            using (var stream = new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                JsonSerializer.Serialize<ClothesCollectionModel>(stream, collection);
            }
            ViewBag.result = "Successfully added";
            return View("Index");
        }

        public IActionResult GetClothes()
        {
            try
            {
                var collection = new ClothesCollectionModel();
                using (var stream = new FileStream(pathToFile, FileMode.Open))
                {
                    collection = JsonSerializer.Deserialize<ClothesCollectionModel>(stream);
                }
                for (int i = 0; i < collection.ListOfClothes.Count; i++)
                {
                    if (collection.ListOfClothes[i].Name == null && collection.ListOfClothes[i].Description == null && collection.ListOfClothes[i].Price == 0 && collection.ListOfClothes[i].Size == 0)
                    {
                        collection.ListOfClothes.Remove(collection.ListOfClothes[i]);
                    }
                }
                return View(collection);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult FindShow()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Show(long id)
        {
            var collection = new ClothesCollectionModel();
            using (var stream = new FileStream(pathToFile, FileMode.Open))
            {
                collection = JsonSerializer.Deserialize<ClothesCollectionModel>(stream);
            }
            try
            {
                foreach (var item in collection.ListOfClothes)
                {
                    if (item.Id == id)
                    {
                        return View(item);
                    }
                }
                ViewBag.result = "Element not found!";
                return View("Index");
            }
            catch
            {
                return View("Error") ;
            }
        }
        [HttpGet]
        public IActionResult FindDelete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var collection = new ClothesCollectionModel();
            using (var stream = new FileStream(pathToFile, FileMode.Open))
            {
                collection = JsonSerializer.Deserialize<ClothesCollectionModel>(stream);
            }
            try
            {
                foreach (var item in collection.ListOfClothes)
                {
                    if (item.Id == id)
                    {
                        collection.ListOfClothes.Remove(item);
                        using (var stream = new FileStream(pathToFile, FileMode.Create, FileAccess.Write))
                        {
                            JsonSerializer.Serialize<ClothesCollectionModel>(stream, collection);
                        }
                        ViewBag.result = "Successfully deleted";
                        return View("Index");
                    }
                }
                ViewBag.result = "Element doesn't consist!";
                return View("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        private void Serialize(ClothesCollectionModel collection)
        {
            using (var stream = new FileStream(pathToFile, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize<ClothesCollectionModel>(stream, collection);
            }
        }
    }
}
