import asyncio
import websockets



async def handle_client(websocket, path):
    print(f"Client connected: {websocket.remote_address}")
    while True:
        input = await websocket.recv()

        #the input you receive is a
        print(f"Received message from Waifuverse user : {input}")



        #this is where you take the waifuverse output and make your own prompt


        #this is where you put the output of your custom generation
        #you could send this to anywhere for input, and get ouptu

        #send it to API, OpenAI

        #scrape a chrome or firefox using greesemonkey

        #send to Local, Pymelion, Laama, ect


        #please see the docs for the apropraite formating


        #replace this with your own output. The waifu only wants the string return.
        # However this will be more complex in the future.

        #todo, break up into a json. so that can return #verblal, nonverbal, and context update.


        output = "The server hears you."



        await websocket.send(output)


async def start_server():
    #this is only for a local implementation. So it can connect to the waifuverse app.

    #you may need to change the local host or port to something else
    #I speculate you can use a IP to stream across the network. Eg like airlink. but AirGPT


    server = await websockets.serve(handle_client, "localhost", 8765)
    address = server.sockets[0].getsockname()  # get the server address and port
    print(f"Server started at ws://localhost:{address[1]}")
    await asyncio.Future()  # run forever


if __name__ == "__main__":
    asyncio.run(start_server())