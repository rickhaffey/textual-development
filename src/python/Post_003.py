from elasticsearch import Elasticsearch
import json

host = "localhost"
es = Elasticsearch("http://" + host + ":9200")

print(json.dumps(es.info(), indent=4, sort_keys=True))
