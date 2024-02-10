using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingManager.Models;
using MeetingManager.ViewModel;

namespace MeetingManager.ViewModel
{
    //ViewModels
    public class AddUserWindowViewModel
    {
        static Model _model;

        public User User { get; set; }
        public AddUserWindowViewModel()//Должна быть форма WPF
        {
            _model = Model.getInstance();
            User = new User();
            var _user = _model.GetUsers();

            Console.Write("* Укажите имя: ");
            User.Name = Console.ReadLine();

            Console.Write("* Укажите фамилию: ");
            User.Surname = Console.ReadLine();

            Console.Write("* Укажите должность: ");
            User.JobTitile = Console.ReadLine();

            if (User.Name == "" || User.Surname == "" || User.JobTitile == "")
            {
                throw new Exception(string.Format("Поля Имя, Фамилия, должность являются обязательными для заполнения"));
            }
            if (_model.AddUser(User))//Добавление пользователя в модели
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Пользователь с именем '{0}' создан!", User.Name);
                Console.ResetColor();
            }

        }
    }
}
