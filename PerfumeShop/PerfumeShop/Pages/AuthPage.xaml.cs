using PerfumeShop.Classes;
using PerfumeShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PerfumeShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            bool isAuth = false;

            if (string.IsNullOrWhiteSpace(TBLogin.Text) || string.IsNullOrWhiteSpace(TBPassword.Text))
                MessageBox.Show("Проверьте заполненность полей", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    using (PefumeShopEntities db = new PefumeShopEntities())
                    {
                        foreach (var user in db.User)
                        {
                            if (user.Login.Replace("\r\n", "") == TBLogin.Text)
                            {
                                if (user.Password.Replace("\r\n", "") == TBPassword.Text)
                                {
                                    Manager.curUser = user;
                                    isAuth = true;
                                    MessageBox.Show("Успешный вход!");
                                    if (user.Role == "Администратор")
                                        Manager.MainFrame.Navigate(new AdminPage());
                                    else if (user.Role == "Менеджер")
                                        Manager.MainFrame.Navigate(new ManagerPage());
                                    else
                                        Manager.MainFrame.Navigate(new ClientPage());
                                }
                                else
                                {
                                    MessageBox.Show("Неверный пароль");
                                }
                            }
                        }
                        if (!isAuth)
                            MessageBox.Show("Пользователя с данным логином не существует");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
            
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ClientPage());
        }
    }
}
