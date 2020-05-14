# BanIpsMiddleware

with this middleware you can handle ban ips in your application.
ips and details for ban, save in json file in root of your project and in middleware you use it.

you should invoke BanIpsMiddleware in startup class (Configure method) and when you need to add a ip for ban it,just add ip to json file .
It is best to write this middleware on top of the Configure method.
