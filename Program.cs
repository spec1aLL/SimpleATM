using System;

namespace LearnCSharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            decimal[] balances = { 1000, 5000, 20000 };
            decimal clientBalance = 0;
            int userIndex = -1;
            string[] pinCodes = { "1234", "4567", "8900" };
            int pinAttemps = 0;
            const int maxAttemps = 3;
            bool isCardBlocked = false;
            decimal[,] allUsersHistory = new decimal[3, 10];
            int[] allUsersHistoryCounts = { 0, 0, 0 };

            do
            {
                pinAttemps++;
                bool inMainMenu = true;

                Console.Write("Введите PIN-код: ");
                string pinCodeINput = Console.ReadLine();

                if (pinCodes.Contains(pinCodeINput))
                {
                    pinAttemps = 0;

                    for (int i = 0; i < pinCodes.Length; i++)
                    {
                        if (pinCodeINput == pinCodes[i])
                        {
                            userIndex = i;
                            break;
                        }
                    }

                    if (userIndex != -1)
                    {
                        clientBalance = balances[userIndex];
                    }
                    else
                    {
                        Console.WriteLine("Неверный PIN-код!");
                    }

                    while (inMainMenu)
                    {
                        Console.Clear();

                        Console.WriteLine("Добро пожаловать!");
                        Console.Write("""
                        1 - Снять
                        2 - Пополнить
                        3 - Проверить баланс
                        4 - История операций
                        5 — Выйти
                        Выберите пункт меню (1 - 5):
                        """);
                        string menuItemInput = Console.ReadLine();

                        if (int.TryParse(menuItemInput, out int menuItem))
                        {
                            switch (menuItem)
                            {
                                case 1:
                                    while (true)
                                    {
                                        Console.Clear();

                                        Console.Write("""
                                        Для возварта в главное меню нажмите клавишу Enter...

                                        Введите сумму кратную 100:
                                        """);
                                        string withdrawalInput = Console.ReadLine();

                                        if (string.IsNullOrWhiteSpace(withdrawalInput))
                                        {
                                            Console.Clear();

                                            Console.WriteLine("Отмена операции. Возврат в меню. Нажмите любую клавишу...");
                                            Console.ReadKey();

                                            break;
                                        }

                                        if (decimal.TryParse(withdrawalInput, out decimal withdrawal))
                                        {
                                            decimal commission = withdrawal * 0.025m;
                                            decimal sum = withdrawal + commission;

                                            if (sum > clientBalance)
                                            {
                                                Console.Clear();

                                                Console.WriteLine("Недостаточно средств на счете!");
                                                Console.WriteLine("Отмена операции. Возврат в меню. Нажмите любую клавишу...");
                                                Console.ReadKey();

                                                break;
                                            }

                                            if (withdrawal > 0 && withdrawal <= 50000 && withdrawal % 100 == 0)
                                            {
                                                long securityId = (long)withdrawal << 4;
                                                string issueType = ((long)withdrawal % 500) != 0 ? "Стандартная" : "Мелкими купюрами";

                                                clientBalance = clientBalance - sum;

                                                int currentUserHistoryCount = allUsersHistoryCounts[userIndex];
                                                int historyCapacity = 10;

                                                if (currentUserHistoryCount < historyCapacity)
                                                {
                                                    allUsersHistory[userIndex, currentUserHistoryCount] = -sum;
                                                    allUsersHistoryCounts[userIndex]++;
                                                }

                                                Console.WriteLine($"\n--- Чек операции Снятия ---");
                                                Console.WriteLine($"Время операции: {DateTime.Now}");
                                                Console.WriteLine($"Сумма снятия: {withdrawal:F2}");
                                                Console.WriteLine($"Комиссия банкомата: {commission:F2}");
                                                Console.WriteLine($"Сумма к списанию: {sum}. Операция произведена успешно.");
                                                Console.WriteLine($"Security ID операции: {securityId}");
                                                Console.WriteLine($"Тип выдачи: {issueType}");

                                                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                                                Console.ReadKey();

                                                break;
                                            }
                                            else
                                            {
                                                Console.Clear();

                                                Console.WriteLine("Некорректная сумма (от 100 до 50 000 и кратно 100)!");
                                                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться к снятию.");
                                                Console.ReadKey();

                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();

                                            Console.WriteLine("Ошибка! Введите целое число.");
                                            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться к снятию.");
                                            Console.ReadKey();

                                            continue;
                                        }
                                    }
                                    break;
                                case 2:
                                    while (true)
                                    {
                                        Console.Clear();

                                        Console.Write("""
                                        Для возврата в меню нажмите клавишу Enter...

                                        Введите сумму кратную 10:
                                        """);
                                        string replenishmentInput = Console.ReadLine();

                                        if (string.IsNullOrWhiteSpace(replenishmentInput))
                                        {
                                            Console.Clear();

                                            Console.WriteLine("Отмена операции. Возврат в меню. Нажмите любую клавишу...");
                                            Console.ReadKey();

                                            break;
                                        }

                                        if (decimal.TryParse(replenishmentInput, out decimal replenishment))
                                        {
                                            if (replenishment > 0 && replenishment <= 50000 && replenishment % 10 == 0)
                                            {
                                                long securityId = (long)replenishment << 2;

                                                clientBalance = clientBalance + replenishment;

                                                int currentUserHistoryCount = allUsersHistoryCounts[userIndex];
                                                int historyCapacity = 10;

                                                if (currentUserHistoryCount < historyCapacity)
                                                {
                                                    allUsersHistory[userIndex, currentUserHistoryCount] = replenishment;
                                                    allUsersHistoryCounts[userIndex]++;
                                                }

                                                Console.WriteLine($"\n--- Чек операции Пополнение ---");
                                                Console.WriteLine($"Время операции: {DateTime.Now}");
                                                Console.WriteLine($"Сумма пополнения: {replenishment:F2}");
                                                Console.WriteLine($"Операция произведена успешно. Деньги зачислены на ваш счет.");
                                                Console.WriteLine($"Security ID операции: {securityId}");

                                                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                                                Console.ReadKey();

                                                break;
                                            }
                                            else
                                            {
                                                Console.Clear();

                                                Console.WriteLine("Некорректная сумма!");
                                                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться к пополнению.");
                                                Console.ReadKey();

                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();

                                            Console.WriteLine("Ошибка! Введите целое число.");
                                            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться к пополнению.");
                                            Console.ReadKey();

                                            continue;
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.Clear();

                                    Console.WriteLine($"Ваш баланс: {clientBalance:F2} рублей.");

                                    Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                                    Console.ReadKey();

                                    break;
                                case 4:
                                    Console.Clear();

                                    Console.WriteLine($"--- История операций пользователя №{userIndex + 1} ---");

                                    if (allUsersHistoryCounts[userIndex] == 0)
                                    {
                                        Console.WriteLine("Операций еще не было.");
                                    }
                                    else
                                    {
                                        for (int i = 0; i < allUsersHistoryCounts[userIndex]; i++)
                                        {
                                            decimal currentOp = allUsersHistory[userIndex, i];
                                            string type = currentOp > 0 ? "Пополнение" : "Снятие";
                                            Console.WriteLine($"{i + 1}. {type}: {Math.Abs(currentOp):F2} рублей.");
                                        }
                                    }

                                    Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                                    Console.ReadKey();

                                    break;
                                case 5:
                                    Console.Clear();

                                    inMainMenu = false;

                                    break;
                                default:
                                    Console.Clear();

                                    Console.WriteLine("Некорректный пункт меню. Пожалуйста, выберите от 1 до 5.");
                                    Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                                    Console.ReadKey();

                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();

                            Console.WriteLine("Ошибка! Введите целое число.");
                            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                            Console.ReadKey();

                            continue;
                        }
                    }
                }
                else
                {
                    if (pinAttemps >= maxAttemps)
                    {
                        Console.Clear();
                        isCardBlocked = true;
                        Console.WriteLine("Превышено количество попыток.");
                    }
                    else
                    {
                        Console.WriteLine($"PIN-код неправильный. У вас осталось {maxAttemps - pinAttemps} попыток.\n");
                    }
                }
            }
            while (!isCardBlocked && pinAttemps < maxAttemps);
            Console.WriteLine("\nВаша карта заблокирована! Обратитесь в отделение банка для разблокировки карты.");
        }
    }
}