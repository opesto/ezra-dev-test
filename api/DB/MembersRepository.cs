using System;
using System.Collections.Generic;
using System.Linq;

using EzraTest.Models;

using Microsoft.Data.Sqlite;


namespace EzraTest.DB
{
    public class MembersRepository : IMembersRepository
    {
        private string _connectionString;

        public MembersRepository(string connectionString)
        {
            _connectionString = $"Data Source={connectionString}";
        }

        /// <inheritdoc />
        public IEnumerable<Member> GetMembers()
        {
            return ExecuteQuery("SELECT * FROM MEMBERS", (reader) =>
            {
                return new Member
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                };
            });
        }

        /// <inheritdoc />
        public Member GetMember(Guid id)
        {
            return ExecuteQuery($"SELECT * FROM MEMBERS WHERE Id = '{id:N}'", (reader) =>
            {
                return new Member
                {
                    Id = Guid.Parse(reader.GetString(0)),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                };
            }).FirstOrDefault();
        }

        /// <inheritdoc />
        public void AddMember(Member member)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                String query = "INSERT INTO MEMBERS (Id,Name,Email) VALUES (@Id,@Name,@Email)";
                var command = connection.CreateCommand();
                command.Parameters.AddWithValue("@Id", member.Id);
                command.Parameters.AddWithValue("@Name", member.Name);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.CommandText = query;
                int result = command.ExecuteNonQuery();
                // Check Error
                if(result < 0) {
                    Console.WriteLine("Error inserting data into Database!");
                }
            }
        }

        /// <inheritdoc />
        public void UpdateMember(Guid id, Member member)
        {
             using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                String query = "UPDATE MEMBERS SET Name = @Name, Email = @Email WHERE Id = @Id";
                var command = connection.CreateCommand();
                command.Parameters.AddWithValue("@Name", member.Name);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@Id", id);
                command.CommandText = query;
                int result = command.ExecuteNonQuery();
                // Check Error
                if(result < 0) {
                    Console.Write("Error inserting data into Database!");
                }
                connection.Close();
            }
        }

        /// <inheritdoc />
        public void DeleteMember(Guid id)
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                String query = "DELETE FROM MEMBERS WHERE Id = @Id";
                var command = connection.CreateCommand();
                command.Parameters.AddWithValue("@Id", id);
                command.CommandText = query;
                int result = command.ExecuteNonQuery();
                // Check Error
                if(result < 0) {
                    Console.Write("Error inserting data into Database!");
                }
                connection.Close();
            }
        }

        private IEnumerable<T> ExecuteQuery<T>(string commandText, Func<SqliteDataReader, T> func)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = commandText;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return func(reader);
                    }
                }
            }
        }
    }
}