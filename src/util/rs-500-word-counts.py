import os
import json
from collections import Counter


filepath = './rolling-stone-500.word-counts.txt'
words = {}

with open(filepath, 'w') as f:
    # write out each album entry
    for rank in range(500):

        # copy the file contents to the script
        with open('./albums/' + str(rank + 1) + '.json', 'r') as f2:
            j = json.loads(f2.readline())
            splitWords = j['summary'].split()
            wordCount = Counter(splitWords)
            for k in wordCount.keys():
                if k in words:
                    words[k] += wordCount[k]
                else:
                    words[k] = wordCount[k]


    for k in words.keys():
        f.write(str(words[k]) + "|" + k.encode("UTF-8"))
        f.write("\n")

print("OK") 
