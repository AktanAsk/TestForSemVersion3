 // Проблема 1: SQL-инъекция через конкатенацию строки

 using System.Diagnostics;
 using System.Security.Cryptography;
 using System.Text;

 Console.WriteLine("Введите имя пользователя:");
        string userInput = Console.ReadLine();
        string query = "SELECT * FROM Users WHERE username = '" + userInput + "'"; // SQL-инъекция

        using (SqlConnection connection = new SqlConnection("connection_string"))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(); // Уязвимость SQL-инъекции
        }

        // Проблема 2: Небезопасное выполнение команды через пользовательский ввод
        Console.WriteLine("Введите команду для выполнения:");
        string userCommand = Console.ReadLine();
        Process.Start("cmd.exe", "/c " + userCommand); // Возможность выполнения произвольных команд

        // Проблема 3: Небезопасное сравнение строк (уязвимо к тайминг-атакам)
        string correctPassword = "superSecretPassword";
        Console.WriteLine("Введите пароль:");
        string inputPassword = Console.ReadLine();

        if (correctPassword == inputPassword) // Простое сравнение строк уязвимо к тайминг-атакам
        {
            Console.WriteLine("Пароль верный.");
        }
        else
        {
            Console.WriteLine("Пароль неверный.");
        }

        // Проблема 4: Хранение чувствительных данных в строке
        string apiKey = "12345-ABCDE"; // Строки не удаляются из памяти, что может привести к утечке данных
        Console.WriteLine("API ключ: " + apiKey);

        // Проблема 5: Использование слабого хеширования MD5
        Console.WriteLine("Введите текст для хеширования:");
        string input = Console.ReadLine();
        using (MD5 md5 = MD5.Create()) // MD5 устарел и небезопасен для криптографии
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            Console.WriteLine("MD5 Hash: " + BitConverter.ToString(hashBytes).Replace("-", ""));
        }