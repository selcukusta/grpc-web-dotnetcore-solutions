/* eslint-disable */
//@ts-nocheck

/**
 * @fileoverview gRPC-Web generated client stub for photo
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!



const grpc = {};
grpc.web = require('grpc-web');

const proto = {};
proto.photo = require('./photo_pb.js');

/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.photo.PhotoClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

};


/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.photo.PhotoPromiseClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.photo.PhotoRequest,
 *   !proto.photo.PhotoReply>}
 */
const methodDescriptor_Photo_SayHello = new grpc.web.MethodDescriptor(
  '/photo.Photo/SayHello',
  grpc.web.MethodType.SERVER_STREAMING,
  proto.photo.PhotoRequest,
  proto.photo.PhotoReply,
  /**
   * @param {!proto.photo.PhotoRequest} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.photo.PhotoReply.deserializeBinary
);


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.photo.PhotoRequest,
 *   !proto.photo.PhotoReply>}
 */
const methodInfo_Photo_SayHello = new grpc.web.AbstractClientBase.MethodInfo(
  proto.photo.PhotoReply,
  /**
   * @param {!proto.photo.PhotoRequest} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.photo.PhotoReply.deserializeBinary
);


/**
 * @param {!proto.photo.PhotoRequest} request The request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!grpc.web.ClientReadableStream<!proto.photo.PhotoReply>}
 *     The XHR Node Readable Stream
 */
proto.photo.PhotoClient.prototype.sayHello =
    function(request, metadata) {
  return this.client_.serverStreaming(this.hostname_ +
      '/photo.Photo/SayHello',
      request,
      metadata || {},
      methodDescriptor_Photo_SayHello);
};


/**
 * @param {!proto.photo.PhotoRequest} request The request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!grpc.web.ClientReadableStream<!proto.photo.PhotoReply>}
 *     The XHR Node Readable Stream
 */
proto.photo.PhotoPromiseClient.prototype.sayHello =
    function(request, metadata) {
  return this.client_.serverStreaming(this.hostname_ +
      '/photo.Photo/SayHello',
      request,
      metadata || {},
      methodDescriptor_Photo_SayHello);
};


module.exports = proto.photo;

