#/bin/bash
echo '127.0.0.1 grpc.local' | sudo tee -a /etc/hosts

# Copy all required assets from host to guest
sudo cp /vagrant/certificates/grpc.local.crt /etc/ssl/certs/
sudo cp /vagrant/certificates/grpc.local.key /etc/ssl/private/

sudo mkdir -p /var/www/application
sudo cp -r /vagrant/application/* /var/www/application

sudo mkdir -p /var/www/proxy
sudo cp -r /vagrant/proxy/envoy.yaml /var/www/proxy

#  Install base packages
sudo apt-get update
sudo apt-get install -y \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg-agent \
    software-properties-common

# Install dotnet-runtime-3.1
wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo add-apt-repository universe
sudo apt-get update
sudo apt-get install -y \
    dotnet-runtime-3.1

# Install envoy
curl -sL 'https://getenvoy.io/gpg' | sudo apt-key add -
apt-key fingerprint 6FF974DB
sudo add-apt-repository \
"deb [arch=amd64] https://dl.bintray.com/tetrate/getenvoy-deb \
$(lsb_release -cs) \
stable"
sudo apt-get update
sudo apt-get install -y \
    getenvoy-envoy

