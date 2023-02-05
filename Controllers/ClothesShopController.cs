using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Practice_01._02_KazanovAlexandr.Models;
using System.Text;
using System.Text.Json;
using System;
using System.Data;

namespace Practice_01._02_KazanovAlexandr.Controllers
{
    public class ClothesShopController : Controller
    {
        private readonly string pathToFile = "E:\\Programms\\Practice_01.02_KazanovAlexandr\\wwwroot\\json\\clothes.json";

        public IActionResult AddClothes(ushort price, string name, string description)
        {
            var collection = new ClothesCollectionModel();
            using (var stream = new FileStream(pathToFile, FileMode.Open))
            {
                if (stream.Length != 0)
                {
                    collection = JsonSerializer.Deserialize<ClothesCollectionModel>(stream);
                }
            }
            var clothes = new ClothesModel(price, name, description);
            collection.ListOfClothes.Add(clothes);
            Serialize(collection);
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
                    if (collection.ListOfClothes[i].Name == null && collection.ListOfClothes[i].Description == null && collection.ListOfClothes[i].Price == 0)
                    {
                        collection.ListOfClothes.Remove(collection.ListOfClothes[i]);
                    }
                }
                return View(collection);
            }
            catch(Exception ex)
            {
                throw new Exception("Something went wrong! Try again later");
            }
        }

        private void Serialize(ClothesCollectionModel collection)
        {
            using (var stream = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<ClothesCollectionModel>(stream, collection);
            }
        }
    }
}
