const { HelloRequest } = require("./greet_pb.js");
const { GreeterClient } = require("./greet_grpc_web_pb.js");

var greeterService = new GreeterClient("https://grpc.local");

var request = new HelloRequest();
request.setName("Martial!");

greeterService.sayHello(request, {}, function (err, response) {
  document.getElementById(
    "from-grpc-service"
  ).innerHTML = response.getMessage();
});
