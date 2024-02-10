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
    public class AddMeetingView
    {
        static Model _model;
        public Meeting Meeting { get; set; }
        public User User { get; set; }
        public static ObservableCollection<MeetingViewModel> MeetingAll { get; set; }

        public AddMeetingView(int UserID)
        {
            _model = Model.getInstance();
            User = new User();
            var _user = _model.GetUsers();
            Meeting = new Meeting();
            var _meeting = _model.GetMeeting();

            MeetingAll = new ObservableCollection<MeetingViewModel>(_meeting.Select(x => new MeetingViewModel { Meeting = x, User = _user.FirstOrDefault(y => y.ID == x.UserID) }).Where(z => z.Meeting.UserID ==UserID ));

            Console.Write("Укажите время начала: ");
            Meeting.StartTime = DateTime.Parse(Console.ReadLine());

            //Проверка, что создаваемая встреча не может быть раньше текущей даты
            if (DateTime.Now > Meeting.StartTime)
            {
                throw new Exception(string.Format("* Дата начала должна не должна быть ранее {0}", DateTime.Now.ToString()));
            }

            //Проверка по всем встречам пользователя на наличие пересечений во времени
            foreach (var mt in MeetingAll)
            {
                if (Meeting.StartTime == mt.Meeting.StartTime)
                {
                    
                    Console.WriteLine(string.Format("ID встречи = '{0}' | Дата начала '{1}' | Дата окончания '{2}' ",  mt.Meeting.ID, mt.Meeting.StartTime,mt.Meeting.EndTime));
                    throw new Exception(string.Format("на указанную дату {0} уже запланирована встреча! Выберите другое время.", Meeting.StartTime));
                }
            }

            Console.Write("* Укажите время окончания: ");
            Meeting.EndTime = DateTime.Parse(Console.ReadLine());

         

            Console.Write("* Время за которое нужно уведомить пользователя (!указывается в минутах): ");
            Meeting.UserNotification = Int32.Parse(Console.ReadLine());
            Meeting.UserID = UserID;

            

            if (_model.AddMeeting(Meeting))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Встреча успешно добавлена в расписание!");
                Console.ResetColor();
            }
        }
    }
}
