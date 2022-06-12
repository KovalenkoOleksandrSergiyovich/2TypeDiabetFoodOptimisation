using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class GoodService
    {
        public Goods Good { get; set; }
        public string CreateGood(Goods good)
        {
            Good = good;
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Goods.Add(good);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        public string DeleteGood(Goods good)
        {
            Good = good;
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Goods.Remove(good);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public Goods GetGood(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var good = from b in db.Goods
                           where b.id == id
                           select b;
                return good.First();
            }
        }
        public Goods GetGood(string goodName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var good = from b in db.Goods
                           where b.GoodName == goodName
                           select b;
                return good.First();
            }
        }
        public List<Goods> SelectGoodsLike(string goodName)
        {
            List<Goods> goods = new List<Goods>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var good = from b in db.Goods
                           where b.GoodName.Contains(goodName)
                           select b;
                foreach (Goods g in good)
                {
                    goods.Add(g);
                }
            }
            return goods;
        }
    }
}
