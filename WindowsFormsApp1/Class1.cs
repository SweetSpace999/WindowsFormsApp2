using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using System.Security.Policy;
using WindowsFormsApp1;

namespace WindowsFormsApp1
{
    internal class Client
    {
        int id_client;
        string name;
        DateTime birth_date;

        public Client(int id, string n, DateTime db)
        {
            id_client = id;
            name = n;
            birth_date = db;
        }
        public int Id_client
        {
            get { return id_client; }
            set { id_client = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime Birth_date
        {
            get { return birth_date; }
            set { birth_date = value; }
        }
    }
    internal class Car
    {
        int id_car;
        string car_model;
        string gov_id;
        int price_pday;

        public Car(int id, string b, string n, int pr)
        {
            id_car = id;
            gov_id = n;
            car_model = b;
            price_pday = pr;
        }
        public int Id_car
        {
            get { return id_car; }
            set { id_car = value; }
        }
        public string Car_model
        {
            get { return car_model; }
            set { car_model = value; }
        }

        public string Gov_id
        {
            get { return gov_id; }
            set { gov_id = value; }
        }

        public int Price_pday
        {
            get { return price_pday; }
            set { price_pday = value; }
        }
    }
    internal class Rent
    {
        int rent_id;
        DateTime rent_date;
        int fk_car;
        int fk_client;
        int quantity_days;


        public Rent(int id, DateTime date, int fk1, int fk2, int qd)
        {
            rent_id = id;
            rent_date = date;
            fk_car = fk1;
            fk_client = fk2;
            quantity_days = qd;
        }
        public int Rent_id
        {
            get { return rent_id; }
            set { rent_id = value; }
        }
        public DateTime Rent_date
        {
            get { return rent_date; }
            set { rent_date = value; }
        }
        public int Quant_days
        {
            get { return quantity_days; }
            set { quantity_days = value; }
        }
        public int Fk_car
        {
            get { return fk_car; }
            set { fk_car = value; }
        }
        public int Fk_client
        {
            get { return fk_client; }
            set { fk_client = value; }
        }
    }

    internal class CL
    {
        public List<Client> clients = new List<Client>();
        char[] d = { '\t' };
        public void Read_Clients()
        {
            string[] sc = File.ReadAllLines("client.txt");
            foreach (string s in sc)
            {
                string[] sl = s.Split(d, StringSplitOptions.RemoveEmptyEntries);

                DateTime dd = DateTime.Parse(sl[2]);
                Client cc = new Client(int.Parse(sl[0]), sl[1], dd);
                clients.Add(cc);
            }
        }
        public void Save_Clients()
        {
            var temp = new List<string>();
            foreach (Client x in clients)
            {
                string str = x.Id_client.ToString() + '\t' + x.Name.ToString() + '\t' + x.Birth_date.ToString().Substring(0,10);
                temp.Add(str);
            }
            File.Delete("client.txt");
            File.AppendAllLines("client.txt", temp);
        }
    }
internal class CR // Car
{
    public List<Car> cars = new List<Car>();
    char[] d = { '\t' };
    public void Read_Cars()
    {
        string[] sc = File.ReadAllLines("cars.txt");
        foreach (string s in sc)
        {
            string[] sl = s.Split(d, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(sl);
            Car cc = new Car(int.Parse(sl[0]), sl[1], sl[3], int.Parse(sl[2]));
            cars.Add(cc);
        }
    }
    public void Save_Cars()
    {
        var temp = new List<string>();
        foreach (Car f in cars)
        {
            string str = f.Id_car.ToString() + '\t' + f.Car_model.ToString() + '\t' + f.Price_pday.ToString() + '\t' + f.Gov_id.ToString();
            temp.Add(str);
        }
        File.Delete("cars.txt");
        File.AppendAllLines("cars.txt", temp);
    }
}
    internal class RN // Rent
    {
        public List<Rent> rents = new List<Rent>();
        char[] d = { '\t' };
        public void Read_Rents()
        {
            string[] sc = File.ReadAllLines("rent.txt");
            foreach (string s in sc)
            {
                string[] sl = s.Split(d, StringSplitOptions.RemoveEmptyEntries);

                DateTime dd = DateTime.Parse(sl[1]);
                Rent cc = new Rent(int.Parse(sl[0]), dd, int.Parse(sl[2]), int.Parse(sl[3]), int.Parse(sl[4]));
                rents.Add(cc);
            }
        }
    public void Save_Rent()
        {
            var temp = new List<string>();
            foreach (Rent k in rents)
            {
                string str = k.Rent_id.ToString() + '\t' 
                    + k.Rent_date.ToString().Substring(0,10) + '\t' 
                    + k.Fk_client.ToString() + '\t' + k.Fk_car.ToString() + '\t' + k.Quant_days.ToString();
                temp.Add(str);
            }
            File.Delete("rent.txt");
            File.AppendAllLines("rent.txt", temp);
        }
    }
}
