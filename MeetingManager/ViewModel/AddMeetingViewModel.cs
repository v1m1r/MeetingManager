using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingManager.Models;

namespace MeetingManager
{
    public class AddMeetingViewModel
    {
        static Model _model;
        public Meeting Meeting { get; set; }
        public User User { get; set; }
        public AddMeetingViewModel()
        {
            _model = Model.getInstance();
            User = new User();
            var _user = _model.GetUsers();

            Meeting = new Meeting();
            var _meeting = _model.GetMeeting();

            Console.WriteLine("* Для создания новой встречи необходимо войти в систему *");
            Console.Write("Введите Ваше имя:");
            User.Name = Console.ReadLine();

            //Проверим наличие регистрации у пользователя
            try
            {
                var userName = _user.Where(x => x.Name == User.Name).Select(y => y.Name).First();
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Пользователь с именем '{0}' не существует",User.Name));
            }

            Console.Clear();
            var userID = _user.Where(x => x.Name == User.Name).Select(y => y.ID).First();
            Console.WriteLine("Добро добро пожаловать '{0}'! Ваш ID = '{1}'", User.Name, userID.ToString());
            Console.Write("Укажите время начала: ");
            Meeting.StartTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Укажите время окончания: ");
            Meeting.EndTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Укажите должность: ");
            User.JobTitile = Console.ReadLine();
        }
    }
}
