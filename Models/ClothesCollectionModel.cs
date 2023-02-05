using System.Runtime.Serialization;

namespace Practice_01._02_KazanovAlexandr.Models
{
    [Serializable]
    public class ClothesCollectionModel
    {
        [DataMember]
        public List<ClothesModel> ListOfClothes { get; set; }
        public ClothesCollectionModel()
        {
            ListOfClothes = new List<ClothesModel>();
        }
    }
}
