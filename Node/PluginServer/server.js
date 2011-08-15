var net = require("net");
var fs = require("fs");

var server = net.createServer(function (socket) {
    //socket.write("InteractionPluginServer\r\n");

    socket.setEncoding('utf8');

    socket.on("data",function(command){

        //get rid of the fucking \n
        command = command.substr(0,command.length-2);

        console.log(command);
        switch(command){
            case "r":
            case "requestPluginList":
                loadPluginList(socket);
                break;
            default:
                break;
        }

    });
    //socket.pipe(socket);

    console.log(server.address());
});

function loadPluginList(socket){

    console.log("-> loadPluginList");

    var plugins = [];

    fs.readdir("plugins", function(err, files) {
        if (err) throw err; //early out
        //
        for (var i = 0; i < files.length; ++i) {
           loadFile(files[i],function (filename, data){
               socket.write(JSON.stringify({id:filename,result:data.toString()}));
               //socket.write(data);
           });
        }
    });
}

function loadFile(filename, callback){
    fs.readFile("plugins/" + filename, function (errRead, data) {
        if (errRead) throw errRead;
        //
        callback(filename,data);
    });
}

server.listen(8000, "172.25.83.106");