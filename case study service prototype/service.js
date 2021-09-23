const express = require('express');
const http = require('http');
const WebSocket = require('ws');
const app = express();
app.use('/markers', express.static("markers"));
app.use('/json', express.static("json"));
const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

wss.on('connection', function connection(ws, req) {
    console.log('[CLIENT CONNECTED] %o:%o', req.socket.remoteAddress, req.socket.remotePort)
});

server.on('request', (request, res) => {
  console.log("[REQUEST FROM] %o:%o", res.socket.remoteAddress, res.socket.remotePort);
});

server.listen(process.env.PORT || 8080, () => {
    console.log(`[SERVER STARTED] ${server.address().port}`);
});