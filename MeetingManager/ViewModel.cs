using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingManager.Models;
using MeetingManager.ViewModel;

namespace MeetingManager
{
    public class ViewModelBase
    {
        static Model _model;
        public ViewModelBase()
        {
            _model = Model.getInstance();
        }
        public static ObservableCollection<MeetingViewModel> Meeting { get; set; }

        //Вывод всей доски расписания
        public static void ViewAllMeeting()
        {
            var _user = _model.GetUsers();
            var _meeting = _model.GetMeeting();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Общий список распианий");
            Console.ResetColor();
            Meeting = new ObservableCollection<MeetingViewModel>(_meeting.Select(x => new MeetingViewModel { Meeting = x, User = _user.FirstOrDefault(y => y.ID == x.UserID) }));
            var header = String.Format("|{0,2}|{1,10}|{2,10}|{3,20}|{4,20}|{5,30}|", "ID", "Фамилия", "Имя", "Начало встречи","Конец встречи","За сколько предупредить (мин.)");
            Console.WriteLine(header);
            foreach (var mt in Meeting)
            {
                Console.WriteLine(string.Format("|{0,2}|{1,10}|{2,10}|{3,20}|{4,20}|{5,30}|", mt.Meeting.ID, mt.User.Surname, mt.User.Name, mt.Meeting.StartTime, mt.Meeting.EndTime, mt.Meeting.UserNotification));
            }
        }

        //Добавление нового пользователя в систему
        public static void AddUSer()
        {
            try
            {
                new AddUserWindowViewModel();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("При добавлении пользователя произошла ошибка {0}",ex.Message));
                Console.ResetColor();
            }
        }

        //Добавление новой встречи в систему
        public static void AddMeeting()
        {
            try
            {
                new AddMeetingViewModel();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("Во время создание встречи произошла ошибка {0}", ex.Message));
                Console.ResetColor();
            }
        }

        //Показать список всех зарегистрированных пользователей
        public static void ViewAllUsers()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Список зарегистрированных пользователей в системе");
            Console.WriteLine();
            Console.ResetColor();
            var _user = _model.GetUsers();
            var header = String.Format("|{0,3}|{1,10}|{2,10}|{3,20}|", "ID", "Фамилия", "Имя", "Должность");
            Console.WriteLine(header);
            foreach (var u in _user)
            {
                Console.WriteLine(string.Format("{0,4}|{1,10}|{2,10}|{3,20}|", u.ID,u.Surname,u.Name,u.JobTitile));
            }
        }

        //Вход в систему
        public static void LoginUserAccount()
        {
            try
            {
                new UserPersonalAccountViewModel();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("Во время авторизации произошла ошибка {0}", ex.Message));
                Console.ResetColor();
            }
        }
    }
}
