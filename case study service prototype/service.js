const express = require('express');
const http = require('http');
const WebSocket = require('ws');
const app = express();
app.use('/markers', express.static("markers"));
app.use('/json', express.static("json"));
app.use('/vital-signs-monitor', express.static("vital-signs-monitor"));
const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

wss.on('connection', function connection(ws, req) {
  console.log('[CLIENT CONNECTED] %o:%o', req.socket.remoteAddress, req.socket.remotePort);
});

server.on('request', (request, res) => {
  console.log("[REQUEST FROM] %o:%o", res.socket.remoteAddress, res.socket.remotePort);
});

server.listen(process.env.PORT || 8080, () => {
  console.log(`[SERVER STARTED] ${server.address().port}`);
});

let sys = 120;
let dia = 80;
let map = 93;
let spo = 100;
let respirationRate = 14;
let temperature = 36;
let pulse = 72;
let value = 1;

function getRandomArbitrary(min, max, floor) {
  return floor == true ? Math.floor(Math.random() * (max - min) + min) : Number.parseFloat(Math.random() * (max - min) + min).toFixed(1);
}

function updateParams() {
  sys = getRandomArbitrary(sys - value, sys + value, true);
  dia = getRandomArbitrary(dia - value, dia + value, true);
  map = getRandomArbitrary(map - value, map + value, true);
  spo = getRandomArbitrary(spo - value, spo + value, true);
  respirationRate = getRandomArbitrary(respirationRate - value, respirationRate + value, true);
  temperature = getRandomArbitrary(temperature - value, temperature + value, false);
  pulse = getRandomArbitrary(pulse - value, pulse + value, true);
}

function sendUpdateToClients() {
  updateParams();
  const json = JSON.stringify({ sys: sys, dia: dia, map: map, spo: spo, respirationRate: respirationRate, temperature: temperature, pulse: pulse });
  wss.clients.forEach(function each(client) {
    if (client.readyState === WebSocket.OPEN) {
      client.send(json);
    }
  });
}

setInterval(sendUpdateToClients, 8000);