using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WasteProject.Data.Entities;

namespace WasteProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MainContext context)
        {
            context.Database.EnsureCreated();

           
            if (context.Users.Any())
            {
                return;   
            }

            var users = new Users[]
            {
                new Users{Name="Kullanıcı1",Email="Kullanıcı1@gmail.com",Tc="11223344551",Adress="adres1",Phone="5447702233",CardNo=1234},
                new Users{Name="Kullanıcı2",Email="Kullanıcı2@gmail.com",Tc="11223344552",Adress="adres2",Phone="5447702233",CardNo=1234},
                new Users{Name="Kullanıcı3",Email="Kullanıcı5@gmail.com",Tc="11223344553",Adress="adres3",Phone="5447702233",CardNo=1234},

            };
            foreach (Users s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();

            var wastes = new WasteTypes[]
            {
            new WasteTypes{Name="Kağıt",Point=3},
            new WasteTypes{Name="Plastik",Point=5},
            new WasteTypes{Name="Cam",Point=7},
            new WasteTypes{Name="Metal",Point=4},
            };
            foreach (WasteTypes c in wastes)
            {
                context.WasteTypes.Add(c);
            }
            context.SaveChanges();

            var gifts = new Gifts[]{
            new Gifts{Name="Bisiklet",Point=120,Url="bicycle.PNG"},
            new Gifts{Name="Scooter",Point=200,Url="scooter.jpg"},
            new Gifts{Name="Tv",Point=600,Url="tv.PNG"},
            new Gifts{Name="Robot Süpürge",Point=250,Url="robot.PNG"},
            new Gifts{Name="Tablet",Point=150, Url = "tablet.PNG"},
             };
            foreach (Gifts g in gifts)
            {
                context.Gifts.Add(g);
            }
            context.SaveChanges();

            var points = new UserPoints[]{
            new UserPoints{UserId=1,Point=30},
            new UserPoints{UserId=2,Point=50},
            new UserPoints{UserId=3,Point=175},
            new UserPoints{UserId=1,Point=75},
            new UserPoints{UserId=2,Point=250},
            new UserPoints{UserId=3,Point=160},
            new UserPoints{UserId=1,Point=75},
            new UserPoints{UserId=2,Point=250},
            new UserPoints{UserId=3,Point=160},
            new UserPoints{UserId=1,Point=-25},
            new UserPoints{UserId=2,Point=-30},
            new UserPoints{UserId=3,Point=-40},
            };
            foreach (UserPoints p in points)
            {
                context.UserPoints.Add(p);
            }
            context.SaveChanges();

            var transactions = new WasteTransaction[]{
            new WasteTransaction{UserId=1,WasteId=1,WasteAmount=10,Point=30},
            new WasteTransaction{UserId=2,WasteId=2,WasteAmount=10,Point=50},
            new WasteTransaction{UserId=3,WasteId=3,WasteAmount=25,Point=175},
            new WasteTransaction{UserId=1,WasteId=2,WasteAmount=15,Point=75},
            new WasteTransaction{UserId=2,WasteId=2,WasteAmount=50,Point=250},
            new WasteTransaction{UserId=3,WasteId=4,WasteAmount=40,Point=160},
            new WasteTransaction{UserId=1,WasteId=2,WasteAmount=15,Point=75},
            new WasteTransaction{UserId=2,WasteId=2,WasteAmount=50,Point=250},
            new WasteTransaction{UserId=3,WasteId=4,WasteAmount=40,Point=160},

            };
            foreach (WasteTransaction wt in transactions)
            {
                context.WasteTransaction.Add(wt);
            }
            context.SaveChanges();



        }
    }
}
