using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace tutorial
{
    abstract class Employee
    {
        public int id;
        public double money;
        public string name, surname;
        public Employee(int id,string name,string surname, double money)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.money = money;
        }

        public string getName()
        {
            return "surname: " + surname + "\n" + "name: " + name;
        }
        public double Salary
            { get { return this.money; } }
        public abstract double countSalary();
        
    }

    class Hour : Employee
    {
        public Hour(int id, string name, string surname, double money) : base(id, name, surname, money) { }

        public override double countSalary()
        {
            return money * 8 * 20.8;
        }
     
    }

    class Month : Employee
    {
        public Month(int id, string name, string surname, double salary) : base(id, name, surname, salary) { }
        public override double countSalary()
        {
            return money;
        }
       
    }
    class Program
    {
        static List<Employee> ReadEmployees()
        {
            string file = @"D:\Downloads\eng\in.txt";
            List<Employee> result = new List<Employee>();
            int id;
            string name, surname, type;
            double salary;
            try
            {
                using (StreamReader sw = new StreamReader(file, System.Text.Encoding.Default))
                {
                    while (!sw.EndOfStream)
                    {
                        id = 0; name = ""; surname = ""; salary = 0; type = "";
                        while (sw.Peek() != ' ')
                            id = id * 10 + sw.Read() - 48;
                        sw.Read();
                        while (sw.Peek() != ' ')
                            name += (char)sw.Read();
                        sw.Read();
                        while (sw.Peek() != ' ')
                            surname += (char)sw.Read();
                        sw.Read();
                        while (sw.Peek() != '.' && sw.Peek() != ' ')
                            salary = salary * 10 + sw.Read() - 48;
                        if (sw.Read() == '.')
                        {
                            int k = 1;
                            while (sw.Peek() != ' ')
                            {
                                k *= 10;
                                salary += (double)(sw.Read() - 48) / k;
                            }
                            sw.Read();
                        }
                        while (sw.Peek() != '\n' && sw.Peek() != '\r' && !sw.EndOfStream)
                            type += (char)sw.Read();
                        type = type.ToLower();
                        if (type.Equals("hour"))
                            result.Add(new Hour(id, name, surname, salary));
                        else if (type.Equals("month"))
                            result.Add(new Month(id, name, surname, salary));
                        else
                        {
                            Console.WriteLine("An error occured during reading file");
                            throw new IOException();
                        }
                        sw.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        static void Print(List <Employee> collection)
        {
            string file = @"D:\Downloads\eng\out.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(file, false, System.Text.Encoding.Default))
                {
                    foreach (var w in collection)
                        sw.WriteLine(w.id + " "  + "  " + w.name + " " + " " + w.surname + " " + w.countSalary());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {

            List<Employee> workers = ReadEmployees();

            foreach (var w in workers)
               Console.WriteLine(w.id + " " + " " + w.countSalary() + "  "+ w.name + " " + " " + w.surname);
            Console.WriteLine("\n");
            var result = (from worker in workers
                         orderby worker.countSalary() descending, worker.name ascending
                         select worker).ToArray();
           
            for (int i = 0; i < 5 && i < workers.Count; i++)
                Console.WriteLine(result[i].name + " " + result[i].surname);

            if (workers.Count > 3)
                for (int i = workers.Count - 3; i < workers.Count; i++)
                    Console.WriteLine(result[i].id);
            else
                foreach (var w in result)
                    Console.WriteLine(w.id);

            Print(workers);

            Console.ReadKey();
    }
    }
}