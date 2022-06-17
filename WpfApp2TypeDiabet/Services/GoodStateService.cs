using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class GoodStateService
    {
        public GoodState GoodState { get; set; }
        public GoodState GetCategory(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var caterory = from b in db.GoodState
                               where b.id == id
                               select b;
                return caterory.First();
            }
        }
        public GoodState GetCategory(string stateName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var caterory = from b in db.GoodState
                               where b.Name == stateName
                               select b;
                return caterory.First();
            }
        }
        public List<string> FillGoodStates()
        {
            List<string> StatesList = new List<string>
            {
                "Як є",
                "Варений",
                "Смажений"
            };
            return StatesList;
        }
        public string CreateGoodState(string name, double carbohydrates)
        {
            double breadUnits = 100 / (carbohydrates / 12);

            try
            {
                GoodState = new GoodState() { Name = name, Carbohydrates = breadUnits };
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.GoodState.Add(GoodState);
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
