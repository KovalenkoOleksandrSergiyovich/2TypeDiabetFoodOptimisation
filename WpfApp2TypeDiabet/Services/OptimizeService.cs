using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class OptimizeService
    {
        public class GoodToOptimize
        {
            public string GoodName { get; set; }
            public string GoodID { get; set; }
            public double GoodPrice { get; set; }
            public double GoodAmount { get; set; }
            public string GoodUnit { get; set; }
            public double GoodBU { get; set; }
            public List<Restriction> Restrictions { get; set; }
        }
        public class Result
        {
            public double BU { get; set; }
            public double Price { get; set; }
            public List<ProductBasket> ProductBasket { get; set; }
        }
        public class ProductBasket
        {
            public double Amount { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public string Unit { get; set; }
            public double BU { get; set; }
        }
        public OptimizeModel OptimizeModel { get; set; }
        public void CreateModel(string PhysicalActivityLevel, User user)
        {
            OptimizeModel = new OptimizeModel(PhysicalActivityLevel, user);
        }
        public void CalculateBUBorders()
        {
            switch (OptimizeModel.UserPhysicalActivityLevel)
            {
                case "Низький":
                    {
                        switch (OptimizeModel.User.Gender)
                        {
                            case "Чоловіча":
                                {
                                    OptimizeModel.MinBU = 14;
                                    OptimizeModel.MaxBU = 16;
                                    break;
                                }
                            case "Жіноча":
                                {
                                    OptimizeModel.MinBU = 10;
                                    OptimizeModel.MaxBU = 14;
                                    break;
                                }
                        }
                        break;
                    }
                case "Середній":
                    {
                        switch (OptimizeModel.User.Gender)
                        {
                            case "Чоловіча":
                                {
                                    OptimizeModel.MinBU = 19;
                                    OptimizeModel.MaxBU = 22;
                                    break;
                                }
                            case "Жіноча":
                                {
                                    OptimizeModel.MinBU = 16;
                                    OptimizeModel.MaxBU = 19;
                                    break;
                                }
                        }
                        break;
                    }
                case "Важкий":
                    {
                        OptimizeModel.MinBU = 23;
                        OptimizeModel.MaxBU = 28;
                        break;
                    }
            }

        }
    }
}
