var express = require('express');
var http = require('http');
var socketIo = require('socket.io');

var app = express();
var server = http.Server(app);
server.listen(5000);
var io = socketIo(server);

var luckeyNumber = Math.floor(Math.random() * 101);

io.on('connection', function (socket) {
    console.log('client connected');

    socket.on('message', function (data) {
        console.log(data);
        var playerData = JSON.stringify(data);
        var Data = JSON.parse(playerData);
        var number = Data.number;
        var name = Data.name;
        if (number == luckeyNumber) {
            socket.emit('youWin');
            socket.broadcast.emit('haveWinner', data);
            ResetNumber();

        } else if (number > luckeyNumber) {
            socket.emit('moreThan');
        } else if (number < luckeyNumber) {
            socket.emit('lessThan')
        }
    });

    socket.on('disconnect', function () {
        console.log('player disconnect');
    });
});

console.log('server start');
console.log(luckeyNumber);
function ResetNumber() {
    newLuckeyNumber = Math.floor(Math.random() * 101);
    luckeyNumber = newLuckeyNumber;
    console.log(luckeyNumber);
}