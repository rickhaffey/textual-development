import os
import json

# iterate through the album json files, and generate a bulk index script
# note: bulk index spec format:
# { "index" : { "_index" : "test", "_type" : "type1", "_id" : "1" } }

filepath = './rolling-stone-500.es.bulk-load'
with open(filepath, 'w') as f:
    # write the top level request
    f.write("POST /_bulk\n")

    # write out each album entry
    for rank in range(500):
        header = { "index": { } }
        index = header['index']
        index['_index'] = "rolling-stone-500"
        index['_type'] = "album"
        index['_id'] = str(rank + 1)

        f.write(unicode(json.dumps(header)))
        f.write("\n")

        # copy the file contents to the script
        with open('./albums/' + str(rank + 1) + '.json', 'r') as f2:
            f.write(f2.readline())
            f.write("\n")
