using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingManager.Models;
using MeetingManager.ViewModel;
using MeetingManager.View;
namespace MeetingManager
{
    public class ViewModelUserPersonal
    {
        static Model _model;

        public static ObservableCollection<MeetingViewModel> Meeting { get; set; }
        public static void UserListMeetings(string UserName)
        {
            try
            {
                _model = Model.getInstance();
       
                var _user = _model.GetUsers();
                var _meeting = _model.GetMeeting();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Список встреч пользователя '{0}'",UserName);
                Console.ResetColor();
                Meeting = new ObservableCollection<MeetingViewModel>(_meeting.Select(x => new MeetingViewModel { Meeting = x, User = _user.FirstOrDefault(y => y.ID == x.UserID) }).Where(z=>z.User.Name==UserName));
                var header = String.Format("|{0,4}|{1,4}|{2,20}|{3,20}|{4,18}|{5,18}|","ID встречи", "Фамилия", "Имя", "Начало встречи", "Конец встречи", "За сколько предупредить(минуты)");
                Console.WriteLine(header);
                foreach (var mt in Meeting)
                {
                    Console.WriteLine(string.Format("|{0,10}|{1,4}|{2,20}|{3,20}|{4,18}|{5,18}|",mt.Meeting.ID, mt.User.Surname, mt.User.Name, mt.Meeting.StartTime, mt.Meeting.EndTime, mt.Meeting.UserNotification));
                }
                Console.WriteLine();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("При получении расписания произошла ошибка {0}", ex.Message));
                Console.ResetColor();
            }
        }

        public static void AddMeeting(int UserID)
        {
            try
            {
                new AddMeetingView(UserID);
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("При создании расписания произошла ошибка {0}", ex.Message));
                Console.ResetColor();
            }
        }

        public static void DeleteMeeting(string UserName)
        {
            try
            {
                new DeleteMeetingView(UserName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("При удалении встречи {0} произошла ошибка", ex.Message));
                Console.ResetColor();
            }
        }

        public static void EditMeetingView(string UserName)
        {
            try
            {
                new EditMeetingView(UserName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("При редактировании встречи {0} произошла ошибка", ex.Message));
                Console.ResetColor();
            }
        }

        public static void SaveFileMeeting(string UserName)
        {
            try
            {
                new SaveFileMeetingViewModel(UserName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("Во время сохранения файла {0} произошла ошибка", ex.Message));
                Console.ResetColor();
            }
        }

    }
}
