using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using WCF_Library_Server.DB_Context;
using WCF_Library_Server.Model;

namespace WCF_Library_Server
{
    // типо синглтон - не даёт возможности одновременно запустить два сервера
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WCF_Service : IWCF_Service
    {
        List<User> users;
        List<Message> Messages;


        /// <summary>
        /// Метод авторизации, получает логин&пароль пользователя после чего хеширует его и сравнивает с хешированным паролем из БД
        /// если успех - отгружает имена всех юзеров для общения и все переписки пользователя
        /// </summary>
        public bool Authorisation(string login, string password, ref IDictionary<string, string> messages, ref IDictionary<string, bool> usersInDB)
        {

            // достаём юзера из базы если он существует
            User user = users.FirstOrDefault(u => u.UserName == login);

            if (user != null && MyHash.Authefication(user.HashedPassword, password))
            {

                user.operationContext = OperationContext.Current;

                // передача данных о именах и гедере
                foreach (var item in users)
                {
                    //что бы не добавлять себя
                    if(login != item.UserName)
                        usersInDB.Add(item.UserName, item.Gender);
                }

                //TODO передача пользователю сообщения
                if (Messages.FirstOrDefault(m => m.ToUser == user.UserName) != null)
                {
                    var messagesFromDB = Messages.FirstOrDefault(m => m.ToUser == user.UserName);
                    // если есть диалоги - отгружаем по ключу имя пользователя/сообщение 
                    messages.Add(messagesFromDB.FromUser, messagesFromDB.MessageData);
                }

                return true;

            }
            return false;
        }

        public void RemoveUser(string userName)
        {
            var user = users.FirstOrDefault(i => i.UserName == userName);

            // удалять юзеров может только супер админ
            if (user != null && user.Rank.RankID == 3)
            {
                users.Remove(user);
                using (var db = new DBContext())
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }

        }

        public bool Registration(string userName, bool gender, string password)
        {
            try
            {
                using (var usersDB = new DBContext())
                {

                    if (users.FirstOrDefault(u => u.UserName == userName) == null)
                    {
                        var user = new User()
                        {
                            UserName = userName,
                            Gender = gender,
                            // хеширование пароля происходит в статическом классе
                            HashedPassword = MyHash.HashedPassword(password),
                            // всем вновь созданным юзерам присваевается 1 ранг - юзер
                            Rank = usersDB.UserRanks.FirstOrDefault(r => r.RankID == 1),
                        };

                        // добавляем в БД
                        usersDB.Users.Add(user);
                        // в серверный список
                        users.Add(user);

                        usersDB.SaveChanges();

                        // новый пользователь успешно зарегестрирован
                        return true;
                    }
                    else
                    {
                        //пользователь с таким именем уже существует
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        public void SendMessage(string FromUserLogin, string ToLogin, string messageData)
        {            
            var msg = new Message()
            {
                FromUser = FromUserLogin,
                ToUser = ToLogin,
                MessageData = messageData
            };

            CheckMessage(msg);

            // обнуляем обьекты
            msg = null; 
            

            // меняем местами айдишники - дабы переписка была у обоих пользователей
            msg = new Message()
            {
                FromUser = ToLogin,
                ToUser = FromUserLogin,
                MessageData = messageData
            };

            CheckMessage(msg);

            if (users.FirstOrDefault(u => u.UserName == ToLogin) != null)
            {
                // отрправка юзеру
                users.FirstOrDefault(u => u.UserName == ToLogin).operationContext.GetCallbackChannel<IWCF_ServiceChatCallBack>()
                    .MessageCallBack($"{DateTime.Now.ToShortTimeString()}| {users.FirstOrDefault(u => u.UserName == FromUserLogin).UserName}: {messageData}");

                // отпечатка у отправившего
                users.FirstOrDefault(u => u.UserName == FromUserLogin).operationContext.GetCallbackChannel<IWCF_ServiceChatCallBack>()
                    .MessageCallBack($"{DateTime.Now.ToShortTimeString()}| Me: {messageData}");
            }

        }

        /// <summary>
        /// В данном методе делается следующее - если у пользователя есть данный дилог к ниму добавляется новое сообщение,
        /// в противном случае - в его список сообщений добавляется новый обьект класса Message
        /// </summary>
        private void CheckMessage(Message msg)
        {
            //// переменная для проверки на существования
            var message = Messages.FirstOrDefault(m => m.ToUser == msg.ToUser);

            if (message == null)
            {
                //добавляем новое сообщение в лог сообщений

                using (var db = new DBContext())
                {
                    db.Messages.Add(msg);
                }
            }
            else
            {
                using (var db = new DBContext())
                {
                    db.Messages.FirstOrDefault(m => m.ToUser == msg.ToUser).MessageData += "\n" + DateTime.Now.ToShortTimeString().ToString() + "\t" + msg.MessageData;
                }
            }
        }

        // базовый конструктор
        public WCF_Service()
        {
            using (var userDB = new DBContext())
            {
                users = new List<User>();
                Messages = new List<Message>();

                users.AddRange(userDB.Users);
                Messages.AddRange(userDB.Messages);

                //строки для инициализации DB
                //List<UserRank> ranks = new List<UserRank>()
                //{
                //    new UserRank(){ RankName = "user"},
                //    new UserRank(){ RankName = "admin"},
                //    new UserRank(){ RankName = "super admin"}
                //};

                //userDB.UserRanks.AddRange(ranks);

                //userDB.SaveChanges();

            }
            //так как сервер по идее работает 24/7 - мы сразу загружаем всех пользователей и сообщений в оперативку - и далее обращаемся к БД
            // лишь при изменениях.
        }
    }
}
