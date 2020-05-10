import React, { useState, useEffect } from "react";
import logo from "./logo.svg";
import "./App.css";

const { HelloRequest } = require("./public/greet_pb.js");
const { GreeterClient } = require("./public/greet_grpc_web_pb.js");

const { PhotoRequest } = require("./public/photo_pb.js");
const { PhotoClient } = require("./public/photo_grpc_web_pb.js");

function App() {
  const [helloResponse, setHelloResponse] = useState({
    text: "Waiting from gRPC server...",
  });
  const [photoResponse, setPhotoResponse] = useState({ uri: logo });

  function refresh() {
    let unaryRequest = new HelloRequest();
    unaryRequest.setName("Martial!");
    new GreeterClient("https://grpc.local").sayHello(
      unaryRequest,
      {},
      function (error, response) {
        if (error) {
          console.log(`ERROR-(${error.code}): ${error.message}`);
        } else {
          setHelloResponse({ text: response.getMessage() });
        }
      }
    );

    let serverStreamRequest = new PhotoRequest();
    serverStreamRequest.setName("Martial!");
    var stream = new PhotoClient("https://grpc.local").sayHello(
      serverStreamRequest,
      {
        //Authorization: "Bearer " + window.localStorage.getItem("token"),
      }
    );

    stream.on("data", function (response) {
      var arrayBufferView = new Uint8Array(response.getMessage());
      var blob = new Blob([arrayBufferView], { type: "image/png" });
      var urlCreator = window.URL || window.webkitURL;
      var imageUrl = urlCreator.createObjectURL(blob);
      setPhotoResponse({ uri: imageUrl });
    });

    // stream.on("status", function (status) {
    //   console.log(status.code);
    //   console.log(status.details);
    //   console.log(status.metadata);
    // });
    // stream.on("end", function (end) {
    //   // stream end signal
    // });
  }

  useEffect(() => refresh(), []);

  return (
    <div className="App">
      <header className="App-header">
        <img src={photoResponse.uri} className="App-logo" alt="logo" />
        <p>{helloResponse.text}</p>&nbsp;
        <button onClick={refresh}>Call Again!</button>
      </header>
    </div>
  );
}

export default App;
