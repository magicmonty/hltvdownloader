#!/bin/sh
echo "deb http://ppa.launchpad.net/ermshiperete/monodevelop-beta/ubuntu precise main" > /etc/apt/sources.list.d/monodevelop-beta.list
gpg --ignore-time-conflict --no-options --no-default-keyring --secret-keyring /etc/apt/secring.gpg --trustdb-name /etc/apt/trustdb.gpg --keyring /etc/apt/trusted.gpg --keyserver keyserver.ubuntu.com --recv 6F242C166A1B440BA3C43CBD48B6803E839ECBBE
apt-get update
apt-get -y install mono-devel mono-gmcs nunit-console monodevelop-current
