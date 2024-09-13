using System;
using System.Collections.Generic;
namespace RussianLoto
{
    public class Player
    {

        private string login; // имя

        private string password; // пароль

        private decimal win_amount; // сумма выигрыша

        private DateTime birth_date; // дата рождения

        private List<Card> cards;

        public Player(long id, string login, string password, decimal win_amount, DateTime birth_date)
        {
            this.login = login;
            this.password = password;
            this.win_amount = win_amount;
            this.birth_date = birth_date;
        }

        public Player(string login, string password)
        {
            this.login = login;
            this.password = password;
            win_amount = 0;
            birth_date = DateTime.Now;
        }
    }
}