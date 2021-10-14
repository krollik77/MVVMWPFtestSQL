using MVVMWPF.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMWPF.Model
{
    public static class DataWorker
    {
        //получить все отделы эта часть которая возвращает события
        public static List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }
        //получить все позиции
        public static List<Position> GetAllPositions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }
        //получить всех сотрудников
        public static List<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.ToList();
                return result;
            }
        }


        // 1 строчка кода //создать отдел
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует"; // локальная переменная результата
            using (ApplicationContext db = new ApplicationContext()) // подключение к БД
            {
                //проверяем сущесвует ли отдел
                bool checkIsExist = db.Departments.Any(el => el.Name == name);
                if (!checkIsExist)
                {
                    Department newDepartment = new Department { Name = name }; // после проверки условия создаем отдел департамента
                    db.Departments.Add(newDepartment);
                    db.SaveChanges(); // сохранение БД
                    result = "Сделано!";
                }
                return result;
            }
        }
        //содать позицию
        public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем сущесвует ли позиция
                bool checkIsExist = db.Positions.Any(el => el.Name == name && el.Salary == salary);
                if (!checkIsExist)
                {
                    Position newPosition = new Position
                    {
                        Name = name,
                        Salary = salary,
                        MaxNumber = maxNumber,
                        DepartmentId = department.Id
                    };
                    db.Positions.Add(newPosition);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //создать сотрудника
        public static string CreateUser(string name, string surName, string phone, Position position)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Users.Any(el => el.Name == name && el.SurName == surName && el.Position == position);
                if (!checkIsExist)
                {
                    User newUser = new User
                    {
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }


        //удаление отдел
        public static string DeleteDepartment(Department department)
        {
            string result = "Такого отела не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = "Сделано! Отдел " + department.Name + " удален";
            }
            return result;
        }
        //удаление позицию
        public static string DeletePosition(Position position)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check position is exist
                db.Positions.Remove(position);
                db.SaveChanges();
                result = "Сделано! Позиция " + position.Name + " удалена";
            }
            return result;
        }
        //удаление сотрудника
        public static string DeleteUser(User user)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = "Сделано! Сотрудник " + user.Name + " уволен";
            }
            return result;
        }

        //редактирование отдел
        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Такого отела не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                db.SaveChanges();
                result = "Сделано! Отдел " + department.Name + " изменен";
            }
            return result;
        }
        //редактирование позицию
        public static string EditPosition(Position oldPosition, string newName, int newMaxNumber, decimal newSalary, Department newDepartment)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxNumber = newMaxNumber;
                position.DepartmentId = newDepartment.Id;
                db.SaveChanges();
                result = "Сделано! Позиция " + position.Name + " изменена";
            }
            return result;
        }
        //редактирование сотрудника
        public static string EditUser(User oldUser, string newName, string newSurName, string newPhone, Position newPosition)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                User user = db.Users.FirstOrDefault(p => p.Id == oldUser.Id);
                if (user != null)
                {
                    user.Name = newName;
                    user.SurName = newSurName;
                    user.Phone = newPhone;
                    user.PositionId = newPosition.Id;
                    db.SaveChanges();
                    result = "Сделано! Сотрудник " + user.Name + " изменен";
                }
            }
            return result;
        }


    }

}
