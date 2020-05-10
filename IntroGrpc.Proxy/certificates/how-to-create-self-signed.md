# Create .crt and .key file

```bash
openssl req \
    -keyout grpc.local.key\
    -newkey rsa:2048 \
    -x509 \
    -nodes \
    -new \
    -out grpc.local.crt \
    -subj "/CN=grpc.local" \
    -reqexts SAN \
    -extensions SAN \
    -config <(cat /System/Library/OpenSSL/openssl.cnf \
        <(printf '[SAN]\nsubjectAltName=DNS:grpc.local')) \
    -sha256 \
    -days 3650
```

# Create .pfx file with password "1"

```bash
openssl pkcs12 -export -out grpc.local.pfx -inkey grpc.local.key -in grpc.local.crt
```
