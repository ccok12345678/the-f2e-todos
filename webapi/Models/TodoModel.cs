using System.Data.Common;
using Npgsql;

namespace webapi.Models;

public class TodoModel
{
    readonly NpgsqlConnection connection;
    public TodoModel(string connectStr)
    {
        connection = new NpgsqlConnection(connectStr);
    }

    public class TodoItem
    {
        public int id { get; set; }
        public string task { get; set; } = "";
        public string? comment { get; set; }
        public DateTime? deadline { get; set; }
        public bool completed { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
    }

    static string getAllSqlStr = "SELECT * FROM todos;";
    public List<TodoItem> GetAllTodoItems()
    {
        var todoItemList = new List<TodoItem>();

        using (NpgsqlCommand cmd = new NpgsqlCommand(getAllSqlStr, connection))
        {
            connection.Open();

            try
            {
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var todoItem = new TodoItem
                    {
                        id = (int)reader["id"],
                        task = (string)reader["task"],
                        comment = reader["comment"].ToString(),
                        deadline =  string.IsNullOrEmpty(reader["deadline"].ToString()) ?
                            null : (DateTime)reader["deadline"],
                        completed = (bool)reader["completed"],
                        createDate = (DateTime)reader["create_date"],
                        updateDate = (DateTime)reader["update_date"]
                    };

                    todoItemList.Add(todoItem);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"get all todos error: {e.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        return todoItemList;
    }


}
