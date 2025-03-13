namespace WpfApp7
{
	public class userRepository
	{

		public static void addUser(users user)
		{

			string connection_query = "Server=bd-kip.fa.ru; Database=HabitTracker;UserId=sa;Password=1qaz!QAZ; Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"
			string sql_query = "INSERT INTO users (id, username, password) VALUES (@id, @username, @password)";


			try:

		}
}