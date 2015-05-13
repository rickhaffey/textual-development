from elasticsearch import Elasticsearch, RequestsHttpConnection
import json


def specify_host_connect(host):
    # simple connection
    return Elasticsearch(host=host, port=9200)

def specify_host_proxy_connect(host):
    # simple connection
    # with proxy support so we can capture traffic via Fiddler
    return Elasticsearch(host=host, port=9200, connection_class=RequestsHttpConnection)

def connection_pool_connect(host):
    # asks for nodes in the cluster,
    # then calls nodes round-robin, 
    # and provides failover support
    return Elasticsearch(host=host, port=9200)

def get_info(client):
    return json.dumps(es.info(), indent=4, sort_keys=True)


if __name__ == "__main__":
    es = specify_host_proxy_connect('localhost')
    print(get_info(es))
