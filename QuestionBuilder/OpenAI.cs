using Azure;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using static System.Environment;

namespace QuestionBuilder
{
    internal class OpenAI
    {
        public void GetOpenAIResponse()
        {
           

            string endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            string key = GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

            var client = new OpenAIClient(
                new Uri(endpoint),
                new AzureKeyCredential(key));

            CompletionsOptions completionsOptions = new()
            {
                DeploymentName = "gpt-35-turbo-instruct",
                Prompts = { "When was Microsoft founded?" },
            };

            Response<Completions> completionsResponse = client.GetCompletions(completionsOptions);
            string completion = completionsResponse.Value.Choices[0].Text;
            Console.WriteLine($"Chatbot: {completion}");
        }
    }
}
