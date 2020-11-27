# Update the VARIANT arg in devcontainer.json to pick an Ubuntu version: focal (or ubuntu-20.04), bionic (or ubuntu-18.04)
ARG VARIANT="focal"
FROM buildpack-deps:${VARIANT}-curl

# This Dockerfile adds a non-root user with sudo access. Update the “remoteUser” property in
# devcontainer.json to use it. More info: https://aka.ms/vscode-remote/containers/non-root-user.
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID

# Options for common setup script - SHA generated on release
ARG INSTALL_ZSH="true"
ARG UPGRADE_PACKAGES="true"
ARG COMMON_SCRIPT_SOURCE="https://raw.githubusercontent.com/microsoft/vscode-dev-containers/master/script-library/common-debian.sh"
ARG COMMON_SCRIPT_SHA="dev-mode"

###
### General devlopment
###
RUN apt-get update \
    && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends build-essential tar curl zip unzip g++

ENV TZ=Europe/Moscow
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

###
### .NET 5
###
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O /tmp/packages-microsoft-prod.deb \
    && dpkg -i /tmp/packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y apt-transport-https \
    && apt-get install -y dotnet-sdk-5.0 \
    && apt-get install -y dotnet-runtime-5.0

###
### Node.js
###
RUN curl -sL https://deb.nodesource.com/setup_14.x -o /tmp/nodesource_setup.sh && chmod +x /tmp/nodesource_setup.sh \ 
    && /tmp/nodesource_setup.sh && rm /tmp/nodesource_setup.sh \ 
    && apt install nodejs

###
### CMake
###
RUN wget -qO- "https://cmake.org/files/v3.18/cmake-3.18.1-Linux-x86_64.tar.gz" | tar --strip-components=1 -xz -C /usr/local

###
### Go
###
RUN wget -qO- "https://golang.org/dl/go1.15.linux-amd64.tar.gz" | tar --strip-components=1 -xz -C /usr/local

###
### Bazel
###
RUN apt install curl gnupg \
    && curl https://bazel.build/bazel-release.pub.gpg | apt-key add -; \
    echo "deb [arch=amd64] https://storage.googleapis.com/bazel-apt stable jdk1.8" | tee /etc/apt/sources.list.d/bazel.list \
    && apt-get update \
    && apt-get -y install bazel \
    && wget -q "https://github.com/bazelbuild/buildtools/releases/download/3.4.0/buildifier" -P /usr/local/bin &&  chmod +x /usr/local/bin/buildifier \
    && wget -q "https://github.com/bazelbuild/buildtools/releases/download/3.4.0/buildozer" -P /usr/local/bin && chmod +x /usr/local/bin/buildozer \
    && wget -q "https://github.com/bazelbuild/buildtools/releases/download/3.4.0/unused_deps" -P /usr/local/bin && chmod +x /usr/local/bin/unused_deps

###
### LLDB
###
RUN apt-get install gnupg lldb g++ valgrind -y 

###
### Unix ODBC driver for MSSQL Server 2017
###
RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -; \
    curl https://packages.microsoft.com/config/ubuntu/19.10/prod.list > /etc/apt/sources.list.d/mssql-release.list; \
    apt-get update \
    && ACCEPT_EULA=Y apt-get -y install msodbcsql17 mssql-tools unixodbc-dev

###
### Docker 19.03.8
###
RUN apt-get -y install docker.io

###
### Docker-compose 1.26.2
###
RUN curl -L "https://github.com/docker/compose/releases/download/1.26.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose \
    && chmod +x /usr/local/bin/docker-compose

RUN apt-get update \
    && export DEBIAN_FRONTEND=noninteractive \
    && apt-get install -y python3 python3-dev python3-pip python3-venv python3-behave pipenv

RUN ln -s /usr/bin/python3 /usr/bin/python & \
    ln -s /usr/bin/pip3 /usr/bin/pip

# Install needed packages and setup non-root user. Use a separate RUN statement to add your own dependencies.
RUN apt-get update \
    && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends curl ca-certificates 2>&1 \
    && curl -sSL  ${COMMON_SCRIPT_SOURCE} -o /tmp/common-setup.sh \
    && ([ "${COMMON_SCRIPT_SHA}" = "dev-mode" ] || (echo "${COMMON_SCRIPT_SHA} /tmp/common-setup.sh" | sha256sum -c -)) \
    && /bin/bash /tmp/common-setup.sh "${INSTALL_ZSH}" "${USERNAME}" "${USER_UID}" "${USER_GID}" "${UPGRADE_PACKAGES}" \
    # Clean up
    && apt-get autoremove -y \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/* /tmp/common-setup.sh