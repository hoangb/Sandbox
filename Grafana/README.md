docker pull prom/prometheus
docker run -p 9090:9090 -v C:/git/Sandbox/Grafana/prometheus.yml:/etc/prometheus/prometheus.yml prom/prometheus


# Setup
## create a persistent volume for your data in /var/lib/grafana (database and plugins)
docker volume create grafana-storage

## start grafana
docker run -d --name=grafana -p 3000:3000 grafana/grafana

or

docker run -d -p 3000:3000 --name=grafana -v grafana-storage:/var/lib/grafana grafana/grafana


to login: admin/admin