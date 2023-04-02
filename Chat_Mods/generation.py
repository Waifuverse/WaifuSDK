

def TraitsToPrompt(personalityTraits : [], gender, chatHistory : [], name, relationship, mood):
    #this function generates waht is sent to the GPT.
    #you can change this to whatever you want. Just be mindfull of how you want to capture.
    


    if len(chatHistory) > 25:  # we only keep 25 to keep the chat manageable
        chatHistory = chatHistory[-25:]

    chatHistory = [entry.replace("W:", name + ":") for entry in chatHistory]
    print("HistoryList:")
    chat = "".join(chatHistory)
    if len(chat) > 2000:  # limit chat length to 2000 characters, so that it fits in the gpt it may need to be furhter redueced
        chat = chat[-2000:]
    print(chat)





    prompt = "This is a uplifting, positive, supportive, verbal chat conversation between you and your %s named %s" \
             "%s is talkative,"  %(relationship, name, gender);
    prompt += ', '.join(personalityTraits)
    prompt += " and is feeling very "+ mood

    #prompt += "The context is %s. " %(context)
    #prompt += name + "'s backstory is %s. " %(backstory)
    # prompt+= HumanName



    prompt += chat
    prompt += name+":"

    print("fullPrompt :"+ prompt)

    return prompt

# uncomment this to send to openAI

# import openai
#use your own openAI key
#openai.api_key =""
# def SendChatTo_OpenAI(inputText):
#
#     response = openai.Completion.create(
#         model="text-davinci-003",
#         prompt=inputText,
#         temperature=0.9,
#         max_tokens=100,
#         top_p=0,
#         frequency_penalty=0,
#         presence_penalty=0.9,
#         stop=[":"]
#     )
#     #return the text
#     print(response)
#     outputText = response["choices"][0]['text']
#     return outputText