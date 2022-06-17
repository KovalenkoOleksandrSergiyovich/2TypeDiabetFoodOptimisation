using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.OptimizeService;

namespace WpfApp2TypeDiabet.Models
{

    public class OptimizeModel
    {
        public string Period { get; set; }
        public ObservableCollection<GoodToOptimize> ObjectiveGoods { get; set; } = new ObservableCollection<OptimizeService.GoodToOptimize>();
        public double MaxSum { get; set; }
        public User User { get; set; }
        public string UserPhysicalActivityLevel { get; set; }
        public double MinBU { get; set; }
        public double MaxBU { get; set; }
        public ObservableCollection<Result> Result { get; set; }
        public double TotalSum { get; set; }
        public double TotalBU { get; set; }

        public OptimizeModel(string period, ObservableCollection<GoodToOptimize> objectiveGoods, double maxSum, User user, 
            string userPhysicalActivityLevel, double minBU, double maxBU, ObservableCollection<Result> result, 
            double totalSum, double totalBU)
        {
            Period = period;
            ObjectiveGoods = objectiveGoods;
            MaxSum = maxSum;
            User = user;
            UserPhysicalActivityLevel = userPhysicalActivityLevel;
            MinBU = minBU;
            MaxBU = maxBU;
            Result = result;
            TotalSum = totalSum;
            TotalBU = totalBU;
        }
        public OptimizeModel(string userPhysicalActivityLevel, User user)
        {
            Period = "";
            ObjectiveGoods = new ObservableCollection<GoodToOptimize>();
            MaxSum = 0;
            User = user;
            UserPhysicalActivityLevel = userPhysicalActivityLevel;
            MinBU = 0;
            MaxBU = 0;
            Result = new ObservableCollection<Result>();
            TotalSum = 0;
            TotalBU = 0;
        }
    }
}
