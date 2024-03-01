// Copyright (c) Microsoft. All rights reserved.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

// Create a kernel
var builder = Kernel.CreateBuilder();
// Add a text or chat completion service using either:
// builder.Services.AddAzureOpenAIChatCompletion()
// builder.Services.AddAzureOpenAITextGeneration()
// builder.Services.AddOpenAIChatCompletion()
// builder.Services.AddOpenAITextGeneration()
builder.WithCompletionService();
builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Trace));

var kernel = builder.Build();

Console.Write("Your request: ");
string request = Console.ReadLine()!;

// 0.0 Initial prompt
//////////////////////////////////////////////////////////////////////////////////
string prompt = $"What is the intent of this request? {request}";

Console.WriteLine("0.0 Initial prompt");
Console.WriteLine(await kernel.InvokePromptAsync(prompt));


// 1.0 Make the prompt more specific
//////////////////////////////////////////////////////////////////////////////////
prompt = @$"What is the intent of this request? {request}
You can choose between SendEmail, SendMessage, CompleteTask, CreateDocument.";

Console.WriteLine("1.0 Make the prompt more specific");
Console.WriteLine(await kernel.InvokePromptAsync(prompt));

// 2.0 Add structure to the output with formatting (using Markdown and JSON)
//////////////////////////////////////////////////////////////////////////////////
prompt = @$"## Instructions
Provide the intent of the request using the following format:

```json
{{
    ""intent"": {{intent}}
}}
```

## Choices
You can choose between the following intents:

```json
[""SendEmail"", ""SendMessage"", ""CompleteTask"", ""CreateDocument""]
```

## User Input
The user input is:

```json
{{
    ""request"": ""{request}""
}}
```

## Intent";

Console.WriteLine("2.1 Add structure to the output with formatting (using Markdown and JSON)");
Console.WriteLine(await kernel.InvokePromptAsync(prompt));
