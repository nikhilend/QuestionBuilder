using QuestionBuilder;
using QuestionBuilder.Data;
using QuestionBuilder.DTO;

Console.WriteLine("------------------QUESTION BUILDER-----------------------------");
Console.WriteLine("User Name : admin | Password : admin | for ADMIN Login---------");
Console.WriteLine("User Name : trainer | Password : trainer | for TRAINER Login---");
Console.WriteLine("User Name : employee | Password : admin | for EMPLOYEE Login---");
Console.WriteLine("---------------------------------------------------------------");

List<User> userData = UserData.GetUserData();
List<QuestionBank> questionBanks = new List<QuestionBank>();

// LOGIN
bool login = true;

while (login)
{
    try
    {

        Console.WriteLine("--------------------------HOME---------------------------------");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Exit");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("----Choose : ");
        var loginInput = Console.ReadLine();

        switch (loginInput)
        {
            case "1":
                Console.WriteLine("User Name : ");
                var userName = Console.ReadLine();
                Console.WriteLine("Password : ");
                var pass = string.Empty;
                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass = pass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        pass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                Console.WriteLine();
                //var password = Console.ReadLine();

                User user = GetUser(userData, userName, pass);

                if (user != null)
                {
                    switch (user.UserType)
                    {
                        case "A":
                            Admin();
                            break;
                        case "T":
                            Trainer();
                            break;
                        case "E":
                            Employee();
                            break;
                        case "":
                            Console.WriteLine("---------------------------------------------------------------");
                            Console.WriteLine("----------------Please Enter Some Input------------------------");
                            Console.WriteLine("---------------------------------------------------------------");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine("-------------------------User Not Found------------------------");
                    Console.WriteLine("---------------------------------------------------------------");
                }
                break;
            case "2":
                login = false;
                break;
            default:
                {
                    Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine("----------------Option Not available---------------------------");
                    Console.WriteLine("---------------------------------------------------------------");
                }
                break;
        }

    }
    catch (Exception e)
    {
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("---------Something Went wrong Please Login and try again-------");
        Console.WriteLine(e.Message);
        Console.WriteLine("---------------------------------------------------------------");

    }
   

}




void Employee()
{
    string inp;
    Console.WriteLine("---------------------------------------------------------------");
    Console.WriteLine("------------------------WELCOME EMPLOYEE-----------------------");
    Console.WriteLine("---------------------------------------------------------------");

    bool employee = true;

    while (employee)
    {
        Console.WriteLine("--------------------------EMPLOYEE HOME------------------------");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("1. Attempt Test");
        Console.WriteLine("2. See My Results");
        Console.WriteLine("3. Logout");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("------------Chose : ");
        inp = Console.ReadLine();

        switch (inp)
        {
            case "1":
                ShowAttemptQuestionBanks();
                break;
            case "2":
                ShowEmployeeResults();
                break;
            case "3":
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Logged Out-------------------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                employee = false;
                break;
            case "":
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Please Enter Some Input------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                break;
            default:
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Option Not available---------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                break;
        }
    }
}

void ShowEmployeeResults()
{
    Console.WriteLine("--------------------------EMPLOYEE RESULT----------------------");
    Console.WriteLine("---------------------------------------------------------------");
    bool isAnyAvailable = false;
    foreach (var ques in questionBanks)
    {
        if (ques.Attempted)
        {
            Console.WriteLine(ques.Id + ". " + ques.Name + " | Score : " + ques.UserScore);
            isAnyAvailable = true;
        }

    }
    if (!isAnyAvailable)
    {
        Console.WriteLine("No Test Attempted.");
    }
    Console.WriteLine("---------------------------------------------------------------");
}

void ShowAttemptQuestionBanks()
{
    int count = questionBanks.Count + 1;
    bool showQuestionBanks = true;
    int quesBankChoice = 0;
    int totalScore = 0;

    while (showQuestionBanks)
    {
        Console.WriteLine("--------------------------EMPLOYEE ATEEMPT---------------------");
        Console.WriteLine("---------------------------------------------------------------");
        if (questionBanks.Count == 0)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("------------No Question Banks Available------------------------");
            Console.WriteLine("---------------------------------------------------------------");
            showQuestionBanks = false;
            break;
        }
        foreach (QuestionBank ques in questionBanks)
        {
            Console.WriteLine(ques.Id + ". " + ques.Name);
        }

        Console.WriteLine(count + ". Go back");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("---- Choose which to attempt : ");
        string choiceA = Console.ReadLine();

        bool validInputA = int.TryParse(choiceA, out quesBankChoice);

        if (!validInputA) {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("----------------Please Enter Valid Input-----------------------");
            Console.WriteLine("---------------------------------------------------------------");
        }
        else if (quesBankChoice == count)
        {
            showQuestionBanks = false;
        }
        else if (quesBankChoice < count)
        {
            
            int i = 1;
            QuestionBank que = questionBanks.FirstOrDefault(x => x.Id == quesBankChoice);


            if(string.IsNullOrEmpty(choiceA))
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Please Enter Valid Answer----------------------");
                Console.WriteLine("---------------------------------------------------------------");
            }
            else
            {
                totalScore = 0;
                
               
                foreach (var question in que.Questions)
                {
                    bool validInput = true;
                    while (validInput)
                    {
                        Console.WriteLine("---------------------------------------------------------------");
                        Console.WriteLine(i + ". " + question.Desc);
                        Console.WriteLine("a) " + question.Options.OptionA);
                        Console.WriteLine("b) " + question.Options.OptionB);
                        Console.WriteLine("c) " + question.Options.OptionC);
                        Console.WriteLine("d) " + question.Options.OptionD);
                        Console.WriteLine("---------------------------------------------------------------");
                        Console.WriteLine("Choose option : ");
                        string answer = Console.ReadLine();
                        switch (answer)
                        {
                            case "a":
                            case "A":
                            case "1":
                                choiceA = "OptionA";
                                validInput = false;
                                break;
                            case "b":
                            case "B":
                            case "2":
                                choiceA = "OptionB";
                                validInput = false;
                                break;
                            case "c":
                            case "C":
                            case "3":
                                choiceA = "OptionC";
                                validInput = false;
                                break;
                            case "d":
                            case "D":
                            case "4":
                                choiceA = "OptionD";
                                validInput = false;
                                break;
                            default:
                                Console.WriteLine("---------------------------------------------------------------");
                                Console.WriteLine("----------------Please Enter Valid Option----------------------");
                                Console.WriteLine("---------------------------------------------------------------");
                                break;
                        }

                        if (choiceA.Equals(question.CorrectAnswer))
                        {
                            totalScore = totalScore + 5;
                        }
                        i++;
                    }
                    
                }
            }
            
            que.UserScore = totalScore;
            que.Attempted = true;
        }
        else
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("----------------Option Not available---------------------------");
            Console.WriteLine("---------------------------------------------------------------");
        }
    }
}


void Trainer()
{
    string inp;
    Console.WriteLine("---------------------------------------------------------------");
    Console.WriteLine("------------------------WELCOME TRAINER------------------------");
    Console.WriteLine("---------------------------------------------------------------");

    bool trainer = true;

    while (trainer)
    {
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("1. Create Question Bank");
        Console.WriteLine("2. See Created Question Banks");
        Console.WriteLine("3. Logout");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("------------Chose : ");
        inp = Console.ReadLine();

        switch (inp)
        {
            case "1":
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("-------PLEASE ENTER QUESTION BANK NAME : ");
                Console.WriteLine("---------------------------------------------------------------");

                string qbName = Console.ReadLine();

                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("-------PLEASE ENTER CURRICULUM TEXT : ");
                Console.WriteLine("---------------------------------------------------------------");
                
                string curriculum = Console.ReadLine();

                QuestionBank questionBank = new QuestionBank();

                questionBank.Id = questionBanks.Count + 1;
                questionBank.Name = qbName;
                questionBank.Curriculum = curriculum;

                questionBank.Questions = OpenAIChatResponse.GenerateQuestions(curriculum);

                questionBanks.Add(questionBank);

                PreviewQuestionBank(questionBank);
                Console.WriteLine("--------------------------QUESTION BANK CREATED SUCCESSFULLY---");

                break;
            case "2":
                ShowQuestionBanks();
                break;
            case "3":
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Logged Out-------------------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                trainer = false;
                break;
            case "":
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Please Enter Some Input------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                break;
            default:
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Option Not available---------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                break;
        }
    }
}

void ShowQuestionBanks()
{
    int count = questionBanks.Count + 1;
    int choiceN = 0;
    bool showQuestionBanks = true;

    while (showQuestionBanks)
    {
        Console.WriteLine("--------------------------SHOW QUESTION BANK-------------------");
        Console.WriteLine("---------------------------------------------------------------");
        if (questionBanks.Count == 0)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("------------No Question Banks Available------------------------");
            Console.WriteLine("---------------------------------------------------------------");
            showQuestionBanks =false;
            break;
        }
        foreach (QuestionBank ques in questionBanks)
        {
            Console.WriteLine(ques.Id + ". " + ques.Name);
        }

        Console.WriteLine(count + ". Go back");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("---- Choose which question bank to show : ");
        string choice = Console.ReadLine();

        bool validInput  = int.TryParse(choice, out choiceN);

        if (!validInput)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("----------------Please Enter Valid choice----------------------");
            Console.WriteLine("---------------------------------------------------------------");
        }
        else if (choiceN == count)
        {
            showQuestionBanks = false;
        }
        else if (choiceN < count && choiceN != 0)
        {
            QuestionBank que = questionBanks.FirstOrDefault(x => x.Id == Convert.ToInt32(choice));
            PreviewQuestionBank(que);
        }
        else
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("----------------Option Not available---------------------------");
            Console.WriteLine("---------------------------------------------------------------");
        }
    }
}

void PreviewQuestionBank(QuestionBank que)
{
    Console.WriteLine("---------------------------------------------------------------");
    Console.WriteLine(que.Name);
    Console.WriteLine("---------------------------------------------------------------");

    int i = 1;
    foreach(var q in que.Questions)
    {
        Console.WriteLine( i + ". " + q.Desc);
        Console.WriteLine("a) " + q.Options.OptionA);
        Console.WriteLine("b) " + q.Options.OptionB);
        Console.WriteLine("c) " + q.Options.OptionC);
        Console.WriteLine("d) " + q.Options.OptionD);
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("Correct Answer : " + q.CorrectAnswer);
        Console.WriteLine("---------------------------------------------------------------");
        i++;
    }
}

void Admin()
{
    string inp;
    Console.WriteLine("---------------------------------------------------------------");
    Console.WriteLine("------------------------WELCOME ADMIN--------------------------");
    Console.WriteLine("---------------------------------------------------------------");

    bool admin = true;

    while (admin)
    {
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("1. See Users");
        Console.WriteLine("2. See Trainer Question Banks");
        Console.WriteLine("3. See Employee Results");
        Console.WriteLine("4. Logout");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("------------Chose : ");
        inp = Console.ReadLine();

        switch (inp) { 
            case "1":
                foreach (var user in userData)
                {
                    Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine("| User Name : " + user.UserName  + " | User Type : " + user.UserType + " |");
                    Console.WriteLine("---------------------------------------------------------------");
                }
                break;
            case "2":
                ShowQuestionBanks();
                break;
            case "3":
                ShowEmployeeResults();
                break;
            case "4":
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Logged Out-------------------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                admin = false;
               break;
            case "":
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Please Enter Some Input------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                break;
            default:
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("----------------Option Not available---------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                break;
        }
    }
}

User GetUser(List<User> userData, string userName, string password)
{
    try
    {
        return userData.FirstOrDefault(x => x.UserName.Equals(userName) && x.Password.Equals(password));
    }

    catch(Exception e) {
    }
    return null;
}

