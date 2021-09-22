const WebSocket = require('ws');
const express = require('express');
const http = require('http');
const app = express();
const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

server.use(express.static('/markers'));
server.use(express.static('/json'));

wss.on('connection', function connection(ws, req) {
    console.log('Client connected, remote socket: %o:%o', req.socket.remoteAddress, req.socket.remotePort)
    ws.on('message', function incoming(data) {
        console.log('Received: %o', data)
        console.log('Sending broadcast...')
        wss.clients.forEach(function each(client) {
        if (client !== ws && client.readyState === WebSocket.OPEN) {
            client.send(data);
        }
    });
  });
});



server.listen(process.env.PORT || 8080, () => {
    console.log(`Server started on port ${server.address().port}`);
});