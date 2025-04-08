using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using ChatCommun;
using Azure.AI.OpenAI.Chat;

namespace ChatCommun
{
    public class OpenAIClass
    {
        private Uri endpoint = new Uri("<aquí url de open ai>");
        private string deploymentName = "<aquí el deploy del modelo>";
        private string apiKey = "<aquí la llave de OpenAi>";


        public string RecibeRespuesta(string prompt)
        {

            AzureOpenAIClient azureClient = new(
                endpoint,
                new AzureKeyCredential(apiKey)
            );
            ChatClient chatClient = azureClient.GetChatClient(deploymentName);

            #pragma warning disable AOAI001 // Suppress the diagnostic warning  


            var requestOptions = new ChatCompletionOptions();
            requestOptions.AddDataSource(new AzureSearchChatDataSource()
            {
                Endpoint = AzureAISearchClass.searchServiceName,
                IndexName = AzureAISearchClass.nombreindice,
                Authentication = DataSourceAuthentication.FromApiKey(AzureAISearchClass.adminApiKey),
            });


            List<ChatMessage> messages = new List<ChatMessage>()
            {
                new SystemChatMessage("You are a helpful assistant and always your answer is in spanish"),
                new UserChatMessage(prompt),
            };
            var response = chatClient.CompleteChat(messages, requestOptions);
            return response.Value.Content[0].Text;
        }
    }
}
