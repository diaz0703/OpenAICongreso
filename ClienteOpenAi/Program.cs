using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using ChatCommun;
using Azure.AI.OpenAI.Chat;


var endpoint = new Uri("<aquí url de open ai>");
var deploymentName = "<aquí el deploy del modelo>";
var apiKey = "<aquí la llave de OpenAi>";

AzureOpenAIClient azureClient = new(
    endpoint,
    new AzureKeyCredential(apiKey));
ChatClient chatClient = azureClient.GetChatClient(deploymentName);

#pragma warning disable AOAI001 // Suppress the diagnostic warning  


var requestOptions = new ChatCompletionOptions();


requestOptions.AddDataSource(new AzureSearchChatDataSource()
{
    Endpoint = AzureAISearchClass.searchServiceName,
    IndexName = AzureAISearchClass.nombreindice,
    Authentication = DataSourceAuthentication.FromApiKey(AzureAISearchClass.adminApiKey),
});


// Necesito un hotel que se llama twin
string _propmt = "Tienes algún hotel llamado Twin";



List<ChatMessage> messages = new List<ChatMessage>()
{
    new SystemChatMessage("You are a helpful assistant and always your answer is in spanish"),
    new UserChatMessage(_propmt),
};



var response = chatClient.CompleteChat(messages, requestOptions);


System.Console.WriteLine(response.Value.Content[0].Text);


#pragma warning restore AOAI001 // Restore the diagnostic warning
