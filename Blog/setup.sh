#!/bin/bash
# Install Zip if not present
apt-get update
apt-get install -y unzip mysql-server nginx

# Create DB
mysql -e "CREATE DATABASE IF NOT EXISTS Blog_DB;"
mysql -e "ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'meto9722';"
mysql -e "FLUSH PRIVILEGES;"

# Create Web Dir
mkdir -p /var/www/blog
unzip -o /root/publish_linux.zip -d /var/www/blog
chmod +x /var/www/blog/Blog

# Systemd Service
mv /root/blog.service /etc/systemd/system/blog.service
systemctl daemon-reload
systemctl enable blog
systemctl restart blog

# Nginx
mv /root/blog.nginx /etc/nginx/sites-available/blog
ln -sf /etc/nginx/sites-available/blog /etc/nginx/sites-enabled/blog
rm -f /etc/nginx/sites-enabled/default
systemctl restart nginx

echo "Setup Complete"
