using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MeetingManager.Models;
using MeetingManager.View;

namespace MeetingManager.ViewModel
{
    public class UserPersonalAccountViewModel
    {
        static Model _model;
        public User User { get; set; }
        public Meeting Meeting { get; set; }
        public UserPersonalAccountViewModel()
        {
            _model = Model.getInstance();
            User = new User();
            var _user = _model.GetUsers();

            Meeting = new Meeting();
            var _meeting = _model.GetMeeting();

            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.WriteLine("*** Для создания новой встречи необходимо войти в систему ***");
            Console.ResetColor();
            Console.WriteLine("Введите Ваше имя:");
            User.Name = Console.ReadLine();

            //Проверим наличие регистрации у пользователя
            try
            {
                var userName = _user.Where(x => x.Name == User.Name).Select(y => y.Name).First();
                if(userName!=null)
                {
                    new UserPersonalAccountView(userName);
                }
            }
            catch
            {
                throw new Exception(string.Format("Пользователь с именем'{0}' в системе не найден. Необходима регистрация",User.Name));
            }
          
        }
    }

}