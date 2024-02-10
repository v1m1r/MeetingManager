using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingManager.Models;
using MeetingManager.ViewModel;

namespace MeetingManager.View
{
    public class UserPersonalAccountView
    {
        static Model _model;
        public static ObservableCollection<MeetingViewModel> Meeting { get; set; }
        public UserPersonalAccountView(string UserName)
        {
            _model = Model.getInstance();
            var _user = _model.GetUsers();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("==== Добро пожаловать в личный кабинет ====");
            Console.ResetColor();
            var userID = _user.Where(x => x.Name == UserName).Select(y => y.ID).First();
            Console.WriteLine("Привет '{0}'! Ваш ID => '{1}'", UserName, userID.ToString());
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(CheckMeetingNotice(UserName,_model));
            Console.ResetColor();

            int userInput = 0;
            do
            {
                userInput = DisplayMenu();
                if (userInput == 1) { Clear(); ViewModelBase.ViewAllMeeting(); }
                if (userInput == 2) { Clear(); ViewModelUserPersonal.AddMeeting(userID); }
                if (userInput == 3) { Clear(); ViewModelUserPersonal.UserListMeetings(UserName); }
                if (userInput == 4) { Clear(); ViewModelUserPersonal.DeleteMeeting(UserName); }
                if (userInput == 5) { Clear(); ViewModelUserPersonal.EditMeetingView(UserName); }
                if (userInput == 6) { Clear(); ViewModelUserPersonal.SaveFileMeeting(UserName); }
                if (userInput == 7) { Console.Clear(); }
            } while (userInput != 7);
        }

        static public int DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---------");
            Console.WriteLine("Меню личного кабинета");
            Console.WriteLine("---------");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("1. Просмотр всего расписания");
            Console.WriteLine("____");
            Console.WriteLine("2. Добавить встречу в расписание");
            Console.WriteLine("____");
            Console.WriteLine("3. Посмотреть список моих встреч");
            Console.WriteLine("____");
            Console.WriteLine("4. Удалить встречу");
            Console.WriteLine("____");
            Console.WriteLine("5. Редактировать встречу");
            Console.WriteLine("____");
            Console.WriteLine("6. Сохранить список встреч в файл");
            Console.WriteLine("____");
            Console.WriteLine("7. Выход из личного кабинета");
            var result = Console.ReadLine();
            int MenuItem = 0;
            try
            {
                MenuItem = Convert.ToInt32(result);
            }
            catch (System.FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка!!! Для выбора пункта меню используйте цифры!");
                Console.ResetColor();
            }
            return MenuItem;
        }

        static void Clear()
        { Console.Clear(); }

        public string CheckMeetingNotice(string UserName, Model _model)
        {
            var _user = _model.GetUsers();
            var _meeting = _model.GetMeeting();
            Meeting = new ObservableCollection<MeetingViewModel>(_meeting.Select(x => new MeetingViewModel { Meeting = x, User = _user.FirstOrDefault(y => y.ID == x.UserID) }).Where(z => z.User.Name == UserName));
            foreach (var mt in Meeting)
            {
                TimeSpan item = new TimeSpan(0,0, mt.Meeting.UserNotification, 0);              
                DateTime dtNow = DateTime.Now.Add(item);
                if (mt.Meeting.StartTime.ToShortTimeString() == dtNow.ToShortTimeString())
                {
                    return string.Format("Предупреждение о начале встречи = ID '{0}' | Начало встречи '{1}'", mt.Meeting.ID,mt.Meeting.StartTime.ToString());
                }
            }
            return "";
        }
    }
}
