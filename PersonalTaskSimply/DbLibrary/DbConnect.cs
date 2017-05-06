using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLibrary
{
    public class DbConnect
    {
        private killEntities dbLink;

        public killEntities DbLink
        {
            get { return dbLink; }
        }

        public DbConnect()
        {
            dbLink = new killEntities();
        }

        public bool AddEmployee(Employees employee)
        {
            try
            {
                dbLink.Employees.Add(employee);
                dbLink.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool EditEmployee(Employees employee)
        {
            Employees singleEmployee =
                (from employeedb in dbLink.Employees
                 where employeedb.Id == employee.Id
                 select employeedb).FirstOrDefault();
            try
            {
                singleEmployee.Name = employee.Name;
                singleEmployee.LastName = employee.LastName;
                singleEmployee.DateBirth = employee.DateBirth;
                singleEmployee.Adress = employee.Adress;
                singleEmployee.Phone = employee.Phone;
                singleEmployee.Position = employee.Position;
                singleEmployee.Salary = employee.Salary;
                dbLink.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteEmployee(Employees employee)
        {
            try
            {
                dbLink.Employees.Remove(employee);
                dbLink.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool AddTask(Task newTask)
        {
            try
            {
                dbLink.Task.Add(newTask);
                dbLink.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool EditTask(Task newTask)
        {
            Task singleTask =
                (from task in dbLink.Task
                 where task.Id == newTask.Id
                 select task).FirstOrDefault();
            try
            {
                singleTask.Title = newTask.Title;
                singleTask.DateBegin = newTask.DateBegin;
                singleTask.DateEnd = newTask.DateEnd;
                singleTask.Customer = newTask.Customer;
                singleTask.PhoneCustomer = newTask.PhoneCustomer;

                foreach (Employees item in newTask.Employees)
                {
                    string str = item.Id.ToString();
                }

                singleTask.Employees = newTask.Employees;
                dbLink.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteEmployee(Task task)
        {
            try
            {
                dbLink.Task.Remove(task);
                dbLink.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<Task> GetAllTask()
        {
            List<Task> tasks =
                    (from task in dbLink.Task
                     select task).ToList();

            return tasks;
        }
        
        public List<Employees> GetAllPerson()
        {
            List<Employees> employees =
                    (from employee in dbLink.Employees
                     select employee).ToList();

            return employees;
        }

        public Task GetTask(int id)
        {
            Task singleTask =
                (from task in dbLink.Task
                 where task.Id == id
                 select task).FirstOrDefault();

            return singleTask;
        }

        public Employees GetPerson(int id)
        {
            Employees singleEmployee =
                (from employee in dbLink.Employees
                 where employee.Id == id
                 select employee).FirstOrDefault();

            return singleEmployee;
        }
    }
}
