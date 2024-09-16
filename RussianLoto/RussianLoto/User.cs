namespace RussianLoto
{
    public class User
    {

        private string login;

        private string password;

        private int win_amount;

        private int balance;

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public User(string login, string password, int win_amount, int balance)
        {
            this.login = login;
            this.password = password;
            this.win_amount = win_amount;
            this.balance = balance;
        }   

        public int getBalance() { return this.balance; }

        public string getLogin() { return this.login; }

        public int getWinAmount() { return this.win_amount; }

        public void setWinAmount(int win_amount) { this.win_amount = win_amount; }

        public void setBalance(int balance) {  this.balance = balance; }

        public bool cardPaid() {
            if ((this.balance -= 50) > 0)
            {
                this.balance -= 50;
                return true;
            }
            return false;
        }

        public void isWon(int jackpot)
        {
            this.balance += jackpot;
            this.win_amount += balance;
        }
    }
}
