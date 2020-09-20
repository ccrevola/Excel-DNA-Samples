namespace Communications.Messaging
{
    public class Message<T>  where T: class
    {
        public string Header { get; set; }
        public T Data { get; set; }
    }
}