import os
import json

filepath = './rolling-stone-500.summary-dumps.txt'

with open(filepath, 'w') as f:
    # write out each album entry
    for rank in range(500):

        # copy the file contents to the script
        with open('./albums/' + str(rank + 1) + '.json', 'r') as f2:
            j = json.loads(f2.readline())
            f.write(j['summary'].encode("UTF-8"))
            f.write("\n")
print("OK") 
