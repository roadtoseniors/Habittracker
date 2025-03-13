using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WpfApp7
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Метод для выполнения SQL-запросов
        private void ExecuteNonQuery(string sql)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Метод для получения данных
        private List<T> ExecuteQuery<T>(string sql, Func<NpgsqlDataReader, T> mapper)
        {
            var result = new List<T>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(mapper(reader));
                        }
                    }
                }
            }
            return result;
        }

        // Регистрация нового пользователя
        public void RegisterUser(string username, string password)
        {
            string sql = $"INSERT INTO users (name, goal) VALUES ('{username}', '{password}')";
            ExecuteNonQuery(sql);
        }

        // Проверка логина пользователя
        public bool LoginUser(string username, string password)
        {
            string sql = $"SELECT * FROM users WHERE name = '{username}' AND goal = '{password}'";
            var users = ExecuteQuery(sql, reader => new
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Goal = reader.GetString(2)
            });

            return users.Count > 0;
        }

        public int? GetUserId(string username)
        {
            string sql = $"SELECT id FROM users WHERE name = '{username}'";
            var userIds = ExecuteQuery(sql, reader => reader.GetInt32(0));

            // Возвращаем первый найденный ID или null, если пользователь не найден
            return userIds.FirstOrDefault();
        }

        // Получение всех привычек для пользователя
        public List<Habit> GetHabits(int userId)
        {
            string sql = $"SELECT * FROM habits WHERE user_id = {userId}";
            return ExecuteQuery(sql, reader => new Habit
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Name = reader.GetString(2),
                Description = reader.GetString(3)
            });
        }

        // Добавление новой привычки
        public void AddHabit(int userId, string name, string description)
        {
            string sql = $"INSERT INTO habits (user_id, name, description) VALUES ({userId}, '{name}', '{description}')";
            ExecuteNonQuery(sql);
        }
    }
}
