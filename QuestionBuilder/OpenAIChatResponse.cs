using Azure.AI.OpenAI;
using OpenAI.Chat;
using QuestionBuilder.DTO;
namespace QuestionBuilder
{

    public class OpenAIChatResponse
    {

        public static List<Question> GenerateQuestions(string text)
        
       {
            try
            {
                AzureOpenAIClient azureClient = new(
                new Uri("https://hexavarsity-secureapi.azurewebsites.net/api/azureai"),
                new Azure.AzureKeyCredential("912ab40a6f64cf92"));//912ab40a6f64cf92"

                ChatClient chatClient = azureClient.GetChatClient("gpt-4o-mini");

                    OpenAI.Chat.ChatCompletion chat = chatClient.CompleteChat(new[] {
                    new UserChatMessage(""""
                        Give me only questions
                        which answers are containg in given text after dash. give multiple choice options beside it with right answer.
                        each question must have 4 options a,b,c,d. give in json format.
                        JSON FORMAT(
                        [
                            {
                                "Desc": "question",
                                "Options": {
                                    "OptionA": "Option A",
                                    "OptionB": "Option B",
                                    "OptionC": "Option C",
                                    "OptionD": "Option D"
                                },
                                "CorrectAnswer": "OptionB"
                            }
                        ]
                        )
                        -
                        """" + text)
                     });
                string json = chat.Content[0].Text;
                json = json.Replace("```json", "");
                json = json.Replace("```", "");

                return GetQuestionBank(json);
            }
            catch (Exception ex) {
                throw ex;
            
            }
          
        }

        private static List<Question> GetQuestionBank(string questonJSON)
        {
            if (!string.IsNullOrEmpty(questonJSON))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Question>>(questonJSON);
            }
            throw new Exception("question Json is empty");
        }
    }
}
