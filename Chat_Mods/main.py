import asyncio
import json
import re
import websockets

# Global variables for server addresses
WAIFUVERSE_ENVIRONMENT_SERVER_ADDRESS = "localhost"
WAIFUVERSE_ENVIRONMENT_SERVER_PORT = 8765

clients = {"WaifuverseEnvironment": None}

async def handle_client(websocket, path, client_type):
    global clients

    print(f"Client connected: {websocket.remote_address} ({client_type})")
    clients[client_type] = websocket

    while True:
        input = await websocket.recv()
        print(f"Received message from {client_type}: {input}")

        if client_type == "WaifuverseEnvironment":
            output = handle_incoming_text(input)
            if clients["WaifuverseEnvironment"]:
                await clients["WaifuverseEnvironment"].send(output)
                print(f"Output sent to WaifuverseEnvironment: {output}")

        else:
            output = "Unknown client type"

async def start_server():
    waifuverse_server = await websockets.serve(lambda websocket, path: handle_client(websocket, path, "WaifuverseEnvironment"), WAIFUVERSE_ENVIRONMENT_SERVER_ADDRESS, WAIFUVERSE_ENVIRONMENT_SERVER_PORT)
    waifuverse_address = waifuverse_server.sockets[0].getsockname()
    print(f"WaifuverseEnvironment server started at ws://{waifuverse_address[0]}:{waifuverse_address[1]}")

    await waifuverse_server.wait_closed()

def remove_extra_new_lines(input_string):
    input_string = input_string.encode('ascii', 'ignore').decode('ascii')
    input_string = ''.join(filter(lambda x: x.isprintable(), input_string))
    input_string = re.sub(r'\n+', '\n', input_string)
    input_string = input_string.strip()

    return input_string

def extract_text_between_quotes(text):
    if '"' not in text:
        return text

    quoted_text = re.findall(r'"([^"]*)"', text)
    result = ' '.join(quoted_text)
    return result

def handle_incoming_text(text):
    #handle incomign text here
    words = text
    return words

if __name__ == "__main__":
    asyncio.run(start_server())
