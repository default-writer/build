#!/usr/bin/env bash
set -e
sudo apt-get update;
sudo apt-get install -y apt-transport-https && sudo apt-get update
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
wget https://download.visualstudio.microsoft.com/download/pr/c1a30ceb-adc2-4244-b24a-06ca29bb1ee9/6df5d856ff1b3e910d283f89690b7cae/dotnet-sdk-3.1.302-linux-x64.tar.gz
mkdir -p $HOME/dotnet && tar zxf dotnet-sdk-3.1.302-linux-x64.tar.gz -C $HOME/dotnet
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet