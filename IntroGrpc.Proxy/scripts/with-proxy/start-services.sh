#/bin/bash
sudo cat > /etc/systemd/system/grpcnetcore.service  << EOF
[Unit]
Description=.NET Core GRPC Application
Requires=network-online.target
After=network-online.target

[Service]
Type=simple
WorkingDirectory=/var/www/application
ExecStart=/var/www/application/IntroGrpc.Server
Restart=always
RestartSec=30
KillSignal=SIGINT
SyslogIdentifier=grpcnetcore
User=root
Environment=ASPNETCORE_URLS=http://127.0.0.1:8080
Environment=ASPNETCORE_AllowedHosts=grpc.local
Environment=ASPNETCORE_Kestrel__EndpointDefaults__Protocols=Http2

[Install]
WantedBy=multi-user.target
EOF

sudo cat > /etc/systemd/system/grpcproxy.service  << EOF
[Unit]
Description=Envoy Proxy for GRPC Service
Requires=network-online.target
After=network-online.target

[Service]
Type=simple
ExecStart=/usr/bin/envoy -c /var/www/proxy/envoy.yaml -l trace --log-path /tmp/envoy_info.log --service-cluster netcore_grpc_service
Restart=always
KillSignal=SIGINT
RestartSec=30
User=root

[Install]
WantedBy=multi-user.target
EOF

sudo systemctl daemon-reload
sudo systemctl enable grpcnetcore.service
sudo systemctl start grpcnetcore.service
sudo systemctl enable grpcproxy.service
sudo systemctl start grpcproxy.service