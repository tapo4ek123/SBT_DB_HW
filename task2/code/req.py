import json
import time
import redis


with open('international-migration.json', 'r') as f:
  data = json.load(f)

client = redis.Redis(host='localhost', port=6379)


print('###################################')

print('set as json')
start_time = time.time()
client.json().set('json', redis.commands.json.path.Path.root_path(), data)
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')

print('###################################')


data = json.dumps(data)


print('set as hset')
start_time = time.time()
client.hset('hset', 'data', data)
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')


print('set as str')
start_time = time.time()
client.set('str', data)
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')


print('set as zset')
start_time = time.time()
client.zadd('zset', {data : 1})
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')


print('set as list')
start_time = time.time()
client.rpush('list', data)
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')


#########################

print('###################################')
print('read as json')
start_time = time.time()
client.json().get('json')
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')

print('read as hset')
start_time = time.time()
client.hgetall('hset')
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')


print('read as str')
start_time = time.time()
client.get('str')
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')


print('read as zset')
start_time = time.time()
client.zrange('zset', 0, 1)
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')


print('read as list')
start_time = time.time()
client.lrange('list', 0, 1)
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')


cluster = redis.cluster.RedisCluster('92.63.192.58', port=6379, password='pass')


print('set as str in a cluster')
start_time = time.time()
cluster.set('str', data)
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')

print('read as str from cluster')
start_time = time.time()
k = cluster.get('str')
finish_time = time.time()
print((finish_time - start_time) * 1000, 'ms')
print('###################################')