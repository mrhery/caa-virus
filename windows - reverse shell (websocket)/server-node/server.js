import { createServer } from 'https';
import { readFileSync } from 'fs';
import { WebSocketServer } from 'ws';

const server = createServer({
	cert: readFileSync('J:/xampp8.0/apache/conf/ssl.crt/server.crt'),
	key: readFileSync('J:/xampp8.0/apache/conf/ssl.key/server.key')
});

// const wss = new WebSocketServer({ server });
const wss = new WebSocketServer({ port:8080 });

var admin = [];
var client = [];

wss.on('connection', function connection(ws, req) {
	console.log(req.url);
	
	if(req.url == "/admin"){
		admin.push(ws);
	}else{
		client.push(ws);
	}
	
	ws.on('error', console.error);

	ws.on('message', function message(data) {
		var obj = JSON.parse(data);
		
		if(obj.action != undefined){
			switch(obj.action){
				case "cmd":
					client.forEach(function(cl){
						cl.send(JSON.stringify({
							action: "cmd",
							cmd: obj.cmd
						}));
					});
				break;
				
				case "result":
					admin.forEach(function(ad){
						ad.send(JSON.stringify({
							action: "result",
							result: obj.result
						}));
					});
				break;
			}
		}
	});
	
	ws.on("close", function(){
		
	});
});

// server.listen(8080);