using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using Plugins;

var kernelSettings = KernelSettings.LoadSettings();

var builder = Kernel.CreateBuilder();
builder.Services.AddLogging(c => c.SetMinimumLevel(LogLevel.Information).AddDebug());
builder.Services.AddChatCompletionService(kernelSettings);

Kernel kernel = builder.Build();

//Adding Bing Connector
string bingKey = "your bing key";
var bingConnector = new BingConnector(bingKey);
var plugin = new WebSearchEnginePlugin(bingConnector);
kernel.ImportPluginFromObject(plugin, "BingPlugin");

// Create the chat history
ChatHistory chatMessages = [];

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Loop till we are cancelled
while (true)
{
    // Get user input
    System.Console.Write("User > ");
    chatMessages.AddUserMessage(Console.ReadLine()!);

    // Get the chat completions
    OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
    };

    // Get the response from the AI
    var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
        chatMessages,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);


    // Print the chat completions
    ChatMessageContent? chatMessageContent = null;
    await foreach (var content in result)
    {
        System.Console.Write(content);
        if (chatMessageContent == null)
        {
            System.Console.Write("Assistant > ");
            chatMessageContent = new ChatMessageContent(
                content.Role ?? AuthorRole.Assistant,
                content.ModelId!,
                content.Content!,
                content.InnerContent,
                content.Encoding,
                content.Metadata);
        }
        else
        {
            chatMessageContent.Content += content;
        }
    }
    System.Console.WriteLine();
    if (chatMessageContent != null)
    {
        chatMessages.AddMessage(chatMessageContent.Role, chatMessageContent.Content!);
    }
}
