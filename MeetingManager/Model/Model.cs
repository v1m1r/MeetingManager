using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MeetingManager.ViewModel;

namespace MeetingManager.Models
{
    public class Model
    {
        static Model _instance;

        readonly List<User> _user;
        readonly List<Meeting> _meeting;

        public static ObservableCollection<MeetingViewModel> Meeting { get; set; }

        Model()
        {
            _user = new List<User>
            {
                new User {ID=1, Name="Виктор",Surname="Мирошкин",JobTitile="Разработчик"},
                new User {ID=2, Name="Иван", Surname="Иванов",JobTitile="Руководитель отдела"},
                new User { ID=3, Name="Анатолий",Surname="Петров",JobTitile="Подбор персонала"},
            };

            _meeting = new List<Meeting>
            {
                new Meeting { ID=1,UserID=1,StartTime=new DateTime(2024,01,17,12,00,00),EndTime=new DateTime(2024,01,17,13,00,00),UserNotification=30 },
                new Meeting { ID=2,UserID=3,StartTime=new DateTime(2024,01,16,16,00,00), EndTime=new DateTime(2024,01,16,17,00,00), UserNotification =50},
                new Meeting { ID=3,UserID=3,StartTime=new DateTime(2024,01,18,14,00,00), EndTime=new DateTime(2024,01,17,15,00,00),UserNotification=90},
                new Meeting { ID=4,UserID=1,StartTime=new DateTime(2024,01,18,12,00,00),EndTime=new DateTime(2024,01,18,13,00,00),UserNotification=24 },
                new Meeting { ID=5,UserID=1,StartTime=new DateTime(2024,12,10,17,00,00),EndTime=new DateTime(2024,12,10,18,00,00),UserNotification=30 },
                new Meeting { ID=6,UserID=1,StartTime=new DateTime(2024,01,18,14,34,00),EndTime=new DateTime(2024,01,18,18,00,00),UserNotification=2 },
                new Meeting { ID=7,UserID=2,StartTime=new DateTime(2024,01,18,11,18,00),EndTime=new DateTime(2024,01,18,12,00,00),UserNotification=20 },
                new Meeting { ID=8,UserID=2,StartTime=new DateTime(2024,01,19,12,15,00),EndTime=new DateTime(2024,01,19,16,00,00),UserNotification=10 },
            };
        }


        public static Model getInstance()
        {
            if (_instance == null)
                _instance = new Model();
            return _instance;
        }

        public List<User> GetUsers()
        {
            return _user.ToList();
        }

        public List<Meeting> GetMeeting()
        {
            return _meeting.ToList();
        }

        public bool AddUser(User user)
        {
            if (!CheckUser(user))
                return false;
            user.ID = _user.Max(x => x.ID) + 1;
            _user.Add(user);
            return true;
        }

        public bool CheckUser(User user)
        {
            if (user == null)
                throw new Exception(string.Format("Нет пользователей"));
            else
            {
                var _user = GetUsers();
                foreach (var users in _user)
                {
                    if (users.Name == user.Name && users.Surname == user.Surname && users.JobTitile == user.JobTitile)
                    {
                        throw new Exception(string.Format("пользователь с фамилией '{0}', именем '{1}', на должности '{2}' существует", user.Surname, user.Name, user.JobTitile));
                    }
                }
            }
            return true;
        }

        public bool AddMeeting(Meeting meeting)
        {
            //if (!CheckMeeting(meeting))
                meeting.ID = _meeting.Max(x => x.ID) + 1;
            _meeting.Add(meeting);
            return true;
        }

        //public bool CheckMeeting(Meeting meeting)
        //{

        //}

        public bool DeleteMeeting(Meeting meeting)
        {
            var error = "";
            if (meeting == null)
                error = "Null meeting";
            else
            {
                var findLimit = _meeting.FirstOrDefault(x => x.ID == meeting.ID);

                _meeting.Remove(findLimit);
               
             }
            if (error.Length == 0)
                return true;
            else
                return false;
        }

        public bool EditMeeting(Meeting meeting)
        {
            //if (!CheckUser(user))
            //    return false;
            var FindMeeting = _meeting.FirstOrDefault(x => x.ID == meeting.ID);

            FindMeeting.StartTime = meeting.StartTime;
            FindMeeting.EndTime = meeting.EndTime;
            FindMeeting.UserNotification = meeting.UserNotification;
            return true;
        }

        public bool SaveToXMLMeeting(ObservableCollection<MeetingViewModel> Meeting,string Get_date)
        {
            StringBuilder xml = new StringBuilder("<Meeting>");
            xml.Append(string.Format("\n<UserMeetingInfo UserID=\"{0}\" UserName=\"{1}\" UserSurname=\"{2}\" UserJobtitle=\"{3}\">\n", Meeting.Select(x => x.User.ID).First().ToString(), Meeting.Select(x => x.User.Name).First().ToString(), Meeting.Select(x => x.User.Surname).First().ToString(), Meeting.Select(x => x.User.JobTitile).First().ToString()));
            foreach (var mt in Meeting.Where(x=>x.Meeting.StartTime.ToShortDateString()==Get_date))
            {
                xml.Append(string.Format("\n<MeetingInfo StartMeeting=\"{0}\" EndMeeting=\"{1}\" UserNotificationMeeting=\"{2}\"></MeetingInfo>\n", mt.Meeting.StartTime.ToShortDateString(), mt.Meeting.EndTime.ToShortDateString(), mt.Meeting.UserNotification));

            }
            xml.Append("\n</UserMeetingInfo>");
            xml.Append("\n</Meeting>");
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml.ToString());
            xdoc.Save("UserMeetingInfo.xml");

            return true;
        }
    }

    }

