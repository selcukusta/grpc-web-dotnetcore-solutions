#/bin/bash
sudo cat > /etc/systemd/system/grpcnetcore.service  << EOF
[Unit]
Description=.NET Core GRPC Application
Requires=network-online.target
After=network-online.target

[Service]
Type=simple
WorkingDirectory=/var/www/application
ExecStart=/var/www/application/IntroGrpc.CoreServer
Restart=always
RestartSec=30
KillSignal=SIGINT
SyslogIdentifier=grpcnetcore
User=root
Environment=ASPNETCORE_URLS=https://+:443
Environment=ASPNETCORE_AllowedHosts=grpc.local
Environment=ASPNETCORE_Kestrel__Certificates__Default__Path=/etc/ssl/private/grpc.local.pfx
Environment=ASPNETCORE_Kestrel__Certificates__Default__Password=1
############ PROTOCOL ############
# Default protocol selection is Http1AndHttp2. So you don't have to specify it.
#Environment=ASPNETCORE_Kestrel__EndpointDefaults__Protocols=Http1AndHttp2
# If you force Http1, web clients are working properly but backend clients are failed.
#Environment=ASPNETCORE_Kestrel__EndpointDefaults__Protocols=Http1
# If you force Http2 and you have another endpoints which should be crawled by bots, it's failed.
#Environment=ASPNETCORE_Kestrel__EndpointDefaults__Protocols=Http2

[Install]
WantedBy=multi-user.target
EOF

sudo systemctl daemon-reload
sudo systemctl enable grpcnetcore.service
sudo systemctl start grpcnetcore.service