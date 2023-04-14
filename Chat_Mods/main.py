import asyncio
import json

import websockets

# Global variables for server addresses
KITSUNE_SERVER_ADDRESS = "localhost"
KITSUNE_SERVER_PORT = 8765
MACAQUE_SERVER_ADDRESS = "localhost"
MACAQUE_SERVER_PORT = 8000

clients = {"Kitsune": None, "Macaque": None}

async def handle_client(websocket, path, client_type):
    global clients

    print(f"Client connected: {websocket.remote_address} ({client_type})")
    clients[client_type] = websocket

    while True:
        input = await websocket.recv()
        print(f"Received message from {client_type}: {input}")

        if client_type == "Kitsune":
            print("sending to Macaque")
            output = input
            if clients["Macaque"]:
                await clients["Macaque"].send(output)
        elif client_type == "Macaque":
            msg = json.loads(input)
            isGenerating = bool(msg['isGenerating'])
            output = remove_extra_new_lines(msg['content'])
            #remove the RP ** lines
            output = extract_text_between_quotes(output)

            if not isGenerating:
                if clients["Kitsune"]:
                    await clients["Kitsune"].send(output)
                    print(f"Output sent to Kitsune: {output}")
            else:
                print("output generating")
                print(output)
        else:
            output = "Unknown client type"

async def start_server():
    kitsune_server = await websockets.serve(lambda websocket, path: handle_client(websocket, path, "Kitsune"), KITSUNE_SERVER_ADDRESS, KITSUNE_SERVER_PORT)
    kitsune_address = kitsune_server.sockets[0].getsockname()
    print(f"Kitsune server started at ws://{kitsune_address[0]}:{kitsune_address[1]}")

    macaque_server = await websockets.serve(lambda websocket, path: handle_client(websocket, path, "Macaque"), MACAQUE_SERVER_ADDRESS, MACAQUE_SERVER_PORT)
    macaque_address = macaque_server.sockets[0].getsockname()
    print(f"Macaque server started at ws://{macaque_address[0]}:{macaque_address[1]}")

    await asyncio.gather(kitsune_server.wait_closed(), macaque_server.wait_closed())

import re
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
if __name__ == "__main__":
    asyncio.run(start_server())
