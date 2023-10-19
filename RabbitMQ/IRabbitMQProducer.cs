namespace OgrenciSistem.RabbitMQ;

public interface IRabbitMQProducer{

//Generic tip alan, her tip alabilmesi için
    public void SendMessage<T>(T message);
}