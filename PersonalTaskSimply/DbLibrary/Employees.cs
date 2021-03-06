//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        public Employees()
        {
            this.Task = new HashSet<Task>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateBirth { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public float Salary { get; set; }
        public int Rate { get; set; }
        public Nullable<int> erk { get; set; }
        public Nullable<int> pkp { get; set; }
        public Nullable<int> ekr { get; set; }
        public Nullable<int> ems { get; set; }
        public Nullable<int> skr { get; set; }
    
        public virtual ICollection<Task> Task { get; set; }

        public void GetRate()
        {
            Rate = (int)((double)(erk + pkp + ekr + ems + skr) / 5);
        }

        public override string ToString()
        {
            return Name + " " + LastName;
        }

        public string ForTextBoxToString()
        {
            string str = "Id: " + Id + "\r\n" +
                "Name: " + Name + "\r\n" +
                "LastName: " + LastName + "\r\n" +
                "DateBirth: " + DateBirth.ToShortDateString() + "\r\n" +
                "Adress: " + Adress + "\r\n";
            if (Phone != "")
            {
                str += "Phone: " + Phone + "\r\n";
            }
            str += "Position: " + Position + "\r\n" +
                "Salary: " + Salary + "\r\n" +
                "erk: " + erk + "\r\n" +
                "pkp: " + pkp + "\r\n" +
                "ekr: " + ekr + "\r\n" +
                "ems: " + ems + "\r\n" +
                "skr: " + skr + "\r\n" +
                "Rate: " + Rate + "\r\n";

            return str;
        }
    }
}
