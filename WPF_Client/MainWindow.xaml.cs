using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF_Client.WCF_Service;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWCF_ServiceCallback
    {
        WCF_ServiceClient client;
        Dictionary<string, string> message;
        Dictionary<string, bool> users;
        bool check = false;

        public MainWindow()
        {
            InitializeComponent();

            client = new WCF_ServiceClient(new InstanceContext(this));
        }

        /// <summary>
        /// Событие авторизации
        /// </summary>
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            users = new Dictionary<string, bool>();
            message = new Dictionary<string, string>();

            check = client.Authorisation(userNameTB.Text, passwordTB.Password, ref message, ref users);

            if (check)
            {                
                loginGrid.Visibility = Visibility.Hidden;
                // меняем размер окна и включаем третью оболочку.
                Width = 830;
                Height = 675;
                messengerGrid.Visibility = Visibility.Visible;

                foreach (var item in users)
                {
                    usersListView.Items.Add(item.Key);
                }

                currentUserField.Text = userNameTB.Text;
            }
            else
            {
                MessageBox.Show("Неправильно логин или пароль.");
            }
        }

        /// <summary>
        /// Событие перехода с окна авторизации на окно регистрации
        /// </summary>
        private void toRegistrateWindowButton_Click(object sender, RoutedEventArgs e)
        {
            loginGrid.Visibility = Visibility.Hidden;
            regGrid.Visibility = Visibility.Visible;
        }


        /// <summary>
        ///  Событие регистрации нового пользователя в сети
        /// </summary>
        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            // проверяем заполнение полей и выбор пола
            if ((bool)mRButton.IsChecked && loginTB.Text.Length > 3 && passTB.Text.Length > 3 && passTB.Text.Length < 16)
            {
                check = client.Registration(loginTB.Text, true, passTB.Text);
            }
            else if ((bool)fRButton.IsChecked && loginTB.Text.Length > 3 && passTB.Text.Length > 3 && passTB.Text.Length < 16)
            {
                // в методе регистрации стоит проверка на существование логина в БД
                check = client.Registration(loginTB.Text, false, passTB.Text);
            }
            else
            {
                MessageBox.Show("Одно из полей не заполненно!\nЗаполните все поля для регистрации");
                return;
            }

            // если регистрация прошла успешно - поздравляем и работаем дальше. В противном случае просим сменить логин.
            if (check)
            {
                MessageBox.Show("Регистрация прошла успешно");
                regGrid.Visibility = Visibility.Hidden;
                loginGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Пользователь с таким именем уже существует");
            }
        }

        /// <summary>
        /// Событие возврата из окна регистрации в окно логина. ( если случайно нажали на кнопку регистрации или вдруг вспомнили логин или пароль.
        /// </summary>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            regGrid.Visibility = Visibility.Hidden;
            loginGrid.Visibility = Visibility.Visible;
        }

        private void usersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (message.Count > 0 && message.ContainsKey((string)usersListView.SelectedItem))
            {
                messagesListBox.Items.Clear();
                messagesListBox.Items.Add(message[(string)usersListView.SelectedItem]);
            }
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            messengerGrid.Visibility = Visibility.Hidden;
            Height = 600;
            Width = 600;

            // убираем из списка нашего пользователя для возможности повторного входа
            //client.Disconnect(userNameTB.Text);

            users.Clear();
            message.Clear();
            usersListView.Items.Clear();

            loginGrid.Visibility = Visibility.Visible;
        }

        private void sendMessagesTexBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (usersListView.SelectedItem != null)
                {
                    client.SendMessage(userNameTB.Text, usersListView.SelectedItem.ToString(), sendMessagesTexBox.Text.ToString());

                    sendMessagesTexBox.Text = "";
                }
                else
                {
                    MessageBox.Show("Виберите пользователя которому хотите отправить сообщение");
                }

            }
        }

        public void MessageCallBack(string answer)
        {
            messagesListBox.Items.Add(answer);
        }

        private void DeleteMessageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            bool check;
            if(usersListView.SelectedValue != null)
            {
                check = client.RemoveUser(usersListView.SelectedItem.ToString(), userNameTB.Text);

                switch (check)
                {
                    case true:
                        {
                            usersListView.Items.Remove(usersListView.SelectedItem);
                            MessageBox.Show("Пользователь успешно удалён");
                        }
                        
                        break;
                    case false:
                        MessageBox.Show("Пользователь успешно удалён");
                        break;
                    default:
                        break;
                }
            }           
        }

        private void SetAdminButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SetUser_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void loginTB_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            loginTB.Text = "";
            loginTB.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void passTB_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            passTB.Text = "";
            passTB.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
