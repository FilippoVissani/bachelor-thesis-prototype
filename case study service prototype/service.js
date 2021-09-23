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

let sys = 120;//115-125
let dia = 80;//75-85
let spo = 97;//96-99
let respirationRate = 14;//16-20
let temperature = 36;//35-38
let pulse = 72;//60-120

function getRandomArbitrary(actual, min, max) {
 let rand = Number.parseFloat(Math.random() * 3 + 1).toFixed(1);
 rand = Math.random() < 0.5 ? parseFloat(rand)*parseFloat(-1) : parseFloat(rand)*parseFloat(1)
 let newVal = Math.floor(parseFloat(actual)+parseFloat(rand));
 return (newVal >= min && newVal <= max) ? newVal : actual;
}

function updateParams() {
  sys = getRandomArbitrary(sys, 115, 125, true);
  dia = getRandomArbitrary(dia, 75, 85, true);
  spo = getRandomArbitrary(spo, 96, 99, true);
  respirationRate = getRandomArbitrary(respirationRate, 16, 20, true);
  temperature = getRandomArbitrary(temperature, 35, 38, false);
  pulse = getRandomArbitrary(pulse, 60, 120, true);
}

function sendUpdateToClients() {
  updateParams();
  const json = JSON.stringify({ sys: sys, dia: dia, spo: spo, respirationRate: respirationRate, temperature: temperature, pulse: pulse });
  wss.clients.forEach(function each(client) {
    if (client.readyState === WebSocket.OPEN) {
      client.send(json);
    }
  });
}

setInterval(sendUpdateToClients, 3000);