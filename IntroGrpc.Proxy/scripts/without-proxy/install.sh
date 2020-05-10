#/bin/bash
echo '127.0.0.1 grpc.local' | sudo tee -a /etc/hosts

# Copy all required assets from host to guest
sudo cp /vagrant/certificates/grpc.local.pfx /etc/ssl/private/

sudo mkdir -p /var/www/application
sudo cp -r /vagrant/application/* /var/www/application

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