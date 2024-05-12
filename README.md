# TinyURL
For caching I used this method because it uses a dictionary to allow fast acces to the key/value pairs, 
it also uses a linked list to maintain the order of access, allowing efficient removal of the least recently used pair,
and also when the cache reaches the maximum capacity, it removes the least recently used pair before adding a new pair.
