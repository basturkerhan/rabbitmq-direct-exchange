### RabbitMQ ile Direct Exchange yapısı kullanılarak oluşturulmuş Publisher-Consumer konsol uygulamasıdır.
### Dockerfile dosyaları içerisindeki ENV URI alanına RabbitMQ Cloud adresi yazılmalıdır.

### ./UdemyRabbitMQ.publisher
#### docker build -t direct-exc-pub-img .
#### docker run --name direct-exc-pub-con direct-exc-pub-img

### ./UdemyRabbitMQ.subscriber
#### docker build -t direct-exc-subs-img .
#### docker run --name direct-exc-subs-con direct-exc-subs-img
