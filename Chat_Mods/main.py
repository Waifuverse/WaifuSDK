import asyncio
import json

import websockets

# Global variables for server addresses
KITSUNE_SERVER_ADDRESS = "localhost"
KITSUNE_SERVER_PORT = 8765
MACAQUE_SERVER_ADDRESS = "localhost"
MACAQUE_SERVER_PORT = 8000

async def handle_client(websocket, path, client_type):
    print(f"Client connected: {websocket.remote_address} ({client_type})")
    while True:
        input = await websocket.recv()

        #the input you receive is a
        #print(f"Received message from {client_type}: {input}")


        # Perform different actions based on client type
        if client_type == "Kitsune":
            # Handle Kitsune client input
            output = "Kitsune output"
        elif client_type == "Macaque":
            # Handle Macaque client input
            output = "Macaque output"
            msg = json.loads(input)

            output = remove_extra_new_lines(msg['message'])
            print(output)
        else:
            # Handle other client types
            output = "Unknown client type"

        #await websocket.send(output)


async def start_server():
    # Start the server for Kitsune clients
    kitsune_server = await websockets.serve(lambda websocket, path: handle_client(websocket, path, "Kitsune"), KITSUNE_SERVER_ADDRESS, KITSUNE_SERVER_PORT)
    kitsune_address = kitsune_server.sockets[0].getsockname()
    print(f"Kitsune server started at ws://{kitsune_address[0]}:{kitsune_address[1]}")

    # Start the server for Macaque clients
    macaque_server = await websockets.serve(lambda websocket, path: handle_client(websocket, path, "Macaque"), MACAQUE_SERVER_ADDRESS, MACAQUE_SERVER_PORT)
    macaque_address = macaque_server.sockets[0].getsockname()
    print(f"Macaque server started at ws://{macaque_address[0]}:{macaque_address[1]}")

    await asyncio.gather(kitsune_server.wait_closed(), macaque_server.wait_closed())

import re
def remove_extra_new_lines(input_string):


    input_string = input_string.encode('ascii', 'ignore').decode('ascii')

    # Remove non-printable characters
    input_string = ''.join(filter(lambda x: x.isprintable(), input_string))

    # Remove extra new lines
    input_string = re.sub(r'\n+', '\n', input_string)

    # Remove leading and trailing white space
    input_string = input_string.strip()

    return input_string


if __name__ == "__main__":
    asyncio.run(start_server())
