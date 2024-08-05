namespace TVMazeAPI
{
    public class Show
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Person> Cast { get; set; } = new();
    }
}
